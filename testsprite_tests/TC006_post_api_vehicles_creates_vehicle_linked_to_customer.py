import requests

from api_support import (
    DEFAULT_TIMEOUT,
    _url,
    create_customer,
    login_as_super_admin,
    new_vehicle_payload,
)


def test_post_api_vehicles_creates_vehicle_linked_to_customer():
    _, _, headers = login_as_super_admin()
    customer = create_customer(headers)
    payload = new_vehicle_payload(customer["id"])

    response = requests.post(
        _url("/api/Vehicles"),
        headers=headers,
        json=payload,
        timeout=DEFAULT_TIMEOUT,
    )
    assert response.status_code in (200, 201), response.text
    vehicle = response.json()
    assert vehicle.get("id")
    assert vehicle.get("customerId") == customer["id"]
    assert vehicle.get("brand") == payload["brand"]
    assert vehicle.get("model") == payload["model"]


test_post_api_vehicles_creates_vehicle_linked_to_customer()
