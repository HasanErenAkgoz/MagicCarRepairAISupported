from api_support import login_as_super_admin, request, assert_status, assert_unauthorized


def test_insurance_list():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/insurance/policies/expiring", headers=headers)
    assert_status(response, (200, 404), "insurance policies")


def test_insurance_auth():
    assert_unauthorized("/api/insurance/policies/expiring")


test_insurance_list()
test_insurance_auth()
