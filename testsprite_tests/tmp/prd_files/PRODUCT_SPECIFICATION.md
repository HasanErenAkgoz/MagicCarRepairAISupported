# Magic Car Repair Mobile — Product Specification Document (PRD)

**Document version:** 1.0  
**Last updated:** 2026-05-28  
**Product:** Magic Car Repair Mobile (Expo / React Native)  
**Companion backend:** Magic Car Repair API (.NET, port `5169`)  
**Purpose:** TestSprite, QA automation, and stakeholder alignment

---

## 1. Executive Summary

Magic Car Repair is an **AI-assisted, two-sided automotive service platform** delivered as a single mobile application (iOS, Android, and optional web via Expo). Three distinct user personas share one codebase, routed by role after authentication:

| Persona | `UserType` (backend) | Home experience |
|---------|----------------------|-----------------|
| Platform administrator | `SystemAdmin` (1) | Multi-tenant client management |
| Shop owner / staff | `Manager` (2), `Employee` (3) | Shop operations dashboard |
| Vehicle owner (customer) | `Customer` (4) | Customer portal |

The product connects **vehicle owners** who need transparent pricing and repair tracking with **auto repair shops** that need digital work orders, inventory, billing, and customer communication—all in one app with **Turkish and English** UI.

**Core value proposition:** Pre-repair AI diagnosis + cross-shop price comparison for customers; end-to-end shop operations (work orders, quotes, invoices, appointments, insurance) for service providers.

---

## 2. Problem Statement

### Vehicle owners
- Uncertainty about repair cost and shop trust before visiting a garage
- No visibility into work order progress without phone calls
- Fragmented communication (WhatsApp, paper receipts)

### Repair shops
- Work orders tracked on paper or spreadsheets
- Quotes, appointments, inventory, and billing in separate tools
- Lost customer history and no structured loyalty

### Platform goal
Replace opacity and manual processes with a **single digital workflow** from AI-assisted discovery → quote → work order → invoice → payment, with real-time messaging and push notifications.

---

## 3. Product Scope

### In scope (this mobile app)
- Authentication (email/password, OTP reset, optional 2FA, biometric lock on device)
- Guest flows: welcome, shop discovery, registration (customer & shop)
- Shop panel: customers, vehicles, work orders, inventory, employees, invoices, appointments, reports, insurance, AI diagnosis, open quote requests, rewards
- Customer portal: vehicles, AI diagnosis, shop discovery, quote requests, work order tracking, messages, invoices, loyalty, insurance view, help center
- System admin: tenant (client) CRUD, client users, roles
- Settings: language (TR/EN), currency, profile, password, subscription info
- Real-time chat (SignalR) when authenticated
- Push notification registration on sign-in (native builds)

### Out of scope (separate systems)
- Web admin panel (documented separately; not this Expo app’s primary surface)
- Payment gateway UI completion (Iyzico integration may be partial)
- Native-only features in browser-based TestSprite runs: camera barcode scan, push, biometrics, some image pickers

### Test environment (TestSprite / web)
- **Frontend URL:** `http://localhost:8081/` (Expo Metro web: `npm run web`)
- **Backend API:** `http://localhost:5169` (must be running; app reads `EXPO_PUBLIC_API_HOST` / `EXPO_PUBLIC_API_PORT`)
- **Health check:** Open `http://localhost:5169/health` in browser before E2E
- **Recommended `.env.local` for web testing:**
  ```env
  EXPO_PUBLIC_API_HOST=localhost
  EXPO_PUBLIC_API_PORT=5169
  EXPO_PUBLIC_API_SCHEME=http
  EXPO_PUBLIC_ALLOW_CLEARTEXT=true
  EXPO_PUBLIC_APP_ENV=development
  ```

---

## 4. User Roles & Access Control

### 4.1 Role mapping

| Role | `UserType` | App role key | Default home screen |
|------|------------|--------------|---------------------|
| System admin | 1 | `systemAdmin` | `SuperAdminDashboard` (via `Home` route) |
| Manager (shop owner) | 2 | `shop` | `AdminDashboardScreen` |
| Employee | 3 | `shop` | `AdminDashboardScreen` |
| Customer | 4 | `customer` | `CustomerHomeScreen` |
| Unauthenticated | — | `guest` | `Welcome` or `Login` |

