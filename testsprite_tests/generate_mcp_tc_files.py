"""Regenerate TC*.py files for TestSprite MCP cloud execution."""
from pathlib import Path

HEADER = '''import os
import time
import uuid
import jwt
import requests

BASE_URL = os.environ.get("TEST_API_BASE_URL", "http://localhost:5169").rstrip("/")
TIMEOUT = int(os.environ.get("TEST_API_TIMEOUT", "120"))
ADMIN_EMAIL = "admin@magiccar.com"
ADMIN_PASSWORD = "Admin123!"
JWT_SECRET = os.environ.get(
    "TokenOptions__SecurityKey",
    "YourSuperSecretKeyForJwtTokenSigning_MustBeAtLeast32Chars!",
)


def url(path: str) -> str:
    if not path.startswith("/"):
        path = "/" + path
    return f"{BASE_URL}{path}"


def extract_token(body: dict) -> str:
    data = body.get("data") or {}
    token = data.get("token")
    if token:
        return token
    raise AssertionError(f"Missing data.token in login response: {body}")


def admin_headers() -> dict[str, str]:
    response = requests.post(
        url("/api/Auth/login"),
        json={"email": ADMIN_EMAIL, "password": ADMIN_PASSWORD},
        timeout=TIMEOUT,
    )
    assert response.status_code == 200, f"Login failed: {response.text}"
    body = response.json()
    assert body.get("success") is True, body
    token = extract_token(body)
    return {"Authorization": f"Bearer {token}", "Content-Type": "application/json"}


MANAGER_EMAIL = "manager@magiccar.com"
MANAGER_PASSWORD = "Manager123!"


def first_customer_id(headers: dict[str, str]) -> int:
    """Real CRM customer id (never use Users.Id as customerId)."""
    r = requests.get(url("/api/Customers"), headers=headers, timeout=TIMEOUT)
    assert r.status_code == 200, r.text
    rows = r.json()
    assert isinstance(rows, list) and rows, "No customers — run ensure_db_seed.py first"
    return int(rows[0]["id"])


def manager_headers() -> dict[str, str]:
    response = requests.post(
        url("/api/Auth/login"),
        json={"email": MANAGER_EMAIL, "password": MANAGER_PASSWORD},
        timeout=TIMEOUT,
    )
    assert response.status_code == 200, f"Manager login failed: {response.text}"
    body = response.json()
    assert body.get("success") is True, body
    token = extract_token(body)
    return {"Authorization": f"Bearer {token}", "Content-Type": "application/json"}


def register_customer_headers() -> dict[str, str]:
    suffix = uuid.uuid4().hex[:8]
    email = f"mcp_{suffix}@example.com"
    password = "TestSprite1!"
    payload = {
        "firstName": "MCP",
        "lastName": f"User{suffix}",
        "email": email,
        "password": password,
        "confirmPassword": password,
        "phoneNumber": "5550000099",
        "language": "tr",
        "identityNo": f"TS{suffix[:10]}",
    }
    reg = requests.post(url("/api/Auth/register-customer"), json=payload, timeout=TIMEOUT)
    assert reg.status_code in (200, 201), reg.text
    login = requests.post(
        url("/api/Auth/login"),
        json={"email": email, "password": password},
        timeout=TIMEOUT,
    )
    assert login.status_code == 200, login.text
    token = extract_token(login.json())
    return {"Authorization": f"Bearer {token}", "Content-Type": "application/json"}


def jwt_headers(user_type: int) -> dict[str, str]:
    from datetime import datetime, timedelta, timezone

    now = datetime.now(timezone.utc)
    payload = {
        "sub": "99",
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": "99",
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress": f"test-{user_type}@test.local",
        "UserType": str(user_type),
        "ClientId": "1",
        "Language": "tr",
        "exp": now + timedelta(hours=1),
        "iss": os.environ.get("TokenOptions__Issuer", "MagicCarRepair"),
        "aud": os.environ.get("TokenOptions__Audience", "MagicCarRepair.API"),
    }
    token = jwt.encode(payload, JWT_SECRET, algorithm="HS256")
    return {"Authorization": f"Bearer {token}", "Content-Type": "application/json"}


'''

