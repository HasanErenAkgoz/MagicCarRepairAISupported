# M03 RBAC — TEST_MATRIX

| ID | Method | Path | Role | Expected |
|----|--------|------|------|----------|
| M03-01 | GET | /api/Clients | Admin | 200 |
| M03-02 | GET | /api/Clients | Anonymous | 401 |
| M03-03 | GET | /api/Clients | Customer | 403 |
| M03-04 | GET | /api/Role | Shop | 200 |
| M03-05 | GET | /api/Permission/getall | Admin | 200 |
