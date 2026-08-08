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


def test_workorders_list_mobile_and_legacy():
    _, _, headers = login_as_super_admin()
    mobile = request("GET", "/api/WorkOrders", headers=headers, params={"page": 1, "pageSize": 5})
    assert_status(mobile, 200, "mobile list")
    body = mobile.json()
    assert body.get("success") is True
    assert "data" in body

    legacy = request(
        "GET",
        "/api/WorkOrders",
        headers=headers,
        params={"legacyFormat": "true"},
    )
    assert_status(legacy, 200, "legacy list")


def test_workorders_crud_and_status():
    _, _, headers = login_as_super_admin()
    customer = create_customer(headers)
    vehicle = create_vehicle(headers, customer["id"])
    work_order = create_work_order(headers, customer["id"], vehicle["id"])
    wo_id = work_order["id"]

    get_legacy = request(
        "GET",
        f"/api/WorkOrders/{wo_id}",
        headers=headers,
        params={"legacyFormat": "true"},
    )
    assert_status(get_legacy, 200, "get by id legacy")

    status_resp = request(
        "PUT",
        f"/api/WorkOrders/{wo_id}/status",
        headers=headers,
        json_body={"newStatus": 5, "description": "Module test status"},
    )
    assert_status(status_resp, 200, "update status")

    active = request("GET", "/api/WorkOrders/active", headers=headers)
    assert_status(active, 200, "active work orders")

    timeline = request("GET", f"/api/WorkOrders/{wo_id}/timeline", headers=headers)
    assert_status(timeline, 200, "timeline")

    pending = request("GET", "/api/WorkOrders/pending-approvals", headers=headers)
    assert_status(pending, 200, "pending approvals")

    export_resp = request("GET", "/api/WorkOrders/export", headers=headers)
    assert_status(export_resp, 200, "export")


def test_workorders_pdf_and_qr():
    _, _, headers = login_as_super_admin()
    customer = create_customer(headers)
    vehicle = create_vehicle(headers, customer["id"])
    work_order = create_work_order(headers, customer["id"], vehicle["id"])
    wo_id = work_order["id"]

    pdf = request("GET", f"/api/WorkOrders/{wo_id}/pdf", headers=headers)
    assert_status(pdf, (200, 404), "pdf")

    qr = request("GET", f"/api/WorkOrders/{wo_id}/qr-code", headers=headers)
    assert_status(qr, (200, 404), "qr-code")


def test_workorders_auth():
    assert_unauthorized("/api/WorkOrders")
    assert_forbidden("/api/WorkOrders", USER_TYPE_CUSTOMER)


test_workorders_list_mobile_and_legacy()
test_workorders_crud_and_status()
test_workorders_pdf_and_qr()
test_workorders_auth()