Route access is enforced in `routeAccess.ts` + `RouteGuard`. System admin can impersonate shop/customer users (admin-only feature).

### 4.2 Initial navigation rules
- **Guest:** If last saved role was `shop` → open `Login`; otherwise → `Welcome`
- **Authenticated:** If onboarding not completed for role → `Onboarding`; else → `Home` (role-specific dashboard component)
- **Onboarding:** User can skip via `onboarding-skip` testID; persisted per role

---

## 5. Test Accounts & Data Prerequisites

> Credentials assume a **seeded development database** on the backend (`reset-database` or equivalent seed scripts in `MagicCarRepairAISupported`).

| Account | Email | Password | Role | Use for |
|---------|-------|----------|------|---------|
| System admin (primary E2E) | `admin@magiccar.com` | `Admin123!` | SystemAdmin | Full route smoke via dev E2E menu; all shop + customer routes |
| Shop manager | *(seed-dependent)* | *(seed-dependent)* | Manager | Shop-only workflows without admin impersonation |
| Customer | *(seed-dependent)* | *(seed-dependent)* | Customer | Customer portal journeys |

**Maestro / automation variables:**
```yaml
LOGIN_EMAIL: admin@magiccar.com
LOGIN_PASSWORD: Admin123!
```

**Login screen testIDs:**
- Email: `login-email`
- Password: `login-password`
- Submit: `login-submit`
- Error banner (on failure): `login-error-banner`

**Post-login (dev build only):** Floating **E2E** button → `maestro-launcher-open` → navigate to any route via `maestro-nav-{RouteName}`.

**Every screen wrapper testID:** `screen-{RouteName}` (e.g. `screen-Login`, `screen-WorkOrders`).

---

## 6. Authentication & Security Requirements

### 6.1 Login
1. User enters email + password on `Login` screen
2. App calls `POST /api/Auth/Login` with `Accept-Language` from current locale (TR/EN)
3. **Success:** JWT + refresh token stored in `expo-secure-store`; user object in `AuthContext`; SignalR hub connects; push token registered (native)
4. **2FA required:** Navigate to `TwoFactorLogin` with email; complete verification before home
5. **Failure:** Show `login-error-banner` with API message; remain on login

### 6.2 Registration (guest)
- **Customer path:** `Welcome` → register customer → `CustomerPersonalInfo` → `VehicleInfo` → account creation
- **Shop path:** `Welcome` → register shop → `ShopPersonalInfo` → `ShopBusinessDetails` → tenant creation
- Entry from login: `login-register-customer`, `login-register-shop`

### 6.3 Password recovery
`ForgotPassword` → `VerifyOTP` (phone) → `ResetPassword` (token)

### 6.4 Session
- Token passed to all service calls as Bearer header
- Sign out clears token and disconnects SignalR
- HTTP timeout: **10 seconds** per request (`src/utils/http.ts`)

### 6.5 Acceptable test outcomes
- Valid credentials → reach `Home` or onboarding skip → dashboard visible
- Invalid credentials → error banner, no navigation to authenticated stack
- API down → network error message, no crash

---

## 7. Functional Modules by Persona

### 7.1 Guest (unauthenticated)

| Feature | Route(s) | Expected behavior |
|---------|----------|-------------------|
| Welcome / marketing | `Welcome` | Explore shops (`guest-welcome-explore`) or sign in (`guest-welcome-signin`) |
| Login | `Login` | Email/password auth |
| Shop discovery | `ShopDiscovery`, `ShopDetail` | Browse public shop list; view shop profile |
| Forgot password | `ForgotPassword`, `VerifyOTP`, `ResetPassword` | OTP-based reset flow |

### 7.2 Customer portal (`UserType.Customer`)

**Bottom navigation (CustomerHome):** Home · Messages · Quote Requests · Work Orders · Profile

