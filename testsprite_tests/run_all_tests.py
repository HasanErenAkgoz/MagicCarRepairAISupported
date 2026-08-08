"""Run all TestSprite backend API scripts locally (requires API on TEST_API_BASE_URL)."""

from __future__ import annotations

import subprocess
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent


def main() -> int:
    scripts = sorted(ROOT.glob("TC*.py"))
    if not scripts:
        print("No TC*.py tests found.")
        return 1

    failed: list[str] = []
    for script in scripts:
        print(f"\n=== {script.name} ===")
        result = subprocess.run([sys.executable, str(script)], cwd=ROOT)
        if result.returncode != 0:
            failed.append(script.name)

    print("\n--- Summary ---")
    passed = len(scripts) - len(failed)
    print(f"Passed: {passed}/{len(scripts)}")
    if failed:
        print("Failed:", ", ".join(failed))
        return 1

    print("All tests passed.")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
