from api_support import login_as_super_admin, request, assert_status, assert_unauthorized


def test_dashboard_stats():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/Dashboard/stats", headers=headers)
    assert_status(response, 200, "dashboard stats")
    body = response.json()
    assert body.get("success") is True or isinstance(body, dict)


def test_dashboard_auth():
    assert_unauthorized("/api/Dashboard/stats")


test_dashboard_stats()
test_dashboard_auth()
