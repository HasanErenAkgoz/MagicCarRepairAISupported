import requests

from api_support import DEFAULT_TIMEOUT, _url, login_as_super_admin


def test_get_api_customers_with_shop_role_returns_customer_list():
    _, _, headers = login_as_super_admin()
    response = requests.get(_url("/api/Customers"), headers=headers, timeout=DEFAULT_TIMEOUT)
    assert response.status_code == 200, response.text
    data = response.json()
    assert isinstance(data, list), f"Expected JSON array, got {type(data)}"


test_get_api_customers_with_shop_role_returns_customer_list()
