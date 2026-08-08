from api_support import login_as_super_admin, request, assert_status, assert_unauthorized


def test_ai_resolve_part_prices_mock():
    _, _, headers = login_as_super_admin()
    response = request(
        "GET",
        "/api/AI/resolve-part-prices",
        headers=headers,
        params={"partName": "Oil filter", "partNumber": "OF-1"},
    )
    assert_status(response, (200, 400, 404), "AI resolve prices")


def test_ai_auth():
    assert_unauthorized("/api/AI/resolve-part-prices")


test_ai_resolve_part_prices_mock()
test_ai_auth()
