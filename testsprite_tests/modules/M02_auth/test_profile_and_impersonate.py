from api_support import (
    USER_TYPE_MANAGER,
    assert_forbidden,
    assert_unauthorized,
    login_as_super_admin,
    request,
    assert_status,
)


def test_user_profile_requires_auth():
    assert_unauthorized("/api/user/profile")


def test_user_profile_with_admin_token():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/user/profile", headers=headers)
    assert_status(response, 200, "profile")


def test_manager_cannot_impersonate():
    assert_forbidden(
        "/api/Auth/impersonate",
        USER_TYPE_MANAGER,
        method="POST",
        json_body={"targetUserId": 1},
    )


test_user_profile_requires_auth()
test_user_profile_with_admin_token()
test_manager_cannot_impersonate()
