import uuid

from api_support import (
    USER_TYPE_CUSTOMER,
    assert_forbidden,
    assert_unauthorized,
    login_as_super_admin,
    new_employee_payload,
    request,
    assert_status,
    assert_created,
)


def test_employees_crud_smoke():
    _, _, headers = login_as_super_admin()
    suffix = uuid.uuid4().hex[:8]
    create = request("POST", "/api/Employees", headers=headers, json_body=new_employee_payload(suffix))
    body = assert_created(create, "create employee")
    emp_id = body.get("id") or body.get("employeeId")
    assert emp_id, body

    listing = request("GET", "/api/Employees", headers=headers)
    assert_status(listing, 200, "list employees")


def test_employees_auth():
    assert_unauthorized("/api/Employees")
    assert_forbidden("/api/Employees", USER_TYPE_CUSTOMER)


test_employees_crud_smoke()
test_employees_auth()
