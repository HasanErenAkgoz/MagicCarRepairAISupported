from api_support import (
    USER_TYPE_MANAGER,
    assert_forbidden,
    assert_unauthorized,
    register_and_login_customer,
    request,
    assert_status,
)


def test_public_clients_list_anonymous():
    response = request("GET", "/api/public-clients/list")
    assert_status(response, 200, "public clients list")


def test_customer_portal_profile_with_registered_customer():
    """Synthetic JWT user ids are not in DB; handlers throw UnauthorizedAccessException."""
    _, headers, _ = register_and_login_customer()
    response = request("GET", "/api/customer-portal/profile", headers=headers)
    assert_status(response, 200, "customer portal profile")
    body = response.json()
    assert body.get("email") or body.get("firstName"), f"Expected profile fields: {body}"


def test_customer_portal_manager_forbidden():
    assert_forbidden("/api/customer-portal/profile", USER_TYPE_MANAGER)


def test_client_portal_anonymous():
    assert_unauthorized("/api/client-portal/profile")


test_public_clients_list_anonymous()
test_customer_portal_profile_with_registered_customer()
test_customer_portal_manager_forbidden()
test_client_portal_anonymous()
