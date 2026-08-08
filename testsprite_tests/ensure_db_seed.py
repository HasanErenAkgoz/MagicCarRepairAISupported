"""
Seed PostgreSQL via API according to test scenarios (run before any test suite).

Uses admin@magiccar.com / Admin123! and creates customers, vehicles, work orders,
parts, employees, and a portal test user until scenario_seed_manifest targets are met.
"""
from __future__ import annotations

import json
import subprocess
import sys
import uuid
from pathlib import Path
from typing import Any

import requests

from api_support import (
    BASE_URL,
    DEFAULT_TIMEOUT,
    create_customer,
    create_part,
    create_vehicle,
    create_work_order,
    login_as_super_admin,
    new_employee_payload,
    request,
)

ROOT = Path(__file__).resolve().parent
MANIFEST_PATH = ROOT / "scenario_seed_manifest.json"
REPORT_PATH = ROOT / "tmp" / "scenario_seed_report.json"
ANCHOR_SQL = ROOT.parent / "scripts" / "seed_anchor_customer.sql"

PORTAL_TEST_EMAIL = "testsprite_portal_scenario@example.com"
PORTAL_TEST_PASSWORD = "TestSprite1!"

MANAGER_TEST_EMAIL = "manager@magiccar.com"
MANAGER_TEST_PASSWORD = "Manager123!"
MANAGER_CLIENT_ID = 1


def load_targets() -> dict[str, Any]:
    data = json.loads(MANIFEST_PATH.read_text(encoding="utf-8"))
    return data["targets"]


def get_parts_count(headers: dict[str, str]) -> int:
    response = request("GET", "/api/Parts", headers=headers, params={"pageNumber": 1, "pageSize": 1})
    if response.status_code != 200:
        return -1
    return int(response.json().get("totalCount") or 0)


def get_customers(headers: dict[str, str]) -> list[dict[str, Any]]:
    response = request("GET", "/api/Customers", headers=headers)
    if response.status_code != 200:
        return []
    data = response.json()
    return data if isinstance(data, list) else []


def get_work_orders_count(headers: dict[str, str]) -> int:
    response = request(
        "GET",
        "/api/WorkOrders",
        headers=headers,
        params={"legacyFormat": "true"},
    )
    if response.status_code != 200:
        return -1
    data = response.json()
    return len(data) if isinstance(data, list) else 0


def get_employees_count(headers: dict[str, str]) -> int:
    response = request("GET", "/api/Employees", headers=headers)
    if response.status_code != 200:
        return -1
    data = response.json()
    return len(data) if isinstance(data, list) else 0


def get_invoices_count(headers: dict[str, str]) -> int:
    response = request("GET", "/api/Invoices", headers=headers)
    if response.status_code != 200:
        return -1
    data = response.json()
    return len(data) if isinstance(data, list) else 0


def ensure_anchor_customer_id_1() -> dict[str, Any]:
    """Customers.Id=1 for TC017 / loyalty; Users.Id=1 is admin — do not confuse."""
    if not ANCHOR_SQL.is_file():
        return {"action": "skip", "reason": "seed_anchor_customer.sql missing"}

    try:
        proc = subprocess.run(
            [
                "docker",
                "exec",
                "-i",
                "magiccarrepair-postgres",
                "psql",
                "-U",
                "magiccar",
                "-d",
                "MagicCarRepairDb",
            ],
            input=ANCHOR_SQL.read_text(encoding="utf-8"),
            capture_output=True,
            text=True,
            timeout=30,
            check=False,
        )
    except (FileNotFoundError, subprocess.TimeoutExpired) as exc:
        return {"action": "error", "error": str(exc)}

    if proc.returncode != 0:
        return {"action": "error", "error": (proc.stderr or proc.stdout)[:300]}

    return {"action": "insert_or_exists", "customerId": 1}


