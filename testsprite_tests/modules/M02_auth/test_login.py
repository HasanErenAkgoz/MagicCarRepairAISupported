from api_support import (
    ADMIN_EMAIL,
    ADMIN_PASSWORD,
    assert_login_success,
    extract_token,
    extract_user,
    request,
    assert_status,
)


def test_login_success_returns_data_token():
    response = request(
        "POST",
        "/api/Auth/login",
        json_body={"email": ADMIN_EMAIL, "password": ADMIN_PASSWORD},
    )
    assert_status(response, 200, "login")
    body = response.json()
    assert_login_success(body)
    assert extract_token(body).count(".") == 2
    assert extract_user(body).get("email") == ADMIN_EMAIL


def test_login_invalid_password_fails():
    response = request(
        "POST",
        "/api/Auth/login",
        json_body={"email": ADMIN_EMAIL, "password": "WrongPassword!"},
    )
    assert response.status_code == 400, response.text


test_login_success_returns_data_token()
test_login_invalid_password_fails()
