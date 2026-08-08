
# TestSprite AI Testing Report(MCP)

---

## 1️⃣ Document Metadata
- **Project Name:** MagicCarRepairAISupported
- **Date:** 2026-05-28
- **Prepared by:** TestSprite AI Team

---

## 2️⃣ Requirement Validation Summary

#### Test TC001 get_health_returns_healthy
- **Test Code:** [TC001_get_health_returns_healthy.py](./TC001_get_health_returns_healthy.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/ddc8aa3a-3907-4e59-ae53-c2040181d7e3
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC002 post_auth_login_returns_data_token
- **Test Code:** [TC002_post_auth_login_returns_data_token.py](./TC002_post_auth_login_returns_data_token.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/a67e331b-f3b7-41c3-8ec8-0ee436e36c0b
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC003 get_user_profile_authenticated
- **Test Code:** [TC003_get_user_profile_authenticated.py](./TC003_get_user_profile_authenticated.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/ff92a03b-91e9-4da5-89ca-34ac38a83886
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC004 get_clients_system_admin
- **Test Code:** [TC004_get_clients_system_admin.py](./TC004_get_clients_system_admin.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/df2e233c-e70b-46c2-b438-7b03417f4152
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC005 customers_crud_smoke
- **Test Code:** [TC005_customers_crud_smoke.py](./TC005_customers_crud_smoke.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/946ce4ff-85fc-4510-923f-ec2ec5fe3bb1
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC006 vehicles_create_for_customer
- **Test Code:** [TC006_vehicles_create_for_customer.py](./TC006_vehicles_create_for_customer.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 84, in <module>
  File "<string>", line 18, in test_vehicles_create_for_customer
AssertionError: Token not found in login response

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/aaab3b43-51bd-48dc-858f-f11703e67493
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC007 workorders_list_and_status
- **Test Code:** [TC007_workorders_list_and_status.py](./TC007_workorders_list_and_status.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 129, in <module>
  File "<string>", line 26, in test_workorders_list_and_status
AssertionError: Token not found in login response

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/916ba7e9-17b7-442a-8d40-2693a3a058d2
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC008 parts_create_and_bulk_delete
- **Test Code:** [TC008_parts_create_and_bulk_delete.py](./TC008_parts_create_and_bulk_delete.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 88, in <module>
  File "<string>", line 53, in test_parts_create_and_bulk_delete
AssertionError: Part creation failed with status 400

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/73ad262e-67b2-47cb-b684-da86149a6eec
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC009 appointments_list
- **Test Code:** [TC009_appointments_list.py](./TC009_appointments_list.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/5685db4e-a336-414a-a2d6-18bf2384778d
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC010 quote_requests_list
- **Test Code:** [TC010_quote_requests_list.py](./TC010_quote_requests_list.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/418358a0-29eb-4362-98b6-dc71ec5fb710
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC011 invoice_from_workorder
- **Test Code:** [TC011_invoice_from_workorder.py](./TC011_invoice_from_workorder.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 119, in <module>
  File "<string>", line 29, in test_invoice_from_workorder
AssertionError: Customer creation failed: {"success":false,"errorCode":"CUSTOMER_IDENTITY_NO_EXISTS","message":"TC011-Id11 kimlik numaras?? ile kay??tl?? m????teri zaten mevcut.","details":{"identityNo":"TC011-Id11"}}

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/ac629fd9-7e3f-4b5d-bcc4-1e1d30eaad81
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC012 accounting_monthly_summary
- **Test Code:** [TC012_accounting_monthly_summary.py](./TC012_accounting_monthly_summary.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/25c9c282-e684-4b26-8eec-b9a768d53954
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC013 dashboard_stats
- **Test Code:** [TC013_dashboard_stats.py](./TC013_dashboard_stats.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/7970a76f-c953-4472-9b72-fe6af3e0d692
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC014 employees_create
- **Test Code:** [TC014_employees_create.py](./TC014_employees_create.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 73, in <module>
  File "<string>", line 56, in test_employees_create
AssertionError: Employee creation failed: 400 - {"success":false,"errorCode":"BODY_PARSE_ERROR","message":"BODY_PARSE_ERROR","errors":{"$.employmentStatus":["The JSON value could not be converted to MagicCarRepairAISupported.Domain.Enums.EmploymentStatus. Path: $.employmentStatus | LineNumber: 0 | BytePositionInLine: 191."]}}

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/7f4186dc-1965-47cc-943d-8c290fcaccf7
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC015 insurance_policies_expiring
- **Test Code:** [TC015_insurance_policies_expiring.py](./TC015_insurance_policies_expiring.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/4d2905ba-c588-4321-8fe4-62f01e21a305
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC016 notifications_my_list
- **Test Code:** [TC016_notifications_my_list.py](./TC016_notifications_my_list.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/2b11e445-0948-427f-b7e8-b73838181b5d
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC017 loyalty_customer_points
- **Test Code:** [TC017_loyalty_customer_points.py](./TC017_loyalty_customer_points.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/9ac064d7-305f-48bd-9a75-3f499f37bdd6
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC018 ai_resolve_part_prices
- **Test Code:** [TC018_ai_resolve_part_prices.py](./TC018_ai_resolve_part_prices.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/2bf9926e-c84b-4327-81c7-5a19ee5e1c7c
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC019 audit_logs_list
- **Test Code:** [TC019_audit_logs_list.py](./TC019_audit_logs_list.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/58d813fa-cc80-4136-8d18-5d1e81cda09d
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC020 public_clients_list
- **Test Code:** [TC020_public_clients_list.py](./TC020_public_clients_list.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/4334425c-4e8b-425e-a6e8-60a33ee52dc2
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC021 customer_portal_profile
- **Test Code:** [TC021_customer_portal_profile.py](./TC021_customer_portal_profile.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/lang/lib/python3.12/site-packages/urllib3/connectionpool.py", line 534, in _make_request
    response = conn.getresponse()
               ^^^^^^^^^^^^^^^^^^
  File "/var/lang/lib/python3.12/site-packages/urllib3/connection.py", line 571, in getresponse
    httplib_response = super().getresponse()
                       ^^^^^^^^^^^^^^^^^^^^^
  File "/var/lang/lib/python3.12/http/client.py", line 1430, in getresponse
    response.begin()
  File "/var/lang/lib/python3.12/http/client.py", line 331, in begin
    version, status, reason = self._read_status()
                              ^^^^^^^^^^^^^^^^^^^
  File "/var/lang/lib/python3.12/http/client.py", line 292, in _read_status
    line = str(self.fp.readline(_MAXLINE + 1), "iso-8859-1")
               ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
  File "/var/lang/lib/python3.12/socket.py", line 720, in readinto
    return self._sock.recv_into(b)
           ^^^^^^^^^^^^^^^^^^^^^^^
TimeoutError: timed out

The above exception was the direct cause of the following exception:

Traceback (most recent call last):
  File "/var/lang/lib/python3.12/site-packages/requests/adapters.py", line 667, in send
    resp = conn.urlopen(
           ^^^^^^^^^^^^^
  File "/var/lang/lib/python3.12/site-packages/urllib3/connectionpool.py", line 841, in urlopen
    retries = retries.increment(
              ^^^^^^^^^^^^^^^^^^
  File "/var/lang/lib/python3.12/site-packages/urllib3/util/retry.py", line 490, in increment
    raise reraise(type(error), error, _stacktrace)
          ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
  File "/var/lang/lib/python3.12/site-packages/urllib3/util/util.py", line 39, in reraise
    raise value
  File "/var/lang/lib/python3.12/site-packages/urllib3/connectionpool.py", line 787, in urlopen
    response = self._make_request(
               ^^^^^^^^^^^^^^^^^^^
  File "/var/lang/lib/python3.12/site-packages/urllib3/connectionpool.py", line 536, in _make_request
    self._raise_timeout(err=e, url=url, timeout_value=read_timeout)
  File "/var/lang/lib/python3.12/site-packages/urllib3/connectionpool.py", line 367, in _raise_timeout
    raise ReadTimeoutError(
urllib3.exceptions.ReadTimeoutError: HTTPConnectionPool(host='proxy.tun.testsprite.com', port=9090): Read timed out. (read timeout=120)

During handling of the above exception, another exception occurred:

Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 80, in <module>
  File "<string>", line 21, in test_customer_portal_profile
  File "/var/lang/lib/python3.12/site-packages/requests/api.py", line 115, in post
    return request("post", url, data=data, json=json, **kwargs)
           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
  File "/var/lang/lib/python3.12/site-packages/requests/api.py", line 59, in request
    return session.request(method=method, url=url, **kwargs)
           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
  File "/var/lang/lib/python3.12/site-packages/requests/sessions.py", line 589, in request
    resp = self.send(prep, **send_kwargs)
           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
  File "/var/lang/lib/python3.12/site-packages/requests/sessions.py", line 703, in send
    r = adapter.send(request, **kwargs)
        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
  File "/var/lang/lib/python3.12/site-packages/requests/adapters.py", line 713, in send
    raise ReadTimeout(e, request=request)
requests.exceptions.ReadTimeout: HTTPConnectionPool(host='proxy.tun.testsprite.com', port=9090): Read timed out. (read timeout=120)

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/8924d918-8823-4069-b4c3-72cf3a83a4e6
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC022 subscription_current
- **Test Code:** [TC022_subscription_current.py](./TC022_subscription_current.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/5bbb708e-02d1-4973-96d2-f72c33f42c7e
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC023 stock_alerts_active
- **Test Code:** [TC023_stock_alerts_active.py](./TC023_stock_alerts_active.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/ebaf11e4-fc82-468a-bba3-affeccea1fb7
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC024 auth_anonymous_401
- **Test Code:** [TC024_auth_anonymous_401.py](./TC024_auth_anonymous_401.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/47d3e939-30d8-42f8-acfe-5b053532266d
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC025 login_invalid_password_400
- **Test Code:** [TC025_login_invalid_password_400.py](./TC025_login_invalid_password_400.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/cbe5f872-848c-4652-a7d5-fc8f41568731
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC026 profile_anonymous_401
- **Test Code:** [TC026_profile_anonymous_401.py](./TC026_profile_anonymous_401.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/9315dfcb-4ac3-48c7-abab-236241aecab6
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC027 permission_manager_forbidden
- **Test Code:** [TC027_permission_manager_forbidden.py](./TC027_permission_manager_forbidden.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/c3407a4a-b258-4b09-a412-2622ff87a577
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC028 impersonate_manager_forbidden
- **Test Code:** [TC028_impersonate_manager_forbidden.py](./TC028_impersonate_manager_forbidden.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 35, in <module>
  File "<string>", line 30, in test_impersonate_manager_forbidden
AssertionError: Expected 403 Forbidden, got 400

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/c5b1aea7-931d-469d-9005-217afa798dd5
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC029 workorders_mobile_list_contract
- **Test Code:** [TC029_workorders_mobile_list_contract.py](./TC029_workorders_mobile_list_contract.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/7fe7bee9-ed44-4ef4-80ca-59ca62ba206e
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC030 workorders_export
- **Test Code:** [TC030_workorders_export.py](./TC030_workorders_export.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/c0dc8f99-caa1-41a6-a7cd-52b7a62db9bd
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC031 dashboard_charts_and_revenue
- **Test Code:** [TC031_dashboard_charts_and_revenue.py](./TC031_dashboard_charts_and_revenue.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 32, in <module>
  File "<string>", line 18, in test_dashboard_charts_and_revenue
AssertionError: Token not found in login response

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/156e3b8f-ad67-47a7-b89b-a5f9fe328586
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC032 customers_invalid_id_404
- **Test Code:** [TC032_customers_invalid_id_404.py](./TC032_customers_invalid_id_404.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 37, in <module>
  File "<string>", line 26, in test_customers_invalid_id_404
AssertionError: Token not found in login response

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/b242e21d-06e5-4cae-8db0-16ec2ac7c86f
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC033 parts_low_stock_and_movements
- **Test Code:** [TC033_parts_low_stock_and_movements.py](./TC033_parts_low_stock_and_movements.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/adf83384-85ae-41d7-96b7-3b65e1d1868a
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC034 part_suppliers_and_auto_orders
- **Test Code:** [TC034_part_suppliers_and_auto_orders.py](./TC034_part_suppliers_and_auto_orders.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/ccf87e6a-884d-42cf-b9fe-9941dfd16abe
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC035 accounting_income_expense_reports
- **Test Code:** [TC035_accounting_income_expense_reports.py](./TC035_accounting_income_expense_reports.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/fdea20dd-8e50-4ec0-a0d3-e92b78cc1163
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC036 sync_data_empty_payload
- **Test Code:** [TC036_sync_data_empty_payload.py](./TC036_sync_data_empty_payload.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 36, in <module>
  File "<string>", line 30, in test_sync_data_empty_payload
AssertionError: Unexpected status code: 400

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/725fcf08-f01a-4a21-93e1-fb504deebb40
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC037 client_portal_profile_401
- **Test Code:** [TC037_client_portal_profile_401.py](./TC037_client_portal_profile_401.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/5fd931cb-24ca-4df5-86bd-6b5c4cf75014
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC038 customer_cannot_list_shop_customers
- **Test Code:** [TC038_customer_cannot_list_shop_customers.py](./TC038_customer_cannot_list_shop_customers.py)
- **Test Error:** Traceback (most recent call last):
  File "/var/task/handler.py", line 258, in run_with_retry
    exec(code, exec_env)
  File "<string>", line 55, in <module>
  File "<string>", line 23, in test_customer_cannot_list_shop_customers
AssertionError: Register failed: {"data":null,"success":false,"message":"Şifreler eşleşmiyor."}

- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/c39c174c-39df-4e9f-a0c8-4dfdff7e2c9c
- **Status:** ❌ Failed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC039 vehicles_anonymous_401
- **Test Code:** [TC039_vehicles_anonymous_401.py](./TC039_vehicles_anonymous_401.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/cc5a89b0-ac03-4bf9-b498-206f9f6b1112
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---

#### Test TC040 invoices_list
- **Test Code:** [TC040_invoices_list.py](./TC040_invoices_list.py)
- **Test Visualization and Result:** https://www.testsprite.com/dashboard/mcp/tests/c3820539-dbae-482f-9022-f3b5249ed335/8c8fcd67-2e00-4539-8848-9a392a924666
- **Status:** ✅ Passed
- **Analysis / Findings:** {{TODO:AI_ANALYSIS}}.
---


## 3️⃣ Coverage & Matching Metrics

- **72.50** of tests passed

| Requirement        | Total Tests | ✅ Passed | ❌ Failed  |
|--------------------|-------------|-----------|------------|
| ...                | ...         | ...       | ...        |
---


## 4️⃣ Key Gaps / Risks
{AI_GNERATED_KET_GAPS_AND_RISKS}
---