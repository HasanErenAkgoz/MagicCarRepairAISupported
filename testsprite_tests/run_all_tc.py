"""Run all TC*.py from testsprite_backend_test_plan (local verification)."""

from __future__ import annotations

import json
import os
import subprocess
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent
PLAN = ROOT / "testsprite_backend_test_plan.json"
# Per-TC cap so one hung API request (e.g. VS break on DomainException) does not fail the whole suite.
PER_TC_TIMEOUT_SEC = int(os.environ.get("TEST_TC_SUBPROCESS_TIMEOUT", "150"))


def main() -> int:
    plan = json.loads(PLAN.read_text(encoding="utf-8"))
    env = os.environ.copy()
    env["PYTHONPATH"] = str(ROOT) + os.pathsep + env.get("PYTHONPATH", "")

    failed: list[str] = []
    for case in plan:
        tc_id = case["id"]
        matches = sorted(ROOT.glob(f"{tc_id}_*.py"))
        if not matches:
            failed.append(f"{tc_id} (missing file)")
            continue
        script = matches[0]
        try:
            result = subprocess.run(
                [sys.executable, str(script)],
                cwd=ROOT,
                env=env,
                timeout=PER_TC_TIMEOUT_SEC,
            )
        except subprocess.TimeoutExpired:
            failed.append(f"{script.name} (timeout {PER_TC_TIMEOUT_SEC}s)")
            continue
        if result.returncode != 0:
            failed.append(script.name)

    total = len(plan)
    passed = total - len(failed)
    print(f"TC passed: {passed}/{total}")
    if failed:
        print("Failed:", ", ".join(failed))
        return 1
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
