# TestSprite backend tests (Magic Car Repair)

## Tek komut (seed + TC uret + 40 test)

Explorer'da `.ps1` dosyasina cift tiklamayin — Notepad acar. Bunun yerine:

```cmd
cd c:\Projects\MagicCarRepairAISupported
scripts\run-testsprite.cmd
```

## Local run (recommended)

```powershell
cd c:\Projects\MagicCarRepairAISupported\testsprite_tests
pip install -r requirements.txt
# API must be running on http://localhost:5169
python run_full_test_suite.py   # scenario seed + M01–M20 + TC001–TC040
python ensure_db_seed.py        # scenario-based DB seed (run before any tests)
python run_all_modules.py       # 20 modules (M01–M20)
python run_all_tests.py         # legacy smoke TC001–TC010
python run_module_tests.py M06_workorders
```

## Scenario-based database seed

Before tests, `ensure_db_seed.py` reads [`scenario_seed_manifest.json`](scenario_seed_manifest.json) and creates data via the API until targets are met (customers with vehicles/work orders, parts, employees, portal user). It also calls `POST /api/SeedData/run` for empty tables (PostgreSQL-safe after API rebuild). Report: `tmp/scenario_seed_report.json`.

Tests use [`api_support.py`](api_support.py) for login (`data.token`) and valid DTO payloads.

**Customer portal:** use `register_and_login_customer()` — a synthetic JWT without a DB user causes `UnauthorizedAccessException: User not found`. See [`modules/TEST_USERS.md`](modules/TEST_USERS.md).

## TestSprite MCP cloud run

`testsprite_generate_code_and_execute` may **overwrite** `TC*.py` with auto-generated scripts. If cloud results regress, restore this folder from git and use **local** `run_all_tests.py` as the source of truth.

## Bootstrap credentials

- Email: `admin@magiccar.com`
- Password: `Admin123!`

See [docs/DATABASE.md](../docs/DATABASE.md).
