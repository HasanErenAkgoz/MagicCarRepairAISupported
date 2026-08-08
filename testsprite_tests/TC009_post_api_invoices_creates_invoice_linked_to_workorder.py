import requests

from api_support import (
    DEFAULT_TIMEOUT,
    _url,
    create_customer,
    create_vehicle,
    create_work_order,
    login_as_super_admin,
)


def test_post_api_invoices_creates_invoice_linked_to_workorder():
    _, _, headers = login_as_super_admin()
    customer = create_customer(headers)
    vehicle = create_vehicle(headers, customer["id"])
    work_order = create_work_order(headers, customer["id"], vehicle["id"])

    response = requests.post(
        _url(f"/api/Invoices/from-workorder/{work_order['id']}"),
        headers=headers,
        json={},
        timeout=DEFAULT_TIMEOUT,
    )
    assert response.status_code in (200, 201), response.text
    invoice = response.json()
    assert invoice.get("invoiceId") is not None
    assert invoice.get("invoiceId") > 0, f"Invalid invoice id: {invoice}"
    assert invoice.get("workOrderId") == work_order["id"]
    assert invoice.get("invoiceNumber")


test_post_api_invoices_creates_invoice_linked_to_workorder()