def ensure_manager_user(headers: dict[str, str]) -> dict[str, Any]:
    """Manager account for RBAC tests (TC027/TC028)."""
    base = BASE_URL.rstrip("/")
    login = requests.post(
        f"{base}/api/Auth/login",
        json={"email": MANAGER_TEST_EMAIL, "password": MANAGER_TEST_PASSWORD},
        timeout=DEFAULT_TIMEOUT,
    )
    if login.status_code == 200 and login.json().get("success"):
        return {"email": MANAGER_TEST_EMAIL, "action": "login"}

    response = request(
        "POST",
        "/api/user",
        headers=headers,
        json_body={
            "firstName": "Test",
            "lastName": "Manager",
            "email": MANAGER_TEST_EMAIL,
            "phoneNumber": "5550000077",
            "password": MANAGER_TEST_PASSWORD,
            "userType": 2,
            "clientId": MANAGER_CLIENT_ID,
        },
    )
    if response.status_code in (200, 201) and response.json().get("success"):
        return {"email": MANAGER_TEST_EMAIL, "action": "create"}

    return {"email": MANAGER_TEST_EMAIL, "error": response.text[:300]}


def ensure_portal_user() -> dict[str, str]:
    """Fixed portal user for M19 / TC021-style scenarios (register or login)."""
    base = BASE_URL.rstrip("/")
    login = requests.post(
        f"{base}/api/Auth/login",
        json={"email": PORTAL_TEST_EMAIL, "password": PORTAL_TEST_PASSWORD},
        timeout=DEFAULT_TIMEOUT,
    )
    if login.status_code == 200 and login.json().get("success"):
        return {"email": PORTAL_TEST_EMAIL, "password": PORTAL_TEST_PASSWORD, "action": "login"}

    payload = {
        "firstName": "Scenario",
        "lastName": "PortalUser",
        "email": PORTAL_TEST_EMAIL,
        "password": PORTAL_TEST_PASSWORD,
        "confirmPassword": PORTAL_TEST_PASSWORD,
        "phoneNumber": "5550000088",
        "language": "tr",
        "identityNo": f"TSPORTAL{uuid.uuid4().hex[:6]}",
    }
    response = requests.post(
        f"{base}/api/Auth/register-customer",
        json=payload,
        timeout=DEFAULT_TIMEOUT,
    )
    if response.status_code in (200, 201) and response.json().get("success"):
        return {"email": PORTAL_TEST_EMAIL, "password": PORTAL_TEST_PASSWORD, "action": "register"}
    return {"email": PORTAL_TEST_EMAIL, "error": response.text[:200]}


def seed_customers_bundle(headers: dict[str, str], count: int) -> list[dict[str, Any]]:
    created: list[dict[str, Any]] = []
    for _ in range(count):
        customer = create_customer(headers)
        vehicle = create_vehicle(headers, customer["id"])
        wo = create_work_order(headers, customer["id"], vehicle["id"])
        created.append({"customerId": customer["id"], "vehicleId": vehicle["id"], "workOrderId": wo["id"]})
    return created


def customer_rows_for_wo(customers: list[dict[str, Any]]) -> list[dict[str, Any]]:
    return [{"customerId": c.get("id"), "vehicleId": None} for c in customers if c.get("id")]


def seed_work_orders_only(headers: dict[str, str], count: int, existing_customers: list[dict]) -> int:
    rows = [r for r in existing_customers if r.get("customerId")]
    if not rows:
        rows = customer_rows_for_wo(get_customers(headers))
    made = 0
    for i in range(count):
        if not rows:
            bundle = seed_customers_bundle(headers, 1)
            rows.extend(bundle)
        row = rows[i % len(rows)]
        cid = row.get("customerId") or row["id"]
        vid = row.get("vehicleId")
        if not vid:
            vehicle = create_vehicle(headers, cid)
            vid = vehicle["id"]
            row["vehicleId"] = vid
        create_work_order(headers, cid, vid)
        made += 1
    return made


def seed_parts(headers: dict[str, str], target: int) -> int:
    current = get_parts_count(headers)
    made = 0
    while current >= 0 and current < target:
        create_part(headers)
        made += 1
        current = get_parts_count(headers)
    return made