| Module | Route(s) | Requirements |
|--------|----------|--------------|
| Home dashboard | `Home` → CustomerHome | Quick actions: AI diagnosis, shop discovery, quote create, vehicles, maintenance, invoices, loyalty, help, insurance |
| AI diagnosis | `CustomerAiDiagnosis` | Submit complaint + photos; receive AI summary and cost range; optional link to quote/shop offers |
| Shop discovery | `ShopDiscovery`, `ShopDetail` | List nearby shops; trust/rating signals; start quote from shop |
| Price comparison | `ShopOffers` | After AI: compare part prices across shops; select shops for quote |
| Quote requests | `QuoteRequests`, `QuoteRequestDetail`, `QuoteRequestCreate` | Create request; view status; accept/reject shop quotes |
| Work orders | `CustomerWorkOrders`, `CustomerWorkOrderDetail` | List orders by tab (pending / in progress / completed / awaiting approval); timeline and photos |
| Vehicles | `MyVehicles`, `CustomerVehicleAdd`, `MaintenanceHistory` | CRUD vehicles; view service history |
| Messages | `Messages`, `Chat` | Conversation list; per-user chat (optional `workOrderId` context) |
| Invoices | `CustomerInvoices`, `CustomerInvoiceDetail` | View bills from shop |
| Loyalty | `Loyalty`, `Rewards` | Points balance and redeemable rewards |
| Insurance | `CustomerInsurance` | View linked policies/claims (read-oriented) |
| Profile | `CustomerProfile`, `CustomerProfileEdit` | Personal info edit |
| Help | `HelpCenter` | FAQ / support content |

**Customer work order tabs (backend status strings):**
- **Pending:** `AppointmentScheduled`, `VehicleEntered`, `DiagnosisCompleted`
- **In progress:** `InProgress`, `InRepair`, `WaitingForParts`, `QualityControl`, `Washing`
- **Completed:** `ReadyForDelivery`, `Delivered`
- **Awaiting approval:** `requiresCustomerApproval === true` (separate from status enum—not `WaitingCustomerApproval`)

Customer can **approve or reject** shop proposals when `requiresCustomerApproval` is true.

### 7.3 Shop panel (`Manager`, `Employee`)

**Dashboard tabs:** Orders · Customers · Employees · Settings (plus quick-action tiles)

| Module | Route(s) | Requirements |
|--------|----------|--------------|
| Work orders | `WorkOrders`, `WorkOrderDetail`, `WorkOrderEdit`, `WorkOrderCreate` | List with filters (pending/inProgress/completed/cancelled); detail with status timeline, photos, parts/labor; create/edit |
| Customers | `Customers`, `CustomerDetail`, `CustomerAdd`, `CustomerImport` | Search/list; detail includes inline vehicles; bulk import |
| Vehicles | `VehicleAdd`, `VehicleEdit` | Linked to customer |
| Inventory | `Inventory`, `PartAdd`, `PartDetail`, `PartEdit`, `InventoryScan` | Parts list; OEM codes; low-stock alerts; scan on native |
| Employees | `Employees`, `EmployeeDetail`, `EmployeeAdd` | Staff CRUD; role assignment |
| Invoices | `Invoices`, `InvoiceDetail`, `InvoiceCreate`, `InvoicePayment`, `PaymentHistory` | Billing and payment recording |
| Open quotes | `OpenQuoteRequests`, `OpenQuoteRequestDetail` | Incoming customer quote requests; submit shop quote |
| Appointments | `Appointments`, `AppointmentCreate` | Calendar scheduling |
| Reports | `Reports` | Revenue / efficiency summaries |
| AI diagnosis | `AiDiagnosis` | Shop-side AI assist (inventory-aware) |
| Insurance | `InsurancePolicies`, `InsurancePolicyDetail`, `InsurancePolicyCreate`, `InsuranceCompanyManagement`, `InsuranceClaims`, `InsuranceClaimDetail`, `InsuranceClaimCreate` | Policy and claim management |
| Rewards | `RewardManagement` | Loyalty program configuration |
| Business | `BusinessProfile`, `Subscription` | Shop profile and plan |
| Settings | `Settings`, `ChangePassword`, `TwoFactorSetup`, `UserProfileEdit` | App preferences |

**Shop list work order status (API filter groups):** `pending` | `inProgress` | `completed` | `cancelled` (mapped from backend granular statuses).

**Work order photo types (enum, aligned with backend):** Entry=1, Process=2, Exit=3, Damage=4, Other=5

