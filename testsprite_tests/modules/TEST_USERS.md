# Test user strategy

| Role | Source |
|------|--------|
| SystemAdmin / Shop | `admin@magiccar.com` / `Admin123!` (seed) |
| Manager / Employee | `jwt_for_user_type(2|3)` for **403 policy-only** checks (no DB user lookup) |
| Customer portal / profile | `register_and_login_customer()` — real user in DB (synthetic JWT causes `User not found`) |
| Shop CRM customers | `POST /api/Customers` (entity only, not portal login) |

Module M02 may add API-created Manager/Employee accounts in a later iteration.
