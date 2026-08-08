import requests

from api_support import DEFAULT_TIMEOUT, _url, login_as_super_admin


def test_post_api_notifications_id_read_marks_notification_as_read():
    _, _, headers = login_as_super_admin()

    list_response = requests.get(
        _url("/api/Notifications/my-notifications"),
        headers=headers,
        params={"pageNumber": 1, "pageSize": 50},
        timeout=DEFAULT_TIMEOUT,
    )
    assert list_response.status_code == 200, list_response.text
    list_body = list_response.json()
    notifications = list_body.get("data") if isinstance(list_body, dict) else list_body
    assert isinstance(notifications, list) and notifications, "No notifications available"

    notification_id = notifications[0]["id"]
    read_response = requests.post(
        _url(f"/api/Notifications/{notification_id}/read"),
        headers=headers,
        timeout=DEFAULT_TIMEOUT,
    )
    assert read_response.status_code == 200, read_response.text
    assert read_response.json().get("success") is True

    refreshed = requests.get(
        _url("/api/Notifications/my-notifications"),
        headers=headers,
        params={"pageNumber": 1, "pageSize": 50},
        timeout=DEFAULT_TIMEOUT,
    )
    refreshed_list = refreshed.json().get("data") or []
    marked = next((n for n in refreshed_list if n.get("id") == notification_id), None)
    assert marked is not None
    assert marked.get("readDate") is not None


test_post_api_notifications_id_read_marks_notification_as_read()
