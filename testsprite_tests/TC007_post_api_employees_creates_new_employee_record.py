import requests

from api_support import DEFAULT_TIMEOUT, _url, login_as_super_admin, new_employee_payload


def test_post_api_employees_creates_new_employee_record():
    _, _, headers = login_as_super_admin()
    payload = new_employee_payload()

    response = requests.post(
        _url("/api/Employees"),
        headers=headers,
        json=payload,
        timeout=DEFAULT_TIMEOUT,
    )
    assert response.status_code in (200, 201), response.text
    employee = response.json()
    assert employee.get("id")
    assert employee.get("firstName") == payload["firstName"]
    assert employee.get("lastName") == payload["lastName"]
    assert employee.get("email") == payload["email"]


test_post_api_employees_creates_new_employee_record()
