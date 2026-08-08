"""Shared helpers for TestSprite backend API tests (IDataResult + domain contracts)."""

from __future__ import annotations

import os
import uuid
from datetime import datetime, timedelta, timezone
from typing import Any

import jwt
import requests

BASE_URL = os.environ.get("TEST_API_BASE_URL", "http://localhost:5169").rstrip("/")
DEFAULT_TIMEOUT = int(os.environ.get("TEST_API_TIMEOUT", "60"))
CREATED_STATUS_CODES = (200, 201)
JWT_SECRET = os.environ.get(
    "TokenOptions__SecurityKey",
    "YourSuperSecretKeyForJwtTokenSigning_MustBeAtLeast32Chars!",
)
JWT_ISSUER = os.environ.get("TokenOptions__Issuer", "MagicCarRepair")
JWT_AUDIENCE = os.environ.get("TokenOptions__Audience", "MagicCarRepair.API")

ADMIN_EMAIL = "admin@magiccar.com"
ADMIN_PASSWORD = "Admin123!"

# UserType: 1=SystemAdmin, 2=Manager, 3=Employee, 4=Customer
USER_TYPE_SYSTEM_ADMIN = 1
USER_TYPE_MANAGER = 2
USER_TYPE_EMPLOYEE = 3
USER_TYPE_CUSTOMER = 4


def _url(path: str) -> str:
    if not path.startswith("/"):
        path = "/" + path
    return f"{BASE_URL}{path}"


def extract_token(login_body: dict[str, Any]) -> str:
    data = login_body.get("data") or {}
    token = data.get("token")
    if token:
        return token
    raise AssertionError(f"Login response missing data.token: {login_body}")


def extract_user(login_body: dict[str, Any]) -> dict[str, Any]:
    data = login_body.get("data") or {}
    user = data.get("user")
    if isinstance(user, dict):
        return user
    raise AssertionError(f"Login response missing data.user: {login_body}")


def assert_login_success(body: dict[str, Any]) -> None:
    assert body.get("success") is True, f"Expected success=true, got: {body}"


def assert_created(response: requests.Response, context: str) -> dict[str, Any]:
    assert response.status_code in CREATED_STATUS_CODES, f"{context}: {response.text}"
    return response.json()


def jwt_for_user_type(
    user_type: int,
    client_id: int = 1,
    user_id: int = 99,
) -> str:
    """Build JWT aligned with JwtTestTokenFactory / HasUserType policy checks."""
    now = datetime.now(timezone.utc)
    payload = {
        "sub": str(user_id),
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": str(user_id),
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress": f"test-{user_type}@test.local",
        "UserType": str(user_type),
        "ClientId": str(client_id),
        "Language": "tr",
        "exp": now + timedelta(hours=1),
        "iss": JWT_ISSUER,
        "aud": JWT_AUDIENCE,
    }
    return jwt.encode(payload, JWT_SECRET, algorithm="HS256")


def auth_headers_for_user_type(user_type: int, client_id: int = 1, user_id: int = 99) -> dict[str, str]:
    token = jwt_for_user_type(user_type, client_id, user_id)
    return {"Authorization": f"Bearer {token}", "Content-Type": "application/json"}


def request(
    method: str,
    path: str,
    *,
    headers: dict[str, str] | None = None,
    json_body: Any = None,
    params: dict[str, Any] | None = None,
    timeout: int = DEFAULT_TIMEOUT,
    session: requests.Session | None = None,
) -> requests.Response:
    client = session or requests.Session()
    return client.request(
        method.upper(),
        _url(path),
        headers=headers,
        json=json_body,
        params=params,
        timeout=timeout,
    )


def assert_status(
    response: requests.Response,
    expected: int | tuple[int, ...],
    context: str,
) -> None:
    allowed = (expected,) if isinstance(expected, int) else expected
    assert response.status_code in allowed, f"{context}: {response.status_code} {response.text[:500]}"


def assert_unauthorized(path: str, method: str = "GET", json_body: Any = None) -> None:
    response = request(method, path, json_body=json_body)
    assert_status(response, 401, f"Anonymous {method} {path}")


def assert_forbidden(
    path: str,
    user_type: int,
    method: str = "GET",
    json_body: Any = None,
) -> None:
    headers = auth_headers_for_user_type(user_type)
    response = request(method, path, headers=headers, json_body=json_body)
    assert_status(response, 403, f"UserType {user_type} {method} {path}")


def login_as_super_admin(
    session: requests.Session | None = None,
    timeout: int = DEFAULT_TIMEOUT,
) -> tuple[str, dict[str, Any], dict[str, str]]:
    client = session or requests.Session()
    response = client.post(
        _url("/api/Auth/login"),
        json={"email": ADMIN_EMAIL, "password": ADMIN_PASSWORD},
        timeout=timeout,
    )
    assert response.status_code == 200, f"Login failed ({response.status_code}): {response.text}"
    body = response.json()
    assert_login_success(body)
    token = extract_token(body)
    user = extract_user(body)
    assert token.count(".") == 2, "JWT token format invalid"
    headers = {"Authorization": f"Bearer {token}", "Content-Type": "application/json"}
    return token, user, headers


def login_as_shop_staff(
    session: requests.Session | None = None,
    timeout: int = DEFAULT_TIMEOUT,
) -> tuple[str, dict[str, str]]:
    token, _, headers = login_as_super_admin(session, timeout)
    return token, headers


def new_part_payload(suffix: str | None = None) -> dict[str, Any]:
    suffix = suffix or uuid.uuid4().hex[:8]
    return {
        "partCode": f"TS-{suffix}",
        "name": f"TestSprite Part {suffix}",
        "description": "Created by TestSprite",
        "category": 99,
        "brandType": 2,
        "purchasePrice": 50.0,
        "salePrice": 100.0,
        "taxRate": 20,
        "minimumStockLevel": 1,
        "initialStockQuantity": 5,
        "unit": "Adet",
    }


