# Web Admin Panel (Next.js) — Implementation Guide

This document describes how to run a **web-based Admin/Shop panel** for MagicCarRepair using the existing .NET backend (`Core.Packages.WebAPI`) and its JWT authentication flow.

## Scope (MVP)

- **Persona**: Shop Admin side (`Manager` / `Employee`)
- **Auth**: Reuse existing backend JWT login (`POST /api/Auth/login`)
- **Backend**: Existing .NET Web API running locally (default `http://localhost:5169`)
- **Frontend**: New Next.js app (App Router) in `C:\\Projects\\MagicCarRepairWebAdmin`

## Feature mapping (Mobile → Web)

Mobile admin screens live in `MagicCarRepairMobile/src/screens/` and are registered in `MagicCarRepairMobile/src/navigation/RootNavigator.tsx`.

Web routes are proposed as:

- **Login**
  - Mobile: `LoginScreen`
  - Web: `/login`
  - API: `POST /api/Auth/login` (`AUTH_ENDPOINTS.login`)

- **Dashboard**
  - Mobile: `AdminDashboardScreen`
  - Web: `/dashboard`
  - API: `GET /api/Dashboard/stats`, `GET /api/Dashboard/weekly-revenue`, `GET /api/Dashboard/fleet-status`

- **Work orders**
  - Mobile: `WorkOrderManagementScreen`, `WorkOrderDetailScreen`, `WorkOrderEditScreen`, `WorkOrderCreateScreen`
  - Web: `/work-orders`, `/work-orders/[id]`, `/work-orders/[id]/edit`, `/work-orders/new`
  - API: `WORK_ORDER_ENDPOINTS.*`

- **Customers**
  - Mobile: `CustomersScreen`, `CustomerDetailScreen`, `CustomerAddScreen`
  - Web: `/customers`, `/customers/[id]`, `/customers/new`
  - API: `CUSTOMER_ENDPOINTS.*`

- **Vehicles (via customer detail)**
  - Mobile: `VehicleAddScreen`, `VehicleEditScreen`
  - Web: nested actions under `/customers/[id]` (or `/vehicles/[id]` later)
  - API: `VEHICLE_ENDPOINTS.*`

- **Inventory / Parts**
  - Mobile: `InventoryScreen`, `InventoryScanScreen`, `PartAddScreen`, `PartDetailScreen`, `PartEditScreen`
  - Web: `/inventory/parts`, `/inventory/parts/new`, `/inventory/parts/[id]`, `/inventory/parts/[id]/edit`
  - API: `INVENTORY_ENDPOINTS.*`

- **Employees**
  - Mobile: `EmployeesScreen`, `EmployeeDetailScreen`, `EmployeeAddScreen`
  - Web: `/employees`, `/employees/new`, `/employees/[id]`
  - API: `EMPLOYEE_ENDPOINTS.*`

- **Insurance**
  - Mobile: `InsurancePoliciesScreen`, `InsurancePolicyDetailScreen`, `InsurancePolicyCreateScreen`, `InsuranceClaimsScreen`, `InsuranceClaimDetailScreen`, `InsuranceClaimCreateScreen`, `InsuranceCompanyManagementScreen`
  - Web: `/insurance/policies`, `/insurance/policies/[id]`, `/insurance/policies/new`, `/insurance/claims`, `/insurance/claims/[id]`
  - API: `/api/Insurance/*` (plus any list/detail endpoints used by mobile `insuranceService.ts`)

- **Billing / Invoices**
  - Mobile: `InvoiceListScreen`, `InvoiceDetailScreen`, `InvoiceCreateScreen`, `InvoicePaymentScreen`, `PaymentHistoryScreen`
  - Web: `/billing/invoices`, `/billing/invoices/[id]`, `/billing/invoices/new`
  - API: backend invoice endpoints (used by mobile billing services)

- **Notifications**
  - Mobile: `NotificationsScreen`
  - Web: `/notifications`
  - API: `NOTIFICATION_ENDPOINTS.*`

- **Settings**
  - Mobile: `SettingsScreen`
  - Web: `/settings`
  - API: mostly client-side (language/currency); optionally `TRANSLATION_ENDPOINTS.*`

## Environment configuration

### Backend

Run the API:

```powershell
dotnet run --project .\\Core.Packages.WebAPI\\MagicCarRepairAISupported.WebAPI.csproj
```

Swagger (dev): `http://localhost:5169/swagger`

### Web (Next.js)

- `NEXT_PUBLIC_API_BASE_URL=http://localhost:5169/api`
- Run dev server:

```powershell
npm run dev
```

## API contract expectations

- Most endpoints respond using the wrapper:
  - `{ success: boolean, data: T, message?: string }`
- Web should implement a single `apiFetch` helper that:
  - Adds `Authorization: Bearer <token>`
  - Adds `Accept-Language`
  - Normalizes error bodies into a typed error (`ApiError`)

## Auth + token storage (MVP vs hardened)

- MVP: store access token in `localStorage` and use route guards.
- Hardened: move access token to memory and use refresh strategy (or adopt a BFF pattern).