### 7.4 System admin (`SystemAdmin`)

| Module | Route(s) | Requirements |
|--------|----------|--------------|
| Dashboard | `SuperAdminDashboard` | Tenant overview |
| Clients | `ClientAdd`, `ClientDetail`, `ClientEdit` | Create/edit repair shop tenants |
| Client users | `ClientUsers`, `ClientUserDetail` | Users per tenant |
| Roles | `Roles`, `RoleDetail` | Permission roles per client |
| Impersonation | *(via admin UI)* | Act as shop or customer user for support |

Admin E2E account can open **all routes** via dev menu; detail screens may show API errors with placeholder IDs—**smoke test = screen mounts** (`screen-{Route}` visible).

---

## 8. Critical User Journeys (Acceptance Criteria)

### Journey A — Shop staff login and view work orders
1. Open app → `Login` (or skip from `Welcome`)
2. Enter valid shop/admin credentials → submit
3. Skip onboarding if shown
4. Land on shop `Home` dashboard
5. Navigate to `WorkOrders` (`nav-shop-work-orders` or E2E menu)
6. **Pass:** List loads or empty state; no unhandled error; `screen-WorkOrders` visible

### Journey B — Customer views work order progress
1. Login as `Customer`
2. Open `CustomerWorkOrders` tab
3. Tap an order → `CustomerWorkOrderDetail`
4. **Pass:** Status label matches backend; timeline/photos render; approval buttons only if `requiresCustomerApproval`

### Journey C — Guest explores shops
1. Launch unauthenticated → `Welcome`
2. Tap explore → `ShopDiscovery`
3. Open a shop → `ShopDetail`
4. **Pass:** Shop info visible without login; optional CTA to register/login

### Journey D — Customer creates quote request
1. Login as customer
2. From home: `QuoteRequestCreate` or via `ShopDetail`
3. Fill vehicle/complaint fields → submit
4. **Pass:** Request appears in `QuoteRequests`; shop sees it in `OpenQuoteRequests`

### Journey E — Password reset (guest)
1. `Login` → `login-forgot-password`
2. Complete phone OTP flow
3. **Pass:** Reach `ResetPassword` with valid token; success returns to login

### Journey F — Settings language switch
1. Authenticated → `Settings`
2. Change language TR ↔ EN
3. **Pass:** UI strings update; subsequent API calls send new `Accept-Language`

---

## 9. Quote & Work Order Business Flow

```
Customer                          Shop                         System
────────                          ────                         ──────
CustomerAiDiagnosis / ShopOffers
        │
        ▼
QuoteRequestCreate ──────────► OpenQuoteRequests
        │                              │
        │                              ▼
        │                      Submit quote (OpenQuoteRequestDetail)
        │                              │
        ▼                              │
QuoteRequestDetail ◄── accept ─────────┘
        │
        ▼
Work order created (backend)
        │
        ▼
CustomerWorkOrders ◄── status updates ─── WorkOrderDetail / Edit
        │
        ▼
CustomerInvoices ◄── invoice issued ─── Invoices / InvoiceCreate
```

**Notifications:** Push on key transitions (native); in-app `Notifications` screen for all authenticated roles.

---

## 10. AI Features

| Surface | Route | Input | Output |
|---------|-------|-------|--------|
| Customer AI | `CustomerAiDiagnosis` | Photos, text complaint, vehicle context | Diagnosis summary, estimated cost range, suggested parts |
| Shop AI | `AiDiagnosis` | Similar + shop inventory context | Repair suggestions tied to stock |
| Shop offers | `ShopOffers` | AI-derived part list | Cross-tenant price comparison |

**Dependency:** Backend OpenAI integration; failures should show user-friendly error, not white screen.

**Web/TestSprite limitation:** Image upload may be degraded on web; test text-only paths or mocked responses.

---

## 11. Real-Time & Messaging

- **SignalR hub** connects after login (`signalRService.ts`)
- **Chat** (`Chat`): parameters `otherUserId`, `otherUserName`, optional `workOrderId`
- **Messages** (`Messages`): inbox list
- **Pass criteria:** Screen loads; sending message requires active hub (may fail gracefully if backend down)

