from api_support import (
    USER_TYPE_CUSTOMER,
    assert_forbidden,
    assert_unauthorized,
    create_customer,
    create_vehicle,
    login_as_super_admin,
    new_vehicle_payload,
    request,
    assert_status,
)


def test_vehicles_crud_smoke():
    _, _, headers = login_as_super_admin()
    customer = create_customer(headers)
    vehicle = create_vehicle(headers, customer["id"])
    assert vehicle.get("customerId") == customer["id"]

    get_one = request("GET", f"/api/Vehicles/{vehicle['id']}", headers=headers)
    assert_status(get_one, 200, "get vehicle")

    by_customer = request("GET", f"/api/Vehicles/customer/{customer['id']}", headers=headers)
    assert_status(by_customer, 200, "vehicles by customer")


def test_vehicles_auth():
    assert_unauthorized("/api/Vehicles")
    assert_forbidden("/api/Vehicles", USER_TYPE_CUSTOMER, method="POST", json_body=new_vehicle_payload(1))


test_vehicles_crud_smoke()
test_vehicles_auth()
