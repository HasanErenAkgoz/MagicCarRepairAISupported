import requests

from api_support import DEFAULT_TIMEOUT, _url


def test_get_health_returns_healthy_status_without_authentication():
    response = requests.get(_url("/health"), timeout=DEFAULT_TIMEOUT)
    assert response.status_code == 200, response.text
    assert response.text == "Healthy"


test_get_health_returns_healthy_status_without_authentication()
