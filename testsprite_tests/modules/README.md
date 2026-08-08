# Backend API module tests (M01–M20)

Local Python integration tests; source of truth for API coverage. Do not rely on TestSprite `generateCodeAndExecute` overwriting root `TC*.py`.

## Run

```bash
cd testsprite_tests
pip install -r requirements.txt
python run_module_tests.py M01
python run_all_modules.py
```

## Environment

- API: `http://localhost:5169` (`TEST_API_BASE_URL`)
- PostgreSQL via `docker compose`
- Bootstrap: `admin@magiccar.com` / `Admin123!`
- JWT tests: `TokenOptions__SecurityKey` (optional env)

## Per module

Each folder has `TEST_MATRIX.md`, `test_*.py`, and `REPORT.md` (generated after run).
