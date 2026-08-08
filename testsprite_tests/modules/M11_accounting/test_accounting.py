from api_support import login_as_super_admin, request, assert_status, assert_unauthorized


def test_accounting_endpoints():
    _, _, headers = login_as_super_admin()
    for path in (
        "/api/Incomes",
        "/api/Expenses",
        "/api/Taxes",
        "/api/SalaryPayments",
        "/api/accounting-reports/monthly-summary",
    ):
        response = request("GET", path, headers=headers)
        assert_status(response, (200, 404), path)


def test_accounting_auth():
    assert_unauthorized("/api/Incomes")


test_accounting_endpoints()
test_accounting_auth()
