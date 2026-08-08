# Backend Authorization Audit Checklist

Mobile RBAC is **UX-only**. Every API must enforce tenant and role boundaries server-side.

**Backend codebase:** `c:\Projects\MagicCarRepairAISupported`  
**Entry:** `Core.Packages.WebAPI` — `http://localhost:5169`

---

## Implementation status (2026-05-26)

| Area | Status | Implementation |
|------|--------|----------------|
| Shop vs Customer API | **Done** | `AuthPolicyNames` on shop controllers + method-level quotes/AI/portal |
| RBAC gap controllers | **Done** | Permission (SA), Role/Chat/Payments (ShopStaff), Notifications send/push (ShopStaff) |
| Anonymous AI rate limit | **Done** | Policy `ai-anonymous` on `generate-description`, `analyze-damage-photos` |
| Tenant `?? 1` fallback | **Done** | `ITenantService.GetRequiredClientId()` |
| `X-Client-Id` header | **Done** | Only honored when `UserType == SystemAdmin` |
| Health check | **Done** | `GET /health` (PostgreSQL) |
| Auth integration tests | **Done** | `Core.Packages.WebAPI.Tests/Authorization/AuthorizationIntegrationTests.cs` |
| Secrets in repo | **Done** | Sanitized `appsettings.json` (gitignored); see backend `docs/SECRETS.md` |
| Mobile route guards | **Done** | `routeAccess.ts` + `RouteGuard` |
| Mobile app lock blocks API | **Done** | `setAppLockChecker` in `http.ts` |

### Policies (`Core.Packages.WebAPI/Authorization/`)

- **ShopStaff** — UserType 1, 2, 3 (SA, Manager, Employee)
- **CustomerOnly** — UserType 4
- **CustomerOrSystemAdmin** — 1 or 4 (portal, diagnose, payments, quote accept)
- **SystemAdminOnly** — 1

### Automated regression (`dotnet test`)

- Customer → `GET /api/Customers` → **403**
- Customer → `GET /api/quote-requests/open` → **403**
- Shop → `GET /api/quote-requests/open` → not **403**
- Customer → `GET /api/customer-portal/work-orders` → not **403**
- Manager → `POST /api/Auth/impersonate` → **403**

---

## Sign-off

| Area | Owner | Date | Pass |
|------|-------|------|------|
| Customer isolation | CI | 2026-05-26 | Auto |
| Tenant isolation | | | |
| Impersonation | CI | 2026-05-26 | Auto |
| Refresh rotation | | | |

See also: [BACKEND_REPO.md](./BACKEND_REPO.md)
