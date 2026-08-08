from api_support import login_as_super_admin, request, assert_status, assert_unauthorized


def test_notifications_list():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/Notifications/my-notifications", headers=headers)
    assert_status(response, 200, "notifications")


def test_notifications_auth():
    assert_unauthorized("/api/Notifications/my-notifications")


test_notifications_list()
test_notifications_auth()
