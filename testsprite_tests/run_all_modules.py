"""Run every registered module in order and summarize."""

from __future__ import annotations

import subprocess
import sys
from datetime import datetime, timezone
from pathlib import Path

from module_registry import MODULES

ROOT = Path(__file__).resolve().parent
SUMMARY_PATH = ROOT / "modules" / "RUN_SUMMARY.md"


def write_summary(failures: list[str], skipped: list[str]) -> None:
    passed = [m.code for m in MODULES if m.code not in failures and m.code not in skipped]
    lines = [
        "# Module test run summary",
        "",
        f"- **Date:** {datetime.now(timezone.utc).strftime('%Y-%m-%d %H:%M UTC')}",
        f"- **Modules passed:** {len(passed)}/{len(MODULES)}",
        f"- **Failures:** {len(failures)}",
        "",
    ]
    if failures:
        lines.append("## Failed modules")
        for code in failures:
            lines.append(f"- {code}")
        lines.append("")
    lines.append("## Passed modules")
    for code in passed:
        lines.append(f"- {code}")
    SUMMARY_PATH.write_text("\n".join(lines) + "\n", encoding="utf-8")


def main() -> int:
    failures: list[str] = []
    skipped: list[str] = []
    for module in MODULES:
        module_dir = ROOT / "modules" / module.folder
        if not module_dir.is_dir():
            print(f"SKIP {module.code}: folder missing")
            skipped.append(module.code)
            continue
        if not list(module_dir.glob("test_*.py")):
            print(f"SKIP {module.code}: no tests")
            skipped.append(module.code)
            continue

        print(f"\n{'=' * 60}\n{module.code} {module.name}\n{'=' * 60}")
        result = subprocess.run(
            [sys.executable, str(ROOT / "run_module_tests.py"), module.code],
            cwd=ROOT,
        )
        if result.returncode != 0:
            failures.append(module.code)

    write_summary(failures, skipped)
    print(f"\n{'=' * 60}")
    if failures:
        print("Modules with failures:", ", ".join(failures))
        return 1
    print("All modules passed.")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
