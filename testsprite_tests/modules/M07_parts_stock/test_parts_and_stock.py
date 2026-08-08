from api_support import (
    USER_TYPE_CUSTOMER,
    assert_forbidden,
    assert_unauthorized,
    create_part,
    login_as_super_admin,
    request,
    assert_status,
)


def test_parts_smoke():
    _, _, headers = login_as_super_admin()
    part = create_part(headers)
    get_one = request("GET", f"/api/Parts/{part['id']}", headers=headers)
    assert_status(get_one, 200, "get part")

    list_resp = request("GET", "/api/Parts", headers=headers, params={"pageNumber": 1, "pageSize": 5})
    assert_status(list_resp, 200, "list parts")

    low = request("GET", "/api/Parts/low-stock", headers=headers)
    assert_status(low, 200, "low stock")

    bulk = request(
        "POST",
        "/api/Parts/bulk-delete",
        headers=headers,
        json_body={"ids": [part["id"]]},
    )
    assert_status(bulk, 200, "bulk delete")
    assert bulk.json().get("success") is True


def test_part_suppliers_list():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/PartSuppliers", headers=headers)
    assert_status(response, 200, "part suppliers")


def test_stock_movements_list():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/StockMovements", headers=headers)
    assert_status(response, 200, "stock movements")


def test_stock_alerts_list():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/StockAlerts/active", headers=headers)
    assert_status(response, 200, "stock alerts")


def test_auto_orders_pending():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/AutoOrders/pending", headers=headers)
    assert_status(response, 200, "auto orders pending")


def test_parts_auth():
    assert_unauthorized("/api/Parts")
    assert_forbidden("/api/Parts", USER_TYPE_CUSTOMER)


test_parts_smoke()
test_part_suppliers_list()
test_stock_movements_list()
test_stock_alerts_list()
test_auto_orders_pending()
test_parts_auth()
