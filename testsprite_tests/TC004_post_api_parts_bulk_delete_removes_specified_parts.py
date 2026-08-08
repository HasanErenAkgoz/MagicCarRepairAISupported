import requests

from api_support import DEFAULT_TIMEOUT, _url, create_part, login_as_super_admin


def test_post_api_parts_bulk_delete_removes_specified_parts():
    _, _, headers = login_as_super_admin()
    part1 = create_part(headers)
    part2 = create_part(headers)
    delete_ids = [part1["id"], part2["id"]]

    bulk_response = requests.post(
        _url("/api/Parts/bulk-delete"),
        headers=headers,
        json={"ids": delete_ids},
        timeout=DEFAULT_TIMEOUT,
    )
    assert bulk_response.status_code == 200, bulk_response.text
    result = bulk_response.json()
    assert result.get("success") is True
    assert result.get("deletedCount", 0) >= 1
    deleted_ids = result.get("deletedIds") or []
    assert any(part_id in deleted_ids for part_id in delete_ids)


test_post_api_parts_bulk_delete_removes_specified_parts()
