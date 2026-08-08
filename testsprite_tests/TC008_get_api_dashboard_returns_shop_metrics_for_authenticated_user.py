import requests

from api_support import DEFAULT_TIMEOUT, _url, login_as_super_admin


def test_get_api_dashboard_returns_shop_metrics_for_authenticated_user():
    _, _, headers = login_as_super_admin()
    response = requests.get(
        _url("/api/Dashboard/stats"),
        headers=headers,
        timeout=DEFAULT_TIMEOUT,
    )
    assert response.status_code == 200, response.text
    data = response.json()
    assert isinstance(data, dict) and len(data) > 0


test_get_api_dashboard_returns_shop_metrics_for_authenticated_user()
