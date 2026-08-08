#!/usr/bin/env python3
"""Fast, local-only release guardrails.

This intentionally performs no network calls and never starts the API. Runtime
authorization integration tests remain the authoritative check; this guard
prevents two easy-to-miss regressions from reaching that stage:

* sensitive AI image-analysis routes becoming anonymous; and
* runtime uploads being committed to Git.
"""

from __future__ import annotations

import re
import subprocess
import sys
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1]
AI_CONTROLLER = ROOT / "Core.Packages.WebAPI" / "Controllers" / "AIController.cs"
UPLOAD_ROOT = "Core.Packages.WebAPI/wwwroot/uploads/"
PROTECTED_AI_ROUTES = ("diagnose", "analyze-damage-photos")
ENVELOPED_NEW_AI_ROUTES = ("diagnosis-assets",)


def fail(message: str) -> None:
    print(f"FAIL: {message}", file=sys.stderr)
    raise SystemExit(1)


def action_attributes_by_route(source: str) -> dict[str, str]:
    """Return the contiguous C# attributes attached to each API action route."""
    pattern = re.compile(
        r"(?P<attributes>(?:\s*\[[^\]]+\]\s*)+)"
        r"public\s+async\s+Task<IActionResult>\s+\w+\s*\(",
        re.MULTILINE,
    )
    routes: dict[str, str] = {}
    for match in pattern.finditer(source):
        attributes = match.group("attributes")
        route = re.search(r'Http(?:Get|Post|Put|Delete)\("(?P<route>[^"]+)"\)', attributes)
        if route:
            routes[route.group("route")] = attributes
    return routes


def verify_ai_routes() -> None:
    source = AI_CONTROLLER.read_text(encoding="utf-8")
    actions = action_attributes_by_route(source)
    for route in PROTECTED_AI_ROUTES:
        attributes = actions.get(route)
        if attributes is None:
            fail(f"protected AI route '{route}' was not found in {AI_CONTROLLER.relative_to(ROOT)}")
        if "AllowAnonymous" in attributes:
            fail(f"protected AI route '{route}' must not allow anonymous access")
        if "Authorize" not in attributes:
            fail(f"protected AI route '{route}' must declare an authorization policy explicitly")

    # New JSON endpoints must return the application result directly. This keeps
    # the mobile `{ success, data, message? }` contract stable; binary media
    # endpoints are intentionally excluded because their response is a file.
    for route in ENVELOPED_NEW_AI_ROUTES:
        attributes = actions.get(route)
        if attributes is None:
            fail(f"enveloped AI route '{route}' was not found in {AI_CONTROLLER.relative_to(ROOT)}")
    if "return result.Success ? Ok(result) : BadRequest(result);" not in source:
        fail("diagnosis-assets must return its IDataResult envelope for both success and validation failure")


def verify_uploads_not_tracked() -> None:
    result = subprocess.run(
        ["git", "ls-files", "--", UPLOAD_ROOT],
        cwd=ROOT,
        check=True,
        capture_output=True,
        text=True,
    )
    tracked = [line for line in result.stdout.splitlines() if line]
    if tracked:
        fail("runtime uploads must not be tracked:\n  " + "\n  ".join(tracked))


def main() -> None:
    verify_ai_routes()
    verify_uploads_not_tracked()
    print("Production readiness source checks passed.")


if __name__ == "__main__":
    main()