TESTS: dict[str, str] = {
    "TC001_get_health_returns_healthy.py": '''
def test_main():
    r = requests.get(url("/health"), timeout=TIMEOUT)
    assert r.status_code == 200, r.text
    assert r.text == "Healthy"

test_main()
''',
    "TC002_post_auth_login_returns_data_token.py": '''
def test_main():
    r = requests.post(
        url("/api/Auth/login"),
        json={"email": "admin@magiccar.com", "password": "Admin123!"},
        timeout=TIMEOUT,
    )
    assert r.status_code == 200, r.text
    body = r.json()
    assert body.get("success") is True
    assert extract_token(body).count(".") == 2

test_main()
''',
    "TC003_get_user_profile_authenticated.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/user/profile"), headers=h, timeout=TIMEOUT)
    assert r.status_code == 200, r.text

test_main()
''',
    "TC004_get_clients_system_admin.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/Clients"), headers=h, timeout=TIMEOUT)
    assert r.status_code == 200, r.text

test_main()
''',
    "TC005_customers_crud_smoke.py": '''
def test_main():
    h = admin_headers()
    suffix = uuid.uuid4().hex[:8]
    payload = {
        "identityNo": f"TS{suffix}",
        "firstName": "T",
        "lastName": f"C{suffix}",
        "email": f"c_{suffix}@example.com",
        "phoneNumber": "5550000001",
        "dateTimeOfBirth": "1990-01-01T00:00:00Z",
        "language": "tr",
    }
    c = requests.post(url("/api/Customers"), headers=h, json=payload, timeout=TIMEOUT)
    assert c.status_code in (200, 201), c.text
    cid = c.json().get("id")
    g = requests.get(url(f"/api/Customers/{cid}"), headers=h, timeout=TIMEOUT)
    assert g.status_code == 200, g.text

test_main()
''',
    "TC006_vehicles_create_for_customer.py": '''
def test_main():
    h = admin_headers()
    suffix = uuid.uuid4().hex[:8]
    cp = {
        "identityNo": f"TS{suffix}",
        "firstName": "T",
        "lastName": f"C{suffix}",
        "email": f"v_{suffix}@example.com",
        "phoneNumber": "5550000001",
        "dateTimeOfBirth": "1990-01-01T00:00:00Z",
        "language": "tr",
    }
    c = requests.post(url("/api/Customers"), headers=h, json=cp, timeout=TIMEOUT).json()
    vp = {
        "customerId": c["id"],
        "licensePlate": f"34M{suffix[:4].upper()}",
        "brand": "Toyota",
        "model": "Corolla",
        "year": 2020,
        "color": "Blue",
        "kilometers": 1000,
        "vin": f"V{suffix.upper()}",
    }
    v = requests.post(url("/api/Vehicles"), headers=h, json=vp, timeout=TIMEOUT)
    assert v.status_code in (200, 201), v.text

test_main()
''',
    "TC007_workorders_list_and_status.py": '''
def test_main():
    h = admin_headers()
    suffix = uuid.uuid4().hex[:8]
    cp = {
        "identityNo": f"TS{suffix}",
        "firstName": "T",
        "lastName": f"C{suffix}",
        "email": f"w_{suffix}@example.com",
        "phoneNumber": "5550000001",
        "dateTimeOfBirth": "1990-01-01T00:00:00Z",
        "language": "tr",
    }
    c = requests.post(url("/api/Customers"), headers=h, json=cp, timeout=TIMEOUT).json()
    vp = {
        "customerId": c["id"],
        "licensePlate": f"34W{suffix[:4].upper()}",
        "brand": "Toyota",
        "model": "Corolla",
        "year": 2020,
        "color": "Blue",
        "kilometers": 1000,
        "vin": f"V{suffix.upper()}",
    }
    v = requests.post(url("/api/Vehicles"), headers=h, json=vp, timeout=TIMEOUT).json()
    wo = requests.post(
        url("/api/WorkOrders"),
        headers=h,
        json={"customerId": c["id"], "vehicleId": v["id"], "customerComplaints": "mcp", "priority": 2},
        timeout=TIMEOUT,
    ).json()
    st = requests.put(
        url(f"/api/WorkOrders/{wo['id']}/status"),
        headers=h,
        json={"newStatus": 5, "description": "mcp"},
        timeout=TIMEOUT,
    )
    assert st.status_code == 200, st.text
    lst = requests.get(url("/api/WorkOrders"), headers=h, params={"legacyFormat": "true"}, timeout=TIMEOUT)
    assert lst.status_code == 200, lst.text

test_main()
''',
    "TC008_parts_create_and_bulk_delete.py": '''
def test_main():
    h = admin_headers()
    suffix = uuid.uuid4().hex[:8]
    pp = {
        "partCode": f"MCP-{suffix}",
        "name": f"Part {suffix}",
        "description": "mcp",
        "category": 99,
        "brandType": 2,
        "purchasePrice": 50.0,
        "salePrice": 100.0,
        "taxRate": 20,
        "minimumStockLevel": 1,
        "initialStockQuantity": 5,
        "unit": "Adet",
    }
    p = requests.post(url("/api/Parts"), headers=h, json=pp, timeout=TIMEOUT).json()
    bd = requests.post(url("/api/Parts/bulk-delete"), headers=h, json={"ids": [p["id"]]}, timeout=TIMEOUT)
    assert bd.status_code == 200, bd.text

test_main()
''',
    "TC009_appointments_list.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/Appointments"), headers=h, timeout=TIMEOUT)
    assert r.status_code == 200, r.text

