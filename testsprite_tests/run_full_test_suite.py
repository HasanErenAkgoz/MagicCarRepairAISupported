"""Seed DB if needed, then run all module tests and legacy TC smoke tests."""
from __future__ import annotations

import subprocess
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent


def run_step(label: str, args: list[str]) -> int:
    print(f"\n{'=' * 60}\n{label}\n{'=' * 60}")
    result = subprocess.run(args, cwd=ROOT)
    return result.returncode


def main() -> int:
    steps: list[tuple[str, list[str]]] = [
        ("1/3 — Ensure database seed data", [sys.executable, "ensure_db_seed.py"]),
        ("2/3 — Module tests M01–M20", [sys.executable, "run_all_modules.py"]),
        ("3/4 — Generate TC001–TC040 scripts", [sys.executable, "generate_mcp_tc_files.py"]),
    ]
    for label, args in steps:
        code = run_step(label, args)
        if code != 0:
            print(f"\nFailed at: {label}")
            return code

    code = run_step("4/4 — Test plan TC001–TC040", [sys.executable, "run_all_tc.py"])
    if code != 0:
        return code
    print("\nFull suite passed (seed + modules + TC).")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
