from api_support import login_as_super_admin, request, assert_status, assert_unauthorized


def test_audit_logs():
    _, _, headers = login_as_super_admin()
    response = request("GET", "/api/AuditLogs", headers=headers, params={"page": 1, "pageSize": 5})
    assert_status(response, (200, 404), "audit logs")


def test_sync_data_smoke():
    _, _, headers = login_as_super_admin()
    response = request(
        "POST",
        "/api/Sync/data",
        headers=headers,
        json_body={"changes": []},
    )
    assert_status(response, (200, 400), "sync data")


def test_audit_auth():
    assert_unauthorized("/api/AuditLogs")


test_audit_logs()
test_sync_data_smoke()
test_audit_auth()
