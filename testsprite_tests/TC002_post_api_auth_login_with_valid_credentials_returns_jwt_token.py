import requests

from api_support import (
    ADMIN_EMAIL,
    ADMIN_PASSWORD,
    DEFAULT_TIMEOUT,
    _url,
    assert_login_success,
    extract_token,
    extract_user,
)


def test_post_api_auth_login_with_valid_credentials_returns_jwt_token():
    response = requests.post(
        _url("/api/Auth/login"),
        json={"email": ADMIN_EMAIL, "password": ADMIN_PASSWORD},
        timeout=DEFAULT_TIMEOUT,
    )
    assert response.status_code == 200, response.text
    body = response.json()
    assert_login_success(body)
    token = extract_token(body)
    user = extract_user(body)
    assert token.count(".") == 2
    assert user.get("email") == ADMIN_EMAIL


test_post_api_auth_login_with_valid_credentials_returns_jwt_token()
