from api_support import (
    USER_TYPE_CUSTOMER,
    assert_forbidden,
    assert_unauthorized,
    create_customer,
    create_vehicle,
    create_work_order,
    login_as_super_admin,
    request,
    assert_status,
)


def test_invoices_list_and_from_workorder():
    _, _, headers = login_as_super_admin()
    listing = request("GET", "/api/Invoices", headers=headers)
    assert_status(listing, 200, "invoices list")

    customer = create_customer(headers)
    vehicle = create_vehicle(headers, customer["id"])
    work_order = create_work_order(headers, customer["id"], vehicle["id"])
    gen = request(
        "POST",
        f"/api/Invoices/from-workorder/{work_order['id']}",
        headers=headers,
    )
    assert_status(gen, (200, 201), "generate invoice")
    body = gen.json()
    invoice_id = body.get("invoiceId") or body.get("id")
    assert invoice_id, f"invoice id missing: {body}"


def test_payments_list():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/Payments", headers=headers)
    assert_status(response, (200, 404), "payments")


def test_invoices_auth():
    assert_unauthorized("/api/Invoices")
    assert_forbidden("/api/Invoices", USER_TYPE_CUSTOMER)


test_invoices_list_and_from_workorder()
test_payments_list()
test_invoices_auth()
