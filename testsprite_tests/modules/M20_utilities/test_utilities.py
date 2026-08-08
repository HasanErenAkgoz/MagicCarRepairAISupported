from api_support import login_as_super_admin, request, assert_status, assert_unauthorized


def test_help_and_error_messages():
    _, _, headers = login_as_super_admin()
    help_resp = request("GET", "/api/Help/articles", headers=headers)
    assert_status(help_resp, (200, 404), "help")

    err_resp = request("GET", "/api/ErrorMessages", headers=headers)
    assert_status(err_resp, (200, 404), "error messages")


def test_translate_public_or_auth():
    response = request("POST", "/api/Translate", json_body={"text": "hello", "targetLanguage": "tr"})
    assert response.status_code in (200, 401, 404), response.text


def test_subscription_info():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/Subscription/current", headers=headers)
    assert_status(response, (200, 404), "subscription")


test_help_and_error_messages()
test_translate_public_or_auth()
test_subscription_info()