test_main()
''',
    "TC010_quote_requests_list.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/quote-requests"), headers=h, timeout=TIMEOUT)
    assert r.status_code == 200, r.text

test_main()
''',
    "TC011_invoice_from_workorder.py": '''
def test_main():
    h = admin_headers()
    suffix = uuid.uuid4().hex[:8]
    cp = {
        "identityNo": f"TS{suffix}",
        "firstName": "T",
        "lastName": f"C{suffix}",
        "email": f"i_{suffix}@example.com",
        "phoneNumber": "5550000001",
        "dateTimeOfBirth": "1990-01-01T00:00:00Z",
        "language": "tr",
    }
    c = requests.post(url("/api/Customers"), headers=h, json=cp, timeout=TIMEOUT).json()
    vp = {
        "customerId": c["id"],
        "licensePlate": f"34I{suffix[:4].upper()}",
        "brand": "Toyota",
        "model": "Corolla",
        "year": 2020,
        "color": "Blue",
        "kilometers": 1000,
        "vin": f"V{suffix.upper()}",
    }
    v = requests.post(url("/api/Vehicles"), headers=h, json=vp, timeout=TIMEOUT).json()
    wo = requests.post(
        url("/api/WorkOrders"),
        headers=h,
        json={"customerId": c["id"], "vehicleId": v["id"], "customerComplaints": "mcp", "priority": 2},
        timeout=TIMEOUT,
    ).json()
    inv = requests.post(url(f"/api/Invoices/from-workorder/{wo['id']}"), headers=h, timeout=TIMEOUT)
    assert inv.status_code in (200, 201), inv.text
    body = inv.json()
    assert body.get("invoiceId") or body.get("id"), body

test_main()
''',
    "TC012_accounting_monthly_summary.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/accounting-reports/monthly-summary"), headers=h, timeout=TIMEOUT)
    assert r.status_code == 200, r.text