def seed_employees(headers: dict[str, str], target: int) -> int:
    current = get_employees_count(headers)
    made = 0
    while current >= 0 and current < target:
        suffix = uuid.uuid4().hex[:8]
        response = request(
            "POST",
            "/api/Employees",
            headers=headers,
            json_body=new_employee_payload(suffix),
        )
        assert response.status_code in (200, 201), response.text
        made += 1
        current = get_employees_count(headers)
    return made


def try_seed_endpoint(headers: dict[str, str]) -> bool:
    response = requests.post(
        f"{BASE_URL.rstrip('/')}/api/SeedData/run",
        headers=headers,
        params={"clearExisting": "false"},
        timeout=300,
    )
    return response.status_code == 200


def main() -> int:
    print(f"Scenario seed — API: {BASE_URL}")
    print(f"Manifest: {MANIFEST_PATH.name}")

    try:
        requests.get(f"{BASE_URL.rstrip('/')}/health", timeout=10).raise_for_status()
    except Exception as exc:
        print(f"API not reachable: {exc}")
        return 1

    targets = load_targets()
    _, _, headers = login_as_super_admin()
    print("Logged in as admin.")

    report: dict[str, Any] = {"targets": targets, "actions": [], "counts": {}}

    if try_seed_endpoint(headers):
        report["actions"].append("SeedData/run (fill empty tables only)")
        print("  Ran POST /api/SeedData/run (PostgreSQL-safe when API rebuilt)")

    print("  Ensuring anchor customer Id=1 (TestSprite TC017)...")
    anchor = ensure_anchor_customer_id_1()
    report["anchor_customer"] = anchor
    print(f"    Anchor customer: {anchor.get('action', anchor.get('error', 'ok'))}")

    customers = get_customers(headers)
    need_customers = max(0, targets["customers"] - len(customers))
    if need_customers:
        print(f"  Creating {need_customers} customer+vehicle+work-order bundles...")
        bundles = seed_customers_bundle(headers, need_customers)
        report["actions"].append(f"api_bundle x{need_customers}")
        customers = get_customers(headers)

    wo_count = get_work_orders_count(headers)
    need_wo = max(0, targets["work_orders"] - wo_count)
    if need_wo:
        print(f"  Creating {need_wo} additional work orders...")
        seed_work_orders_only(headers, need_wo, customer_rows_for_wo(customers))
        report["actions"].append(f"work_orders x{need_wo}")

    parts_added = seed_parts(headers, targets["parts"])
    if parts_added:
        print(f"  Created {parts_added} parts")
        report["actions"].append(f"parts x{parts_added}")

    emp_added = seed_employees(headers, targets["employees"])
    if emp_added:
        print(f"  Created {emp_added} employees")
        report["actions"].append(f"employees x{emp_added}")

    print("  Ensuring manager test user...")
    manager = ensure_manager_user(headers)
    report["manager_user"] = manager
    print(f"    Manager user: {manager.get('action', manager.get('error', 'ok'))}")

    print("  Ensuring portal test user...")
    portal = ensure_portal_user()
    report["portal_user"] = portal
    print(f"    Portal user: {portal.get('action', portal.get('error', 'ok'))}")

    report["counts"] = {
        "customers": len(get_customers(headers)),
        "parts": get_parts_count(headers),
        "work_orders": get_work_orders_count(headers),
        "employees": get_employees_count(headers),
        "invoices": get_invoices_count(headers),
    }

    REPORT_PATH.parent.mkdir(parents=True, exist_ok=True)
    REPORT_PATH.write_text(json.dumps(report, indent=2), encoding="utf-8")
    print("\nCounts after seed:", report["counts"])
    print(f"Report: {REPORT_PATH}")

    ok = (
        report["counts"]["customers"] >= targets["customers"]
        and report["counts"]["parts"] >= targets["parts"]
        and report["counts"]["work_orders"] >= targets["work_orders"]
        and report["counts"]["employees"] >= targets["employees"]
    )
    if not ok:
        print("ERROR: Scenario targets not met.")
        return 1

    print("Database ready for all scenario modules.")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
