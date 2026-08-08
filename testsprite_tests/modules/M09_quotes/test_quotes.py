from api_support import login_as_super_admin, request, assert_status, assert_unauthorized


def test_quote_requests_list():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/quote-requests", headers=headers)
    assert_status(response, (200, 404), "quote requests")


def test_quote_requests_auth():
    assert_unauthorized("/api/quote-requests")


test_quote_requests_list()
test_quote_requests_auth()