test_main()
''',
    "TC013_dashboard_stats.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/Dashboard/stats"), headers=h, timeout=TIMEOUT)
    assert r.status_code == 200, r.text

test_main()
''',
    "TC014_employees_create.py": '''
def test_main():
    h = admin_headers()
    suffix = uuid.uuid4().hex[:8]
    ep = {
        "firstName": "MCP",
        "lastName": f"Emp{suffix}",
        "email": f"e_{suffix}@example.com",
        "phone": "5550000002",
        "position": 5,
        "salary": 25000,
        "hireDate": "2024-01-01T00:00:00Z",
        "employmentStatus": 1,
    }
    r = requests.post(url("/api/Employees"), headers=h, json=ep, timeout=TIMEOUT)
    assert r.status_code in (200, 201), r.text

test_main()
''',
    "TC015_insurance_policies_expiring.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/insurance/policies/expiring"), headers=h, timeout=TIMEOUT)
    assert r.status_code == 200, r.text

test_main()
''',
    "TC016_notifications_my_list.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/Notifications/my-notifications"), headers=h, timeout=TIMEOUT)
    assert r.status_code == 200, r.text

test_main()
''',
    "TC017_loyalty_customer_points.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/Loyalty/customer/1"), headers=h, timeout=TIMEOUT)
    if r.status_code not in (200, 404):
        cid = first_customer_id(h)
        r = requests.get(url(f"/api/Loyalty/customer/{cid}"), headers=h, timeout=TIMEOUT)
    assert r.status_code in (200, 404), r.text

test_main()
''',
    "TC018_ai_resolve_part_prices.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(
        url("/api/AI/resolve-part-prices"),
        headers=h,
        params={"partName": "filter", "partNumber": "OF-1"},
        timeout=TIMEOUT,
    )
    assert r.status_code in (200, 400, 404), r.text

test_main()
''',
    "TC019_audit_logs_list.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/AuditLogs"), headers=h, params={"page": 1, "pageSize": 5}, timeout=TIMEOUT)
    assert r.status_code == 200, r.text

test_main()
''',
    "TC020_public_clients_list.py": '''
def test_main():
    r = requests.get(url("/api/public-clients/list"), timeout=TIMEOUT)
    assert r.status_code == 200, r.text

test_main()
''',
    "TC021_customer_portal_profile.py": '''
def test_main():
    h = register_customer_headers()
    r = requests.get(url("/api/customer-portal/profile"), headers=h, timeout=TIMEOUT)
    assert r.status_code == 200, r.text

test_main()
''',
    "TC022_subscription_current.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/Subscription/current"), headers=h, timeout=TIMEOUT)
    assert r.status_code in (200, 404), r.text

test_main()
''',
    "TC023_stock_alerts_active.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/StockAlerts/active"), headers=h, timeout=TIMEOUT)
    assert r.status_code == 200, r.text

test_main()
''',
    "TC024_auth_anonymous_401.py": '''
def test_main():
    r = requests.get(url("/api/Customers"), timeout=TIMEOUT)
    assert r.status_code == 401, r.text

test_main()
''',
    "TC025_login_invalid_password_400.py": '''
def test_main():
    r = requests.post(
        url("/api/Auth/login"),
        json={"email": ADMIN_EMAIL, "password": "WrongPassword!"},
        timeout=TIMEOUT,
    )
    assert r.status_code == 400, r.text

test_main()
''',
    "TC026_profile_anonymous_401.py": '''
def test_main():
    r = requests.get(url("/api/user/profile"), timeout=TIMEOUT)
    assert r.status_code == 401, r.text

test_main()
''',
    "TC027_permission_manager_forbidden.py": '''
def test_main():
    h = manager_headers()
    r = requests.get(url("/api/Permission/getall"), headers=h, timeout=TIMEOUT)
    assert r.status_code == 403, r.text

test_main()
''',
    "TC028_impersonate_manager_forbidden.py": '''
def test_main():
    h = manager_headers()
    r = requests.post(
        url("/api/Auth/impersonate"),
        headers=h,
        json={"targetUserId": 1},
        timeout=TIMEOUT,
    )
    assert r.status_code == 403, r.text

