"""Registry of backend API test modules (execution order)."""

from __future__ import annotations

from dataclasses import dataclass


@dataclass(frozen=True)
class ModuleInfo:
    code: str
    name: str
    folder: str
    priority: str
    controllers: tuple[str, ...]


MODULES: tuple[ModuleInfo, ...] = (
    ModuleInfo("M01", "platform", "M01_platform", "P0", ()),
    ModuleInfo("M02", "auth", "M02_auth", "P0", ("Auth", "TwoFactor", "User", "Onboarding", "DeviceTokens")),
    ModuleInfo("M03", "rbac", "M03_rbac", "P0", ("Clients", "Role", "Permission")),
    ModuleInfo("M04", "customers", "M04_customers", "P0", ("Customers", "CustomerAccount")),
    ModuleInfo("M05", "vehicles", "M05_vehicles", "P0", ("Vehicles",)),
    ModuleInfo("M06", "workorders", "M06_workorders", "P0", ("WorkOrders",)),
    ModuleInfo("M07", "parts_stock", "M07_parts_stock", "P0", ("Parts", "StockMovements", "StockAlerts", "PartSuppliers", "AutoOrders", "QRCode")),
    ModuleInfo("M08", "appointments", "M08_appointments", "P1", ("Appointments", "Reminders")),
    ModuleInfo("M09", "quotes", "M09_quotes", "P1", ("QuoteRequests", "QuoteRequest")),
    ModuleInfo("M10", "invoices_payments", "M10_invoices_payments", "P0", ("Invoices", "Payments", "MobilePayments", "Commission")),
    ModuleInfo("M11", "accounting", "M11_accounting", "P1", ("Incomes", "Expenses", "Taxes", "SalaryPayments", "AccountingReports")),
    ModuleInfo("M12", "dashboard_reports", "M12_dashboard_reports", "P1", ("Dashboard", "Reports")),
    ModuleInfo("M13", "employees", "M13_employees", "P0", ("Employees",)),
    ModuleInfo("M14", "insurance", "M14_insurance", "P2", ("Insurance",)),
    ModuleInfo("M15", "notifications_comms", "M15_notifications_comms", "P1", ("Notifications", "Chat", "WhatsApp")),
    ModuleInfo("M16", "loyalty_ratings", "M16_loyalty_ratings", "P2", ("Loyalty", "Rewards", "Ratings", "ServiceRatings")),
    ModuleInfo("M17", "ai", "M17_ai", "P2", ("AI",)),
    ModuleInfo("M18", "audit_sync", "M18_audit_sync", "P2", ("AuditLogs", "Sync")),
    ModuleInfo("M19", "portals", "M19_portals", "P1", ("CustomerPortal", "ClientPortal", "PublicClient", "PublicClients")),
    ModuleInfo("M20", "utilities", "M20_utilities", "P2", ("Files", "Translate", "Translation", "ErrorMessages", "Help", "Subscription")),
)


def find_module(query: str) -> ModuleInfo | None:
    q = query.strip().lower()
    for module in MODULES:
        if module.code.lower() == q or module.folder.lower() == q or module.name.lower() == q:
            return module
        if q in module.folder.lower():
            return module
    return None
