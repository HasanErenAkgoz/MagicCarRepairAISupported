using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    public static class HelpArticleSeedData
    {
        public static List<HelpArticle> GetHelpArticles()
        {
            var now = DateTime.UtcNow;

            return new List<HelpArticle>
            {
                // ── Getting Started (3 articles) ──────────────────────────────
                new HelpArticle
                {
                    Id = 1,
                    Title = "How to create your first work order",
                    Content = "Navigate to **Work Orders** from the dashboard and tap **+ New Work Order**. Select a customer, assign a vehicle, describe the issue, and save. The order will appear in your active queue ready for processing.",
                    Category = "Getting Started",
                    Order = 1,
                    IsPublished = true,
                    ViewCount = 0,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = 1
                },
                new HelpArticle
                {
                    Id = 2,
                    Title = "Setting up your shop profile",
                    Content = "Go to **Settings > Shop Profile** to add your business name, address, phone number, and logo. A complete profile builds trust with customers and appears on invoices and digital receipts.",
                    Category = "Getting Started",
                    Order = 2,
                    IsPublished = true,
                    ViewCount = 0,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = 1
                },
                new HelpArticle
                {
                    Id = 3,
                    Title = "Adding employees and setting permissions",
                    Content = "Open **Settings > Employees** and tap **Add Employee**. Enter their details and assign a role (Manager or Employee). Managers can access reports and settings while Employees focus on work orders and daily tasks.",
                    Category = "Getting Started",
                    Order = 3,
                    IsPublished = true,
                    ViewCount = 0,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = 1
                },

                // ── Work Orders (3 articles) ─────────────────────────────────
                new HelpArticle
                {
                    Id = 4,
                    Title = "Understanding work order statuses",
                    Content = "Work orders move through stages: **Appointment Scheduled** > **Vehicle Entered** > **Diagnosis** > **In Progress** > **Quality Control** > **Ready for Delivery** > **Delivered**. Each status change is logged in the timeline so you can track progress at a glance.",
                    Category = "Work Orders",
                    Order = 1,
                    IsPublished = true,
                    ViewCount = 0,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = 1
                },
                new HelpArticle
                {
                    Id = 5,
                    Title = "Adding parts and labor to a work order",
                    Content = "Inside a work order, switch to the **Parts & Labor** tab. Tap **Add Part** to pick from inventory or enter a custom item. Add labor lines with hours and rate. Totals update automatically including tax.",
                    Category = "Work Orders",
                    Order = 2,
                    IsPublished = true,
                    ViewCount = 0,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = 1
                },
                new HelpArticle
                {
                    Id = 6,
                    Title = "Sending work orders to customers for approval",
                    Content = "When a work order requires customer approval, toggle **Requires Customer Approval** on the order detail screen. The customer receives a push notification and can review the estimate, then approve or request changes directly from the app.",
                    Category = "Work Orders",
                    Order = 3,
                    IsPublished = true,
                    ViewCount = 0,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = 1
                },

                // ── Vehicles (2 articles) ────────────────────────────────────
                new HelpArticle
                {
                    Id = 7,
                    Title = "Adding and managing customer vehicles",
                    Content = "Open a customer profile and tap **Add Vehicle**. Enter the license plate, make, model, year, and current mileage. Vehicles are linked to the customer and automatically available when creating work orders.",
                    Category = "Vehicles",
                    Order = 1,
                    IsPublished = true,
                    ViewCount = 0,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = 1
                },
                new HelpArticle
                {
                    Id = 8,
                    Title = "Viewing vehicle maintenance history",
                    Content = "Select a vehicle from the customer profile to see its full service history. Every completed work order is listed chronologically with parts used, labor performed, and total cost. Use this to recommend upcoming maintenance.",
                    Category = "Vehicles",
                    Order = 2,
                    IsPublished = true,
                    ViewCount = 0,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = 1
                },

                // ── Account (2 articles) ─────────────────────────────────────
                new HelpArticle
                {
                    Id = 9,
                    Title = "Changing your password",
                    Content = "Go to **Settings > Change Password**. Enter your current password, then type and confirm your new password. For security, passwords must be at least 8 characters and include a mix of letters and numbers.",
                    Category = "Account",
                    Order = 1,
                    IsPublished = true,
                    ViewCount = 0,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = 1
                },
                new HelpArticle
                {
                    Id = 10,
                    Title = "Managing notification settings",
                    Content = "Visit **Settings > Notifications** to control which alerts you receive. You can toggle push notifications for new work orders, status updates, customer messages, and payment confirmations independently.",
                    Category = "Account",
                    Order = 2,
                    IsPublished = true,
                    ViewCount = 0,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = 1
                },

                // ── Payments (2 articles) ────────────────────────────────────
                new HelpArticle
                {
                    Id = 11,
                    Title = "Creating and sending invoices",
                    Content = "Once a work order is marked **Ready for Delivery**, open it and tap **Generate Invoice**. The invoice is created from the parts and labor on the order. You can send it to the customer via the app or export it as a PDF.",
                    Category = "Payments",
                    Order = 1,
                    IsPublished = true,
                    ViewCount = 0,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = 1
                },
                new HelpArticle
                {
                    Id = 12,
                    Title = "Tracking payment status",
                    Content = "The **Billing** section on the dashboard shows all outstanding and completed payments. Each invoice displays its status (Pending, Paid, Overdue). Tap an invoice to record a payment or send a reminder to the customer.",
                    Category = "Payments",
                    Order = 2,
                    IsPublished = true,
                    ViewCount = 0,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = 1
                }
            };
        }
    }
}