test_main()
''',
    "TC029_workorders_mobile_list_contract.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/WorkOrders"), headers=h, params={"page": 1, "pageSize": 5}, timeout=TIMEOUT)
    assert r.status_code == 200, r.text
    body = r.json()
    assert body.get("success") is True
    assert "data" in body

test_main()
''',
    "TC030_workorders_export.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/WorkOrders/export"), headers=h, timeout=TIMEOUT)
    assert r.status_code == 200, r.text

test_main()
''',
    "TC031_dashboard_charts_and_revenue.py": '''
def test_main():
    h = admin_headers()
    for path in (
        "/api/Dashboard/income-expense-chart",
        "/api/Dashboard/weekly-revenue",
        "/api/Dashboard/today-revenue",
    ):
        r = requests.get(url(path), headers=h, timeout=TIMEOUT)
        assert r.status_code == 200, f"{path}: {r.text}"

test_main()
''',
    "TC032_customers_invalid_id_404.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/Customers/99999999"), headers=h, timeout=TIMEOUT)
    assert r.status_code == 404, r.text

test_main()
''',
    "TC033_parts_low_stock_and_movements.py": '''
def test_main():
    h = admin_headers()
    low = requests.get(url("/api/Parts/low-stock"), headers=h, timeout=TIMEOUT)
    assert low.status_code == 200, low.text
    mov = requests.get(url("/api/StockMovements"), headers=h, timeout=TIMEOUT)
    assert mov.status_code == 200, mov.text

test_main()
''',
    "TC034_part_suppliers_and_auto_orders.py": '''
def test_main():
    h = admin_headers()
    s = requests.get(url("/api/PartSuppliers"), headers=h, timeout=TIMEOUT)
    assert s.status_code == 200, s.text
    a = requests.get(url("/api/AutoOrders/pending"), headers=h, timeout=TIMEOUT)
    assert a.status_code == 200, a.text

test_main()
''',
    "TC035_accounting_income_expense_reports.py": '''
def test_main():
    h = admin_headers()
    for path in (
        "/api/accounting-reports/income",
        "/api/accounting-reports/expense",
        "/api/accounting-reports/cash-flow",
    ):
        r = requests.get(url(path), headers=h, timeout=TIMEOUT)
        assert r.status_code == 200, f"{path}: {r.text}"

test_main()
''',
    "TC036_sync_data_empty_payload.py": '''
def test_main():
    h = admin_headers()
    r = requests.post(url("/api/Sync/data"), headers=h, json={"changes": []}, timeout=TIMEOUT)
    assert r.status_code in (200, 400), r.text

test_main()
''',
    "TC037_client_portal_profile_401.py": '''
def test_main():
    r = requests.get(url("/api/client-portal/profile"), timeout=TIMEOUT)
    assert r.status_code == 401, r.text

test_main()
''',
    "TC038_customer_cannot_list_shop_customers.py": '''
def test_main():
    h = register_customer_headers()
    r = requests.get(url("/api/Customers"), headers=h, timeout=TIMEOUT)
    assert r.status_code == 403, r.text

test_main()
''',
    "TC039_vehicles_anonymous_401.py": '''
def test_main():
    r = requests.get(url("/api/Vehicles"), timeout=TIMEOUT)
    assert r.status_code == 401, r.text

test_main()
''',
    "TC040_invoices_list.py": '''
def test_main():
    h = admin_headers()
    r = requests.get(url("/api/Invoices"), headers=h, timeout=TIMEOUT)
    assert r.status_code == 200, r.text

test_main()
''',
}


def main() -> None:
    root = Path(__file__).resolve().parent
    for name, body in TESTS.items():
        (root / name).write_text(HEADER + body, encoding="utf-8")
    print(f"wrote {len(TESTS)} TC files")


if __name__ == "__main__":
    main()
