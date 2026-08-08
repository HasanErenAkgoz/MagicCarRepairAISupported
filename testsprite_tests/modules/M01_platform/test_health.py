from api_support import DEFAULT_TIMEOUT, _url, request, assert_status


def test_health_returns_healthy_without_authentication():
    response = request("GET", "/health", timeout=DEFAULT_TIMEOUT)
    assert_status(response, 200, "GET /health")
    assert response.text == "Healthy"


test_health_returns_healthy_without_authentication()