def new_customer_payload(suffix: str | None = None) -> dict[str, Any]:
    suffix = suffix or uuid.uuid4().hex[:8]
    return {
        "identityNo": f"TS{suffix[:10]}",
        "firstName": "Test",
        "lastName": f"Customer{suffix}",
        "email": f"testsprite_{suffix}@example.com",
        "phoneNumber": "5550000001",
        "dateTimeOfBirth": "1990-01-01T00:00:00Z",
        "language": "tr",
    }


def new_vehicle_payload(customer_id: int, suffix: str | None = None) -> dict[str, Any]:
    suffix = suffix or uuid.uuid4().hex[:8]
    return {
        "customerId": customer_id,
        "licensePlate": f"34TS{suffix[:4].upper()}",
        "brand": "Toyota",
        "model": "Corolla",
        "year": 2020,
        "color": "Blue",
        "kilometers": 10000,
        "vin": f"VIN{suffix.upper()}",
    }


def new_work_order_payload(customer_id: int, vehicle_id: int) -> dict[str, Any]:
    return {
        "customerId": customer_id,
        "vehicleId": vehicle_id,
        "customerComplaints": "TestSprite work order",
        "priority": 2,
    }


def new_employee_payload(suffix: str | None = None) -> dict[str, Any]:
    suffix = suffix or uuid.uuid4().hex[:8]
    return {
        "firstName": "Test",
        "lastName": f"Employee{suffix}",
        "email": f"testsprite_emp_{suffix}@example.com",
        "phone": "5550000002",
        "position": 5,
        "salary": 25000,
        "hireDate": datetime.now(timezone.utc).isoformat(),
        "employmentStatus": 1,
    }


def create_customer(
    headers: dict[str, str],
    session: requests.Session | None = None,
    timeout: int = DEFAULT_TIMEOUT,
) -> dict[str, Any]:
    client = session or requests.Session()
    response = client.post(
        _url("/api/Customers"),
        headers=headers,
        json=new_customer_payload(),
        timeout=timeout,
    )
    body = assert_created(response, "Create customer")
    assert body.get("id"), f"Customer id missing: {body}"
    return body


def create_vehicle(
    headers: dict[str, str],
    customer_id: int,
    session: requests.Session | None = None,
    timeout: int = DEFAULT_TIMEOUT,
) -> dict[str, Any]:
    client = session or requests.Session()
    response = client.post(
        _url("/api/Vehicles"),
        headers=headers,
        json=new_vehicle_payload(customer_id),
        timeout=timeout,
    )
    body = assert_created(response, "Create vehicle")
    assert body.get("id"), f"Vehicle id missing: {body}"
    return body


def create_work_order(
    headers: dict[str, str],
    customer_id: int,
    vehicle_id: int,
    session: requests.Session | None = None,
    timeout: int = DEFAULT_TIMEOUT,
) -> dict[str, Any]:
    client = session or requests.Session()
    response = client.post(
        _url("/api/WorkOrders"),
        headers=headers,
        json=new_work_order_payload(customer_id, vehicle_id),
        timeout=timeout,
    )
    body = assert_created(response, "Create work order")
    assert body.get("id"), f"Work order id missing: {body}"
    return body


def register_customer(
    suffix: str | None = None,
    password: str = "TestSprite1!",
    session: requests.Session | None = None,
    timeout: int = DEFAULT_TIMEOUT,
) -> dict[str, Any]:
    """Register a real customer user (required for customer-portal handlers that load User from DB)."""
    suffix = suffix or uuid.uuid4().hex[:8]
    email = f"portal_{suffix}@example.com"
    payload = {
        "firstName": "Portal",
        "lastName": f"Customer{suffix}",
        "email": email,
        "password": password,
        "confirmPassword": password,
        "phoneNumber": "5550000099",
        "language": "tr",
        "identityNo": f"TS{suffix[:10]}",
    }
    client = session or requests.Session()
    response = client.post(
        _url("/api/Auth/register-customer"),
        json=payload,
        timeout=timeout,
    )
    assert response.status_code in CREATED_STATUS_CODES, f"Register customer: {response.text}"
    body = response.json()
    assert body.get("success") is True, body
    return {"email": email, "password": password, "data": body.get("data") or {}}


def login_as_customer(
    email: str,
    password: str,
    session: requests.Session | None = None,
    timeout: int = DEFAULT_TIMEOUT,
) -> tuple[str, dict[str, str]]:
    client = session or requests.Session()
    response = client.post(
        _url("/api/Auth/login"),
        json={"email": email, "password": password},
        timeout=timeout,
    )
    assert response.status_code == 200, f"Customer login failed: {response.text}"
    body = response.json()
    assert_login_success(body)
    token = extract_token(body)
    headers = {"Authorization": f"Bearer {token}", "Content-Type": "application/json"}
    return token, headers


def register_and_login_customer(
    suffix: str | None = None,
    session: requests.Session | None = None,
) -> tuple[str, dict[str, str], dict[str, Any]]:
    creds = register_customer(suffix=suffix, session=session)
    token, headers = login_as_customer(creds["email"], creds["password"], session=session)
    return token, headers, creds


def create_part(
    headers: dict[str, str],
    session: requests.Session | None = None,
    timeout: int = DEFAULT_TIMEOUT,
) -> dict[str, Any]:
    client = session or requests.Session()
    response = client.post(
        _url("/api/Parts"),
        headers=headers,
        json=new_part_payload(),
        timeout=timeout,
    )
    body = assert_created(response, "Create part")
    assert body.get("id"), f"Part id missing: {body}"
    return body
