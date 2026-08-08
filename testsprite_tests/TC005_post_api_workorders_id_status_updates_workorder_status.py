import requests

from api_support import (
    DEFAULT_TIMEOUT,
    _url,
    create_customer,
    create_vehicle,
    create_work_order,
    login_as_super_admin,
)


def test_post_api_workorders_id_status_updates_workorder_status():
    _, _, headers = login_as_super_admin()
    customer = create_customer(headers)
    vehicle = create_vehicle(headers, customer["id"])
    work_order = create_work_order(headers, customer["id"], vehicle["id"])
    work_order_id = work_order["id"]

    update_response = requests.put(
        _url(f"/api/WorkOrders/{work_order_id}/status"),
        headers=headers,
        json={"newStatus": 5, "description": "TestSprite status update"},
        timeout=DEFAULT_TIMEOUT,
    )
    assert update_response.status_code == 200, update_response.text
    updated = update_response.json()
    assert updated.get("workOrderId") == work_order_id
    assert updated.get("newStatus") in (5, "InProgress")


test_post_api_workorders_id_status_updates_workorder_status()
