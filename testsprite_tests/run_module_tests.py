"""Run all test_*.py scripts in a single module folder."""

from __future__ import annotations

import argparse
import os
import subprocess
import sys
from datetime import datetime, timezone
from pathlib import Path

from module_registry import MODULES, find_module

ROOT = Path(__file__).resolve().parent
MODULES_DIR = ROOT / "modules"


def run_scripts(scripts: list[Path], cwd: Path) -> tuple[list[str], list[str]]:
    passed: list[str] = []
    failed: list[str] = []
    env = os.environ.copy()
    env["PYTHONPATH"] = str(cwd) + os.pathsep + env.get("PYTHONPATH", "")
    for script in scripts:
        result = subprocess.run([sys.executable, str(script)], cwd=cwd, env=env)
        if result.returncode == 0:
            passed.append(script.name)
        else:
            failed.append(script.name)
    return passed, failed


def write_report(module_folder: Path, passed: list[str], failed: list[str]) -> None:
    total = len(passed) + len(failed)
    lines = [
        f"# Module report: {module_folder.name}",
        "",
        f"- **Date:** {datetime.now(timezone.utc).strftime('%Y-%m-%d %H:%M UTC')}",
        f"- **Passed:** {len(passed)}/{total}",
        f"- **Failed:** {len(failed)}/{total}",
        "",
    ]
    if failed:
        lines.append("## Failed")
        for name in failed:
            lines.append(f"- {name}")
        lines.append("")
    if passed:
        lines.append("## Passed")
        for name in passed:
            lines.append(f"- {name}")
    (module_folder / "REPORT.md").write_text("\n".join(lines) + "\n", encoding="utf-8")


def main() -> int:
    parser = argparse.ArgumentParser(description="Run module API tests")
    parser.add_argument("module", help="Module code or folder, e.g. M01 or M06_workorders")
    args = parser.parse_args()

    info = find_module(args.module)
    if not info:
        print(f"Unknown module: {args.module}")
        return 1

    module_dir = MODULES_DIR / info.folder
    if not module_dir.is_dir():
        print(f"Module folder missing: {module_dir}")
        return 1

    scripts = sorted(module_dir.glob("test_*.py"))
    if not scripts:
        print(f"No test_*.py in {module_dir}")
        return 1

    print(f"Running {info.code} {info.name} ({len(scripts)} files)")
    passed, failed = run_scripts(scripts, ROOT)
    write_report(module_dir, passed, failed)

    print(f"Passed: {len(passed)}/{len(scripts)}")
    if failed:
        print("Failed:", ", ".join(failed))
        return 1
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
