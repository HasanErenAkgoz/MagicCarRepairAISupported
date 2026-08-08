# M02 Auth — TEST_MATRIX

| ID | Method | Path | Auth | Expected |
|----|--------|------|------|----------|
| M02-01 | POST | /api/Auth/login | None | 200, data.token |
| M02-02 | POST | /api/Auth/login | Bad password | 400 |
| M02-03 | POST | /api/Auth/refresh-token | Body | 200 or 400 |
| M02-04 | GET | /api/user/profile | Bearer | 200 |
| M02-05 | POST | /api/Auth/impersonate | Manager | 403 |
