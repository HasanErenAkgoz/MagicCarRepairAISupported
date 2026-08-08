# M19 Portals

| ID | Endpoint | Auth | Expected |
|----|----------|------|----------|
| M19-01 | GET /api/public-clients/list | None | 200 |
| M19-02 | GET /api/customer-portal/profile | Registered customer | 200 |
| M19-03 | GET /api/customer-portal/profile | Manager JWT | 403 |
| M19-04 | GET /api/client-portal/profile | None | 401 |

Note: Synthetic customer JWT (user id 99) triggers `UnauthorizedAccessException: User not found` — use `register_and_login_customer()`.