---

## 12. UI / UX Specification

### 12.1 Visual design
- **Theme:** Dark mode throughout
- **Background:** `#101722`
- **Gradient screens:** `['#1e3a8a', '#1e1b4b', '#0f172a']` via `LinearGradient`
- **Primary action:** `#3c83f6`
- **Error:** `#ef4444`
- **Cards:** Glass style — `rgba(255,255,255,0.06)` fill, `rgba(255,255,255,0.1)` border
- **Safe area:** Always `react-native-safe-area-context` `SafeAreaView`

### 12.2 Internationalization
- **Languages:** Turkish (`tr`), English (`en`)
- **Namespaces:** `common`, `auth`, `customer`, `home`, `customers`, `workOrders`, `settings`, `notifications`, `inventory`, `employees`, `billing`, etc.
- **Rule:** No hardcoded user-visible strings in screens; use `useTranslation('namespace')`

### 12.3 Navigation
- Single root stack (`RootStackParamList`); ~75 routes
- Deep link: `magiccarrepair://set-password?email=&token=`
- Back navigation: `nav-back` where applicable

### 12.4 Error handling
- API errors → `ApiError` with `message`, optional `errorCode`, `fieldErrors`
- Network failure → `NetworkError` with retry-friendly copy
- React errors → `AppErrorBoundary` (Sentry if DSN configured)

---

## 13. API Integration Summary

| Concern | Detail |
|---------|--------|
| Base URL | `{scheme}://{host}:{port}/api/...` from `src/constants/api.ts` |
| Auth header | `Authorization: Bearer {token}` |
| Language header | `Accept-Language: tr` or `en` |
| Response wrapper | `{ success, data, message? }` as `ApiDataResult<T>` |
| Legacy endpoints | Some customer/vehicle endpoints return raw arrays—handled in services |
| Timeout | 10s abort per request |

**Key endpoint groups (constants file):** `AUTH_ENDPOINTS`, `CUSTOMER_PORTAL_ENDPOINTS`, `WORK_ORDER_ENDPOINTS`, `QUOTE_ENDPOINTS`, `INVENTORY_ENDPOINTS`, `INVOICE_ENDPOINTS`, `APPOINTMENT_ENDPOINTS`, `INSURANCE_ENDPOINTS`, etc.

---

## 14. Complete Screen Inventory (Route Names)

Use route names for deep links, E2E menu, and `screen-{Route}` assertions.

### Guest routes
`Welcome`, `Login`, `ForgotPassword`, `VerifyOTP`, `ResetPassword`, `CustomerPersonalInfo`, `VehicleInfo`, `ShopPersonalInfo`, `ShopBusinessDetails`, `TwoFactorLogin`, `SetPassword`, `ShopDiscovery`, `ShopDetail`

### Authenticated routes (role-filtered)
`Home`, `Onboarding`, `Settings`, `ChangePassword`, `Notifications`, `UserProfileEdit`, `TwoFactorSetup`, `WorkOrders`, `WorkOrderDetail`, `WorkOrderEdit`, `WorkOrderCreate`, `Customers`, `CustomerDetail`, `CustomerAdd`, `CustomerImport`, `Inventory`, `InventoryScan`, `PartAdd`, `PartDetail`, `PartEdit`, `VehicleAdd`, `VehicleEdit`, `Employees`, `EmployeeDetail`, `EmployeeAdd`, `BusinessProfile`, `Invoices`, `InvoiceDetail`, `InvoiceCreate`, `InvoicePayment`, `PaymentHistory`, `OpenQuoteRequests`, `OpenQuoteRequestDetail`, `RewardManagement`, `Appointments`, `AppointmentCreate`, `Reports`, `AiDiagnosis`, `Subscription`, `InsurancePolicies`, `InsurancePolicyDetail`, `InsurancePolicyCreate`, `InsuranceCompanyManagement`, `InsuranceClaims`, `InsuranceClaimDetail`, `InsuranceClaimCreate`, `SuperAdminDashboard`, `ClientAdd`, `ClientDetail`, `ClientEdit`, `ClientUsers`, `ClientUserDetail`, `Roles`, `RoleDetail`, `QuoteRequestCreate`, `QuoteRequests`, `QuoteRequestDetail`, `CustomerWorkOrders`, `CustomerWorkOrderDetail`, `Chat`, `Messages`, `CustomerVehicleAdd`, `MyVehicles`, `MaintenanceHistory`, `CustomerProfile`, `CustomerProfileEdit`, `CustomerInvoices`, `CustomerInvoiceDetail`, `CustomerAiDiagnosis`, `ShopOffers`, `Loyalty`, `Rewards`, `HelpCenter`, `CustomerInsurance`

