from api_support import login_as_super_admin, request, assert_status, assert_unauthorized


def test_loyalty_endpoints():
    _, _, headers = login_as_super_admin()
    loyalty = request("GET", "/api/Loyalty/customer/1", headers=headers)
    assert_status(loyalty, (200, 404), "loyalty by customer")
    for path in ("/api/Rewards", "/api/Ratings"):
        response = request("GET", path, headers=headers)
        assert_status(response, (200, 404), path)


def test_loyalty_auth():
    assert_unauthorized("/api/Loyalty/customer/1")


test_loyalty_endpoints()
test_loyalty_auth()
