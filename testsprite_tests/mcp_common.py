"""Shared helpers for TestSprite cloud TC scripts (imported via same directory)."""
import os
import uuid
import requests

BASE_URL = os.environ.get("TEST_API_BASE_URL", "http://localhost:5169").rstrip("/")
TIMEOUT = int(os.environ.get("TEST_API_TIMEOUT", "60"))
ADMIN_EMAIL = "admin@magiccar.com"
ADMIN_PASSWORD = "Admin123!"


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
