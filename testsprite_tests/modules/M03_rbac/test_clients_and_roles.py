from api_support import (
    USER_TYPE_CUSTOMER,
    USER_TYPE_MANAGER,
    assert_forbidden,
    assert_unauthorized,
    login_as_super_admin,
    request,
    assert_status,
)


def test_clients_list_as_admin():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/Clients", headers=headers)
    assert_status(response, 200, "GET Clients")


def test_clients_anonymous_401():
    assert_unauthorized("/api/Clients")


def test_clients_customer_403():
    assert_forbidden("/api/Clients", USER_TYPE_CUSTOMER)


def test_roles_list_as_shop():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/Role", headers=headers)
    assert_status(response, 200, "GET Role")


def test_permissions_system_admin_only():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/Permission/getall", headers=headers)
    assert_status(response, 200, "GET Permission")


def test_permissions_manager_forbidden():
    assert_forbidden("/api/Permission/getall", USER_TYPE_MANAGER)


test_clients_list_as_admin()
test_clients_anonymous_401()
test_clients_customer_403()
test_roles_list_as_shop()
test_permissions_system_admin_only()
test_permissions_manager_forbidden()
