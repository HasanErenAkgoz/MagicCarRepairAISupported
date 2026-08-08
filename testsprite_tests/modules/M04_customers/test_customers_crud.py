import uuid

from api_support import (
    USER_TYPE_CUSTOMER,
    assert_created,
    assert_forbidden,
    assert_unauthorized,
    login_as_super_admin,
    new_customer_payload,
    request,
    assert_status,
)


def test_customers_crud_smoke():
    _, _, headers = login_as_super_admin()
    suffix = uuid.uuid4().hex[:8]
    create = request("POST", "/api/Customers", headers=headers, json_body=new_customer_payload(suffix))
    body = assert_created(create, "create customer")
    customer_id = body["id"]

    get_one = request("GET", f"/api/Customers/{customer_id}", headers=headers)
    assert_status(get_one, 200, "get customer")

    listing = request("GET", "/api/Customers", headers=headers)
    assert_status(listing, 200, "list customers")
    assert isinstance(listing.json(), list)


def test_customers_auth():
    assert_unauthorized("/api/Customers")
    assert_forbidden("/api/Customers", USER_TYPE_CUSTOMER)


test_customers_crud_smoke()
test_customers_auth()
