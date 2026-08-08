from api_support import (
    USER_TYPE_CUSTOMER,
    assert_forbidden,
    assert_unauthorized,
    login_as_super_admin,
    request,
    assert_status,
)


def test_appointments_list():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/Appointments", headers=headers)
    assert_status(response, 200, "appointments")


def test_reminders_list():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/Reminders", headers=headers)
    assert_status(response, (200, 404), "reminders")


def test_appointments_auth():
    assert_unauthorized("/api/Appointments")
    assert_forbidden("/api/Appointments", USER_TYPE_CUSTOMER)


test_appointments_list()
test_reminders_list()
test_appointments_auth()
