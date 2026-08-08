import os
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



def test_main():
    r = requests.get(url("/api/Customers"), timeout=TIMEOUT)
    assert r.status_code == 401, r.text

test_main()