---

## 15. Non-Functional Requirements

| Area | Requirement |
|------|-------------|
| Performance | Lists should show loading indicator; pull-to-refresh where implemented |
| Offline | Graceful error when API unreachable; no silent data loss on forms |
| Security | Tokens in secure storage; no secrets in repo; HTTPS in production |
| Accessibility | Touch targets ≥ 44pt where feasible; readable contrast on dark theme |
| Multi-tenant | Shop data isolated by `clientId` in JWT |

---

## 16. TestSprite / Automated Testing Guidance

### 16.1 Recommended configuration
| Field | Value |
|-------|-------|
| Type | `frontend` |
| Local port | `8081` |
| Path | *(empty)* |
| Scope | `codebase` |

### 16.2 Pre-run checklist
1. Start backend API on port **5169**
2. Seed database (admin user exists)
3. Run `npm run web` in project root
4. Verify `http://localhost:8081` loads Welcome or Login
5. Verify `http://localhost:5169/health` returns OK

### 16.3 Priority test cases (P0)
1. Guest: Welcome → Login screen navigation
2. Login: valid admin credentials → authenticated home
3. Login: invalid password → error banner
4. Authenticated: Settings screen loads
5. Shop: WorkOrders list screen mounts
6. Customer: QuoteRequests screen mounts (with customer account)
7. Guest: ShopDiscovery → ShopDetail

### 16.4 Known limitations (web runner)
- Biometric lock overlay may block login on dev builds—use clear state or web build without lock
- `InventoryScan`, camera, push notifications: skip or expect graceful failure
- Detail screens opened with fake IDs may show API errors—assert **screen container**, not data correctness
- Dev E2E floating menu exists only in **development builds**, not standard Expo Go

### 16.5 Alternative: Maestro (native)
For full native coverage, use `.maestro/` flows with `maestro test .maestro/login-guest.yaml -e LOGIN_EMAIL=... -e LOGIN_PASSWORD=...`

---

## 17. Success Metrics (Product)

| Metric | Target direction |
|--------|------------------|
| Quote response time | Shop responds within 24h (tracked backend) |
| Work order status updates | Customer sees progress without calling shop |
| AI diagnosis completion rate | >70% of started diagnoses completed |
| Shop retention | Monthly active shops per tenant |
| Customer NPS | Post-delivery rating on `Delivered` orders |

---

## 18. Glossary

| Term | Definition |
|------|------------|
| Client / Tenant | A repair shop organization on the platform |
| Work order | Repair job linked to customer, vehicle, status timeline |
| Quote request | Customer asks one or more shops for price estimate |
| Open quote | Shop-facing inbox of pending quote requests |
| Customer approval | `requiresCustomerApproval` flag for estimate sign-off |
| Trust score | Composite ranking for shops (rating, completion rate, response time) |

---

## 19. References (repository)

| Document | Path |
|----------|------|
| Product overview (TR) | `docs/PRODUCT_OVERVIEW.md` |
| Configuration | `docs/CONFIGURATION_GUIDE.md` |
| Backend repo | `docs/BACKEND_REPO.md` |
| Maestro E2E | `.maestro/README.md` |
| Agent architecture notes | `AGENTS.md` |
| Work order API notes | `docs/backend/WORK_ORDER_REQUIREMENTS.md` |

---

## 20. Document Approval

| Role | Name | Date |
|------|------|------|
| Product | — | 2026-05-28 |
| Engineering | — | 2026-05-28 |
| QA / TestSprite | Upload this file as Product Specification Doc | — |

---

*This PRD is derived from the MagicCarRepairMobile codebase and backend contracts as of May 2026. Update when routes, roles, or seed accounts change.*
