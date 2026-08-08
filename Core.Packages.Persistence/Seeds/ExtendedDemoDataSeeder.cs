using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    /// <summary>
    /// Temel seed tamamlandıktan sonra kalan tabloları gerçekçi demo verilerle doldurur.
    /// Veriler <b>tüm kiracılara</b> dağıtılır; iş emri/müşteri/parça gibi FK gerektiren kayıtlar yalnızca ilgili verinin olduğu tenant’ta üretilir (idempotent).
    /// </summary>
    public static class ExtendedDemoDataSeeder
    {
        private const int SeedUserId = 1;
        /// <summary>Müşteri / iş emri / parça seed verisinin bulunduğu varsayılan tenant (mevcut JSON seed).</summary>
        private const int PrimaryDataTenantId = 1;

        private static string? ManagerEmailForClient(int clientId) => clientId switch
        {
            1 => "manager@demo.com",
            2 => "manager2@test.com",
            3 => "manager@yildizoto.com.tr",
            4 => "manager@anadoluoto.com.tr",
            5 => "manager@bogazoto.com.tr",
            6 => "gunesoto@gmail.com",
            7 => "info@prestijautomotive.com",
            _ => null
        };

        public static async Task SeedAsync(BaseDbContext context, ILogger logger, CancellationToken cancellationToken = default)
        {
            var clientIds = await context.Clients.AsNoTracking().OrderBy(c => c.Id).Select(c => c.Id).ToListAsync(cancellationToken);
            if (clientIds.Count == 0)
            {
                logger.LogWarning("Extended demo seed skipped: no clients in database.");
                return;
            }

            await SeedPartSuppliersAsync(context, logger, clientIds, cancellationToken);
            await SeedInsuranceAsync(context, logger, clientIds, cancellationToken);
            await SeedStockMovementsAsync(context, logger, clientIds, cancellationToken);
            await SeedWorkOrderDetailsAsync(context, logger, clientIds, cancellationToken);
            await SeedQuoteFlowAsync(context, logger, clientIds, cancellationToken);
            await SeedInvoicesAndPaymentsAsync(context, logger, clientIds, cancellationToken);
            await SeedTaxAndFinanceAsync(context, logger, clientIds, cancellationToken);
            await SeedAppointmentsAsync(context, logger, clientIds, cancellationToken);
            await SeedNotificationsAsync(context, logger, clientIds, cancellationToken);
            await SeedStockAlertsAndAutoOrdersAsync(context, logger, clientIds, cancellationToken);
            await SeedChatAndRatingsAsync(context, logger, clientIds, cancellationToken);
            await SeedPortfolioAuditUsageAsync(context, logger, clientIds, cancellationToken);
            await SeedSubscriptionAndSalaryAsync(context, logger, clientIds, cancellationToken);
            await SeedHelpLoyaltyRemindersAsync(context, logger, clientIds, cancellationToken);
            await SeedPartPhotosAsync(context, logger, clientIds, cancellationToken);
            await SeedUserDevicesAsync(context, logger, clientIds, cancellationToken);
        }

        private static async Task SeedPartSuppliersAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            var now = DateTime.UtcNow;
            var added = 0;
            foreach (var cid in clientIds)
            {
                if (await context.Set<PartSupplier>().AnyAsync(s => s.ClientId == cid, ct))
                    continue;

                var tag = $"T{cid}";
                var suppliers = new List<PartSupplier>
                {
                    new()
                    {
                        CompanyName = $"Bölgesel Parça Dağıtım ({tag})",
                        ContactPerson = "Serkan Tekin",
                        Phone = $"+90 216 555 {1000 + cid:D4}",
                        Email = $"siparis.t{cid}@demo-parca.local",
                        Address = "Tuzla OSB, Parça Cad. No:12",
                        City = "İstanbul",
                        Country = "TR",
                        TaxNumber = $"{1000000000 + cid}",
                        TaxOffice = "Tuzla",
                        PaymentTerms = "30 gün vade",
                        Notes = "Bosch, Mann Filter — tenant sipariş hattı",
                        IsActive = true,
                        ClientId = cid,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    },
                    new()
                    {
                        CompanyName = $"Yedek Parça Lojistik ({tag})",
                        ContactPerson = "Hakan Yılmaz",
                        Phone = $"+90 312 555 {2000 + cid:D4}",
                        Email = $"lojistik.t{cid}@demo-parca.local",
                        Address = "Ostim Sanayi 1234. Sok. No:8",
                        City = "Ankara",
                        Country = "TR",
                        TaxNumber = $"{2000000000 + cid}",
                        TaxOffice = "Ostim",
                        PaymentTerms = "Peşin / 15 gün",
                        Notes = "Amortisör ve süspansiyon tedarikçisi",
                        IsActive = true,
                        ClientId = cid,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    },
                    new()
                    {
                        CompanyName = $"Elektrik & Akü Tedarik ({tag})",
                        ContactPerson = "Aylin Koç",
                        Phone = $"+90 232 555 {3000 + cid:D4}",
                        Email = $"elektrik.t{cid}@demo-parca.local",
                        Address = "Bornova Sanayi Sitesi B Blok",
                        City = "İzmir",
                        Country = "TR",
                        TaxNumber = $"{3000000000 + cid}",
                        TaxOffice = "Konak",
                        PaymentTerms = "EFT 7 gün",
                        Notes = "Akü, marş, alternatör",
                        IsActive = true,
                        ClientId = cid,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    }
                };

                await context.Set<PartSupplier>().AddRangeAsync(suppliers, ct);
                added += suppliers.Count;
            }

            if (added > 0)
                await context.SaveChangesAsync(ct);
            logger.LogInformation("Extended seed: part suppliers — {Added} row(s) across {TenantCount} tenant(s).", added, clientIds.Count);
        }

        private static async Task SeedInsuranceAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            var now = DateTime.UtcNow;
            foreach (var cid in clientIds)
            {
                if (await context.Set<InsuranceCompany>().AnyAsync(i => i.ClientId == cid, ct))
                    continue;

                var companies = new List<InsuranceCompany>
                {
                    new()
                    {
                        CompanyName = $"Anadolu Sigorta — Şube {cid}",
                        CompanyCode = $"ANADOLU-SIG-C{cid}",
                        ContactPerson = "Hasan Yurt",
                        Phone = $"+90 216 555 {100 + cid:D4}",
                        Email = $"hasar.t{cid}@demo-sigorta.local",
                        Address = "Kadıköy, İstanbul",
                        IsActive = true,
                        SupportedInsuranceTypes = """["Comprehensive","TrafficInsurance"]""",
                        ClientId = cid,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    },
                    new()
                    {
                        CompanyName = $"Sompo Partner ({cid})",
                        CompanyCode = $"SOMPO-C{cid}",
                        ContactPerson = "Deniz Aksoy",
                        Phone = $"+90 212 555 {200 + cid:D4}",
                        Email = $"servis.t{cid}@demo-sompo.local",
                        Address = "Maslak, İstanbul",
                        IsActive = true,
                        SupportedInsuranceTypes = """["Comprehensive"]""",
                        ClientId = cid,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    }
                };

                await context.Set<InsuranceCompany>().AddRangeAsync(companies, ct);
            }

            await context.SaveChangesAsync(ct);

            // Poliçe / hasar: yalnızca müşteri+araç seed'i olan tenant (varsayılan: Client 1)
            const int policyClientId = 1;
            if (!clientIds.Contains(policyClientId))
            {
                logger.LogInformation("Extended seed: insurance policies skipped (tenant {Id} not in seed set).", policyClientId);
                return;
            }

            if (await context.Set<InsurancePolicy>().AnyAsync(p => p.ClientId == policyClientId, ct))
            {
                logger.LogInformation("Extended seed: insurance policies already present for tenant {Id}.", policyClientId);
                return;
            }

            if (!await context.Customers.AnyAsync(c => c.ClientId == policyClientId, ct))
            {
                logger.LogInformation("Extended seed: insurance policies skipped (no customers for tenant {Id}).", policyClientId);
                return;
            }

            var ic1 = await context.Set<InsuranceCompany>().FirstAsync(c => c.CompanyCode == $"ANADOLU-SIG-C{policyClientId}", ct);
            var ic2 = await context.Set<InsuranceCompany>().FirstAsync(c => c.CompanyCode == $"SOMPO-C{policyClientId}", ct);

            var policies = new List<InsurancePolicy>
            {
                new()
                {
                    PolicyNumber = $"POL-2025-{policyClientId}-001234",
                    VehicleId = 1,
                    CustomerId = 1,
                    InsuranceCompanyId = ic1.Id,
                    InsuranceType = InsuranceType.Comprehensive,
                    StartDate = now.AddMonths(-8),
                    EndDate = now.AddMonths(4),
                    PremiumAmount = 18500m,
                    CoverageAmount = 850000m,
                    DeductiblePercentage = 2,
                    DeductibleAmount = 2500m,
                    Status = InsuranceStatus.Active,
                    Notes = "Kasko — cam muafiyeti yok",
                    ClientId = policyClientId,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                },
                new()
                {
                    PolicyNumber = $"TRF-2025-{policyClientId}-998877",
                    VehicleId = 3,
                    CustomerId = 2,
                    InsuranceCompanyId = ic2.Id,
                    InsuranceType = InsuranceType.TrafficInsurance,
                    StartDate = now.AddMonths(-11),
                    EndDate = now.AddMonths(1),
                    PremiumAmount = 4200m,
                    CoverageAmount = null,
                    DeductiblePercentage = 0,
                    Status = InsuranceStatus.Active,
                    Notes = "Zorunlu trafik",
                    ClientId = policyClientId,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                }
            };

            await context.Set<InsurancePolicy>().AddRangeAsync(policies, ct);
            await context.SaveChangesAsync(ct);

            var pol1 = await context.Set<InsurancePolicy>().FirstAsync(p => p.PolicyNumber == $"POL-2025-{policyClientId}-001234", ct);
            var claims = new List<InsuranceClaim>
            {
                new()
                {
                    ClaimNumber = $"HSR-2025-{policyClientId}-045612",
                    WorkOrderId = 1,
                    InsurancePolicyId = pol1.Id,
                    DamageDate = now.AddDays(-32),
                    DamageDescription = "Ön tampon ve far hasarı — park çarpması",
                    DamageAmount = 12000m,
                    ApprovedAmount = 11500m,
                    DeductibleAmount = 2300m,
                    PayableAmount = 9200m,
                    Status = ClaimStatus.Paid,
                    ApprovalDate = now.AddDays(-30),
                    PaymentDate = now.AddDays(-28),
                    Notes = "Anlaşmalı servis ödemesi tamamlandı",
                    ClientId = policyClientId,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                }
            };

            await context.Set<InsuranceClaim>().AddRangeAsync(claims, ct);
            await context.SaveChangesAsync(ct);
            logger.LogInformation("Extended seed: insurance companies (all tenants), policies/claims (tenant {Id}).", policyClientId);
        }

        private static async Task SeedStockMovementsAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            var now = DateTime.UtcNow;
            var total = 0;
            foreach (var cid in clientIds)
            {
                if (await context.Set<StockMovement>().AnyAsync(m => m.ClientId == cid, ct))
                    continue;

                var partIds = await context.Set<Part>().Where(p => p.ClientId == cid).OrderBy(p => p.Id).Select(p => p.Id).Take(5).ToListAsync(ct);
                if (partIds.Count == 0)
                    continue;

                var p0 = partIds[0];
                var p1 = partIds.Count > 1 ? partIds[1] : p0;
                var p2 = partIds.Count > 2 ? partIds[2] : p0;
                var movements = new List<StockMovement>
                {
                    StockMovement.Create(p0, StockMovementType.In, 10, 3, "Tedarikçi girişi", $"PO-2025-{cid}-001", "Purchase", 180m, cid),
                    StockMovement.Create(p0, StockMovementType.Out, 2, 4, "İş emri — fren balata", $"WO-{cid}", "WorkOrder", 280m, cid),
                    StockMovement.Create(p2, StockMovementType.Out, 1, 3, "Periyodik bakım — filtre", $"WO-{cid}-B", "WorkOrder", 75m, cid),
                    StockMovement.Create(p1, StockMovementType.In, 24, 3, "Kampanya alımı", $"PO-2025-{cid}-014", "Purchase", 120m, cid)
                };

                foreach (var m in movements)
                {
                    m.MovementDate = now.AddDays(-Random.Shared.Next(1, 40));
                    m.CreatedDate = m.MovementDate;
                    m.CreatedBy = SeedUserId;
                    m.Status = Status.Active;
                }

                await context.Set<StockMovement>().AddRangeAsync(movements, ct);
                total += movements.Count;
            }

            if (total > 0)
                await context.SaveChangesAsync(ct);
            logger.LogInformation("Extended seed: {Count} stock movement row(s) (tenants with Parts only).", total);
        }

        private static async Task SeedWorkOrderDetailsAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            if (!clientIds.Contains(PrimaryDataTenantId)) return;
            if (await context.Set<WorkOrderItem>().AnyAsync(i => i.ClientId == PrimaryDataTenantId, ct)) return;

            var now = DateTime.UtcNow;
            var items = new List<WorkOrderItem>();

            void AddPart(int woId, int partId, string desc, decimal qty, decimal unit)
            {
                var wi = new WorkOrderItem
                {
                    WorkOrderId = woId,
                    ItemType = WorkOrderItemType.Part,
                    PartId = partId,
                    Description = desc,
                    Quantity = qty,
                    UnitPrice = unit,
                    TaxRate = 20m,
                    BrandType = PartBrandType.Original,
                    WarrantyMonths = 12,
                    ClientId = PrimaryDataTenantId,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                };
                wi.CalculateTotal();
                items.Add(wi);
            }

            AddPart(1, 1, "Ön fren balata takımı (Bosch)", 1, 280m);
            AddPart(1, 4, "Yağ filtresi", 1, 60m);
            AddPart(2, 3, "Hava filtresi", 1, 75m);
            AddPart(3, 5, "Motor yağı 5W-30 (5L)", 1, 180m);
            AddPart(4, 8, "Far ampulü H7", 2, 45m);
            AddPart(5, 2, "Arka fren balata", 1, 230m);

            items.Add(new WorkOrderItem
            {
                WorkOrderId = 1,
                ItemType = WorkOrderItemType.Labor,
                Description = "Fren sistemi kontrol ve işçilik",
                Quantity = 2.5m,
                UnitPrice = 450m,
                TaxRate = 20m,
                ClientId = PrimaryDataTenantId,
                Status = Status.Active,
                CreatedDate = now,
                CreatedBy = SeedUserId
            });
            items[^1].CalculateTotal();

            items.Add(new WorkOrderItem
            {
                WorkOrderId = 3,
                ItemType = WorkOrderItemType.ExternalService,
                Description = "Dışarıda rot balans hizmeti",
                Quantity = 1,
                UnitPrice = 350m,
                TaxRate = 20m,
                ClientId = PrimaryDataTenantId,
                Status = Status.Active,
                CreatedDate = now,
                CreatedBy = SeedUserId
            });
            items[^1].CalculateTotal();

            await context.Set<WorkOrderItem>().AddRangeAsync(items, ct);
            await context.SaveChangesAsync(ct);

            var labors = new List<WorkOrderLabor>
            {
                new()
                {
                    WorkOrderId = 1,
                    EmployeeId = 3,
                    OperationName = "Fren değişimi",
                    StartTime = now.AddDays(-30).AddHours(9),
                    EndTime = now.AddDays(-30).AddHours(12),
                    DurationHours = 3m,
                    HourlyRate = 450m,
                    TotalAmount = 1350m,
                    Description = "Ön aks fren revizyonu",
                    ClientId = PrimaryDataTenantId,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                },
                new()
                {
                    WorkOrderId = 7,
                    EmployeeId = 4,
                    OperationName = "Motor arıza tespiti",
                    StartTime = now.AddDays(-7).AddHours(10),
                    EndTime = now.AddDays(-7).AddHours(13),
                    DurationHours = 3m,
                    HourlyRate = 400m,
                    TotalAmount = 1200m,
                    Description = "OBD teşhis ve test sürüşü",
                    ClientId = PrimaryDataTenantId,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                }
            };

            await context.Set<WorkOrderLabor>().AddRangeAsync(labors, ct);

            var timelines = new List<WorkOrderTimeline>
            {
                new()
                {
                    WorkOrderId = 1,
                    EventDate = now.AddDays(-30),
                    OldStatus = null,
                    NewStatus = WorkOrderStatus.VehicleEntered,
                    StatusChange = WorkOrderStatus.VehicleEntered,
                    EmployeeId = 2,
                    Description = "Araç kabul edildi, şikayet kaydedildi",
                    EventType = "StatusChange",
                    ClientId = PrimaryDataTenantId,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                },
                new()
                {
                    WorkOrderId = 1,
                    EventDate = now.AddDays(-29),
                    OldStatus = WorkOrderStatus.VehicleEntered,
                    NewStatus = WorkOrderStatus.InRepair,
                    StatusChange = WorkOrderStatus.InRepair,
                    EmployeeId = 3,
                    Description = "Fren sisteminde revizyon başlatıldı",
                    EventType = "StatusChange",
                    ClientId = PrimaryDataTenantId,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                },
                new()
                {
                    WorkOrderId = 1,
                    EventDate = now.AddDays(-28),
                    OldStatus = WorkOrderStatus.InRepair,
                    NewStatus = WorkOrderStatus.Delivered,
                    StatusChange = WorkOrderStatus.Delivered,
                    EmployeeId = 2,
                    Description = "Teslimat yapıldı, müşteri memnun",
                    EventType = "StatusChange",
                    ClientId = PrimaryDataTenantId,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                }
            };

            await context.Set<WorkOrderTimeline>().AddRangeAsync(timelines, ct);
            await context.SaveChangesAsync(ct);

            var t1 = await context.Set<WorkOrderTimeline>().OrderBy(t => t.Id).FirstAsync(t => t.WorkOrderId == 1, ct);
            var photos = new List<WorkOrderPhoto>
            {
                new()
                {
                    WorkOrderId = 1,
                    TimelineId = t1.Id,
                    FilePath = "https://images.unsplash.com/photo-1486262715619-67b85e0b08d3?w=800",
                    Description = "Giriş — ön tampon genel",
                    PhotoType = WorkOrderPhotoType.Entry,
                    UploadDate = now.AddDays(-30),
                    UploadedByEmployeeId = 3,
                    ClientId = PrimaryDataTenantId,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                },
                new()
                {
                    WorkOrderId = 1,
                    FilePath = "https://images.unsplash.com/photo-1619642751034-765dfdf7c58e?w=800",
                    Description = "İşlem — fren diski",
                    PhotoType = WorkOrderPhotoType.Process,
                    UploadDate = now.AddDays(-29),
                    UploadedByEmployeeId = 3,
                    ClientId = PrimaryDataTenantId,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                },
                new()
                {
                    WorkOrderId = 7,
                    FilePath = "https://images.unsplash.com/photo-1625047509168-a7026f36de04?w=800",
                    Description = "Motor bölümü genel",
                    PhotoType = WorkOrderPhotoType.Process,
                    UploadDate = now.AddDays(-7),
                    UploadedByEmployeeId = 4,
                    ClientId = PrimaryDataTenantId,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                }
            };

            await context.Set<WorkOrderPhoto>().AddRangeAsync(photos, ct);
            await context.SaveChangesAsync(ct);
            logger.LogInformation("Extended seed: work order items, labors, timeline, photos.");
        }

        private static async Task SeedQuoteFlowAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            if (!clientIds.Contains(PrimaryDataTenantId)) return;
            if (await context.Set<QuoteRequest>().AnyAsync(q => q.ClientId == PrimaryDataTenantId, ct)) return;

            var now = DateTime.UtcNow;
            var qr1 = new QuoteRequest
            {
                RequestNumber = $"QR-{now:yyyyMMdd}-0001",
                CustomerId = 1,
                CustomerName = "Ahmet Yılmaz",
                VehicleId = 1,
                VehicleBrand = "Toyota",
                VehicleModel = "Corolla",
                VehicleLicensePlate = "34ABC123",
                PhotoPaths = """["/demo/damage1.jpg"]""",
                Description = "Sağ arka çamurlukta çizik ve küçük göçük — boya düşünüyorum.",
                RequestType = QuoteRequestType.Paint,
                UrgencyLevel = UrgencyLevel.Normal,
                QuoteDeadline = now.AddDays(5),
                Status = QuoteStatus.Open,
                EstimatedCost = 8500m,
                EstimatedDescription = "AI tahmini: lokal boya + macun",
                ClientId = PrimaryDataTenantId,
                CreatedDate = now.AddDays(-2),
                CreatedBy = SeedUserId
            };

            var qr2 = new QuoteRequest
            {
                RequestNumber = $"QR-{now:yyyyMMdd}-0002",
                CustomerId = 3,
                CustomerName = "Ayşe Kaya",
                VehicleId = 4,
                VehicleBrand = "Ford",
                VehicleModel = "Focus",
                VehicleLicensePlate = "34JKL012",
                PhotoPaths = "[]",
                Description = "Klima gazı düşük, soğutmuyor.",
                RequestType = QuoteRequestType.Malfunction,
                UrgencyLevel = UrgencyLevel.High,
                QuoteDeadline = now.AddDays(3),
                Status = QuoteStatus.QuotesReceived,
                ClientId = PrimaryDataTenantId,
                CreatedDate = now.AddDays(-4),
                CreatedBy = SeedUserId
            };

            await context.Set<QuoteRequest>().AddRangeAsync(new[] { qr1, qr2 }, ct);
            await context.SaveChangesAsync(ct);

            var q1 = await context.Set<QuoteRequest>().FirstAsync(q => q.RequestNumber == qr1.RequestNumber, ct);
            var q2 = await context.Set<QuoteRequest>().FirstAsync(q => q.RequestNumber == qr2.RequestNumber, ct);

            var responses = new List<QuoteResponse>
            {
                new()
                {
                    QuoteRequestId = q1.Id,
                    ClientId = PrimaryDataTenantId,
                    EmployeeId = 2,
                    Amount = 9240m,
                    QuoteAmount = 7700m,
                    TaxRate = 20m,
                    Description = "Lokal boya, çizik giderme, vernik",
                    ValidityDays = 7,
                    EstimatedDays = 3,
                    WarrantyMonths = 12,
                    QuoteDate = now.AddDays(-1),
                    ValidUntilDate = now.AddDays(6),
                    QuoteNumber = $"QN-{now:yyyyMMdd}-00001",
                    Status = "Pending",
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                },
                new()
                {
                    QuoteRequestId = q2.Id,
                    ClientId = PrimaryDataTenantId,
                    EmployeeId = 2,
                    Amount = 4200m,
                    QuoteAmount = 3500m,
                    TaxRate = 20m,
                    Description = "Klima gazı, kompresör kontrolü, polen filtresi",
                    ValidityDays = 5,
                    EstimatedDays = 1,
                    WarrantyMonths = 6,
                    QuoteDate = now.AddDays(-3),
                    Status = "Accepted",
                    QuoteNumber = $"QN-{now:yyyyMMdd}-00002",
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                }
            };

            await context.Set<QuoteResponse>().AddRangeAsync(responses, ct);
            await context.SaveChangesAsync(ct);

            var qrp = new List<QuoteRequestPhoto>
            {
                new()
                {
                    QuoteRequestId = q1.Id,
                    FilePath = "https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?w=800",
                    Description = "Çamurluk hasarı",
                    PhotoType = QuoteRequestPhotoType.Damage,
                    UploadDate = now.AddDays(-2),
                    UploadedByEmployeeId = 2,
                    ClientId = PrimaryDataTenantId,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                }
            };

            await context.Set<QuoteRequestPhoto>().AddRangeAsync(qrp, ct);
            await context.SaveChangesAsync(ct);
            logger.LogInformation("Extended seed: quote requests, responses, photos.");
        }

        private static async Task SeedInvoicesAndPaymentsAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            if (!clientIds.Contains(PrimaryDataTenantId)) return;
            if (await context.Set<Invoice>().AnyAsync(i => i.ClientId == PrimaryDataTenantId, ct)) return;

            var now = DateTime.UtcNow;
            var inv1 = new Invoice
            {
                InvoiceNumber = $"INV-{now.AddDays(-28):yyyyMMdd}-1001",
                InvoiceType = InvoiceType.Sales,
                WorkOrderId = 1,
                CustomerId = 1,
                InvoiceDate = now.AddDays(-28),
                DueDate = now.AddDays(-21),
                SubTotal = 2458.33m,
                TaxAmount = 491.67m,
                TotalAmount = 2950m,
                PaidAmount = 2950m,
                Status = InvoiceStatus.Paid,
                Description = "İş emri WO — fren ve filtre",
                ClientId = PrimaryDataTenantId,
                CreatedDate = now,
                CreatedBy = SeedUserId
            };

            var inv2 = new Invoice
            {
                InvoiceNumber = $"INV-{now.AddDays(-10):yyyyMMdd}-1002",
                InvoiceType = InvoiceType.Purchase,
                SupplierId = (await context.Set<PartSupplier>().FirstAsync(s => s.ClientId == PrimaryDataTenantId, ct)).Id,
                InvoiceDate = now.AddDays(-10),
                DueDate = now.AddDays(-3),
                SubTotal = 5000m,
                TaxAmount = 1000m,
                TotalAmount = 6000m,
                PaidAmount = 6000m,
                Status = InvoiceStatus.Paid,
                Description = "Toplu parça alımı — fren ve filtre",
                ClientId = PrimaryDataTenantId,
                CreatedDate = now,
                CreatedBy = SeedUserId
            };

            await context.Set<Invoice>().AddRangeAsync(new[] { inv1, inv2 }, ct);
            await context.SaveChangesAsync(ct);

            var persisted = await context.Set<Invoice>().Where(i => i.ClientId == PrimaryDataTenantId).OrderBy(i => i.Id).ToListAsync(ct);
            var pi1 = persisted[0];

            var line1 = new InvoiceItem
            {
                InvoiceId = pi1.Id,
                Description = "Fren balata + işçilik paketi",
                Quantity = 1,
                UnitPrice = 2000m,
                TaxRate = 20m,
                ClientId = PrimaryDataTenantId,
                CreatedDate = now,
                CreatedBy = SeedUserId
            };
            line1.CalculateTotal();
            line1.Status = Status.Active;

            var line2 = new InvoiceItem
            {
                InvoiceId = pi1.Id,
                Description = "KDV ve sarf",
                Quantity = 1,
                UnitPrice = 381.94m,
                TaxRate = 20m,
                ClientId = PrimaryDataTenantId,
                CreatedDate = now,
                CreatedBy = SeedUserId
            };
            line2.CalculateTotal();
            line2.Status = Status.Active;

            await context.Set<InvoiceItem>().AddRangeAsync(new[] { line1, line2 }, ct);
            await context.SaveChangesAsync(ct);

            var pay1 = new Payment
            {
                PaymentNumber = Payment.GeneratePaymentNumber(),
                InvoiceId = pi1.Id,
                WorkOrderId = 1,
                CustomerId = 1,
                Amount = 2950m,
                PaymentMethod = PaymentMethod.CreditCard,
                PaymentStatus = PaymentStatus.Paid,
                PaymentDate = now.AddDays(-28),
                PaymentGateway = PaymentGateway.Iyzico,
                GatewayPaymentId = "iyz- demo-123",
                Description = "İş emri kapanış ödemesi",
                ClientId = PrimaryDataTenantId,
                Status = Status.Active,
                CreatedDate = now,
                CreatedBy = SeedUserId
            };

            await context.Set<Payment>().AddAsync(pay1, ct);
            await context.SaveChangesAsync(ct);

            var payPersisted = await context.Set<Payment>().FirstAsync(p => p.InvoiceId == pi1.Id, ct);
            var comm = new Commission
            {
                PaymentId = payPersisted.Id,
                CommissionAmount = 88.50m,
                CommissionRate = 3m,
                Status = CommissionStatus.Paid,
                PaymentDate = now.AddDays(-28),
                Description = "Platform komisyonu (demo)",
                ClientId = PrimaryDataTenantId,
                CreatedDate = now,
                CreatedBy = SeedUserId
            };

            await context.Set<Commission>().AddAsync(comm, ct);
            await context.SaveChangesAsync(ct);
            logger.LogInformation("Extended seed: invoices, items, payments, commission.");
        }

        private static async Task SeedTaxAndFinanceAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            var now = DateTime.UtcNow;
            var added = 0;
            foreach (var cid in clientIds)
            {
                if (await context.Set<Tax>().AnyAsync(t => t.ClientId == cid, ct))
                    continue;

                var baseAmt = 8000m + cid * 750m;
                var taxes = new List<Tax>
                {
                    new()
                    {
                        TaxType = TaxType.VAT,
                        Month = 3,
                        Year = 2026,
                        Amount = baseAmt,
                        DueDate = now.AddDays(15),
                        Status = TaxStatus.Pending,
                        Description = $"Mart 2026 KDV beyanı (tenant {cid})",
                        TaxOffice = "Kadıköy",
                        TaxNumber = $"{1234567890 + cid}",
                        ClientId = cid,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    },
                    new()
                    {
                        TaxType = TaxType.MotorVehicleTax,
                        Month = null,
                        Year = 2026,
                        Amount = 2800m + cid * 50m,
                        DueDate = now.AddMonths(2),
                        Status = TaxStatus.Paid,
                        PaymentDate = now.AddDays(-5),
                        PaymentMethod = PaymentMethod.BankTransfer,
                        PaymentReferenceNumber = $"EFT-MTV-2026-{cid:D3}",
                        Description = $"2026 MTV 1. taksit — işletme {cid}",
                        ClientId = cid,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    }
                };

                await context.Set<Tax>().AddRangeAsync(taxes, ct);
                added += taxes.Count;
            }

            if (added > 0)
                await context.SaveChangesAsync(ct);
            logger.LogInformation("Extended seed: {Count} tax row(s) across tenants.", added);
        }

        private static async Task SeedAppointmentsAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            if (!clientIds.Contains(PrimaryDataTenantId)) return;
            if (await context.Set<Appointment>().AnyAsync(a => a.ClientId == PrimaryDataTenantId, ct)) return;

            var now = DateTime.UtcNow;
            var list = new List<Appointment>
            {
                new()
                {
                    AppointmentNumber = Appointment.GenerateAppointmentNumber(),
                    CustomerId = 1,
                    VehicleId = 1,
                    AppointmentDate = now.AddDays(3).Date,
                    StartTime = TimeSpan.FromHours(9),
                    EndTime = TimeSpan.FromHours(11),
                    AppointmentType = AppointmentType.Maintenance,
                    Description = "20.000 km periyodik bakım",
                    Status = AppointmentStatus.Scheduled,
                    AssignedEmployeeId = 3,
                    ClientId = PrimaryDataTenantId,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                },
                new()
                {
                    AppointmentNumber = Appointment.GenerateAppointmentNumber(),
                    CustomerId = 4,
                    VehicleId = 5,
                    AppointmentDate = now.AddDays(1).Date,
                    StartTime = TimeSpan.FromHours(14),
                    EndTime = TimeSpan.FromHours(15.5),
                    AppointmentType = AppointmentType.Repair,
                    Description = "Fren hidroliği kontrol",
                    Status = AppointmentStatus.Confirmed,
                    AssignedEmployeeId = 5,
                    ClientId = PrimaryDataTenantId,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                },
                new()
                {
                    AppointmentNumber = Appointment.GenerateAppointmentNumber(),
                    CustomerId = 2,
                    VehicleId = 3,
                    AppointmentDate = now.AddDays(-5).Date,
                    StartTime = TimeSpan.FromHours(10),
                    AppointmentType = AppointmentType.Inspection,
                    Description = "Araç muayenesi öncesi kontrol",
                    Status = AppointmentStatus.Completed,
                    AssignedEmployeeId = 4,
                    ClientId = PrimaryDataTenantId,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                }
            };

            await context.Set<Appointment>().AddRangeAsync(list, ct);
            await context.SaveChangesAsync(ct);
            logger.LogInformation("Extended seed: appointments.");
        }

        private static async Task SeedNotificationsAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            var now = DateTime.UtcNow;
            var templatesAdded = 0;
            foreach (var cid in clientIds)
            {
                if (await context.Set<NotificationTemplate>().AnyAsync(n => n.ClientId == cid && n.Name == "WorkOrderCompleted", ct))
                    continue;

                var templates = new List<NotificationTemplate>
                {
                    new()
                    {
                        Name = "WorkOrderCompleted",
                        Description = "İş emri tamamlandı",
                        Type = NotificationType.Push,
                        TitleTemplate = "İş emriniz tamamlandı — {WorkOrderNumber}",
                        ContentTemplate = "Sayın {CustomerName}, aracınız teslime hazır.",
                        RelatedEntityType = "WorkOrder",
                        IsActive = true,
                        ClientId = cid,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    },
                    new()
                    {
                        Name = "QuoteReceived",
                        Description = "Yeni teklif",
                        Type = NotificationType.Email,
                        TitleTemplate = "Yeni fiyat teklifi",
                        ContentTemplate = "Talebiniz {RequestNumber} için teklif alındı.",
                        RelatedEntityType = "QuoteRequest",
                        IsActive = true,
                        ClientId = cid,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    }
                };

                await context.Set<NotificationTemplate>().AddRangeAsync(templates, ct);
                templatesAdded += templates.Count;
            }

            if (templatesAdded > 0)
                await context.SaveChangesAsync(ct);

            var notifAdded = 0;
            foreach (var cid in clientIds)
            {
                var notificationsAlreadySeeded = cid == PrimaryDataTenantId
                    ? await context.Set<Notification>().AnyAsync(n => n.ClientId == cid && n.Title == "Düşük stok uyarısı", ct)
                    : await context.Set<Notification>().AnyAsync(n => n.ClientId == cid && n.Title == "Demo bildirim", ct);
                if (notificationsAlreadySeeded)
                    continue;

                var email = ManagerEmailForClient(cid);
                var mgr = email != null
                    ? await context.Users.FirstOrDefaultAsync(u => u.Email == email, ct)
                    : null;

                var notifications = new List<Notification>();
                if (cid == PrimaryDataTenantId)
                {
                    notifications.Add(new Notification
                    {
                        Type = NotificationType.Push,
                        UserId = mgr?.Id,
                        Title = "Düşük stok uyarısı",
                        Content = "Hava filtresi (FLT-AIR-001) minimum stok altında.",
                        Status = NotificationStatus.Read,
                        SentDate = now.AddDays(-1),
                        ReadDate = now.AddHours(-20),
                        RelatedEntityType = "PartStock",
                        RelatedEntityId = 3,
                        ClientId = cid,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    });
                    notifications.Add(new Notification
                    {
                        Type = NotificationType.Email,
                        RecipientEmail = "ahmet.yilmaz@example.com",
                        Title = "Randevu hatırlatması",
                        Content = "Yarın saat 09:00 bakım randevunuz var.",
                        Status = NotificationStatus.Sent,
                        SentDate = now.AddDays(-1),
                        RelatedEntityType = "Appointment",
                        ClientId = cid,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    });
                }
                else if (mgr != null)
                {
                    notifications.Add(new Notification
                    {
                        Type = NotificationType.Push,
                        UserId = mgr.Id,
                        Title = "Demo bildirim",
                        Content = $"İşletme {cid} — örnek panel bildirimi.",
                        Status = NotificationStatus.Read,
                        SentDate = now.AddDays(-1),
                        ReadDate = now.AddHours(-12),
                        ClientId = cid,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    });
                }

                if (notifications.Count > 0)
                {
                    await context.Set<Notification>().AddRangeAsync(notifications, ct);
                    notifAdded += notifications.Count;
                }
            }

            if (notifAdded > 0)
                await context.SaveChangesAsync(ct);
            logger.LogInformation(
                "Extended seed: {Tpl} template row(s), {Notif} notification row(s).",
                templatesAdded,
                notifAdded);
        }

        private static async Task SeedStockAlertsAndAutoOrdersAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            var now = DateTime.UtcNow;
            var seeded = 0;
            foreach (var cid in clientIds)
            {
                var ps3 = await context.Set<PartStock>().FirstOrDefaultAsync(p => p.ClientId == cid && p.PartId == 3, ct);
                if (ps3 == null)
                    continue;

                if (await context.Set<StockAlert>().AnyAsync(s => s.ClientId == cid && s.PartStockId == ps3.Id, ct))
                    continue;

                var supplier = await context.Set<PartSupplier>().FirstOrDefaultAsync(s => s.ClientId == cid, ct);
                if (supplier == null)
                    continue;

                var alert = new StockAlert
                {
                    PartId = 3,
                    PartStockId = ps3.Id,
                    AlertType = StockAlertType.LowStock,
                    Status = StockAlertStatus.Active,
                    CurrentStock = 3,
                    MinimumStock = 10,
                    RecommendedOrderQuantity = 20,
                    Message = $"Hava filtresi stok altında — tenant {cid}, sipariş önerilir.",
                    FirstAlertDate = now.AddDays(-4),
                    ClientId = cid,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                };

                await context.Set<StockAlert>().AddAsync(alert, ct);
                await context.SaveChangesAsync(ct);

                var ao = new AutoOrder
                {
                    OrderNumber = AutoOrder.GenerateOrderNumber(),
                    PartId = 3,
                    PartSupplierId = supplier.Id,
                    StockAlertId = alert.Id,
                    Quantity = 20,
                    UnitPrice = 45m,
                    Status = AutoOrderStatus.Approved,
                    ExpectedDeliveryDate = now.AddDays(5),
                    Notes = $"Otomatik tetiklenen taslak sipariş (tenant {cid})",
                    ClientId = cid,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                };
                ao.CalculateTotal();

                await context.Set<AutoOrder>().AddAsync(ao, ct);
                await context.SaveChangesAsync(ct);

                alert.AutoOrderCreated = true;
                alert.AutoOrderId = ao.Id;
                alert.Status = StockAlertStatus.OrderCreated;
                context.Set<StockAlert>().Update(alert);
                await context.SaveChangesAsync(ct);
                seeded++;
            }

            logger.LogInformation("Extended seed: stock alerts / auto orders for {Count} tenant(s).", seeded);
        }

        private static async Task SeedChatAndRatingsAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            var now = DateTime.UtcNow;
            var mgr = await context.Users.FirstOrDefaultAsync(u => u.Email == "manager@demo.com", ct);
            var cust = await context.Users.FirstOrDefaultAsync(u => u.Email == "musteri@demo.com", ct);

            if (!await context.Set<ChatMessage>().AnyAsync(c => c.ClientId == PrimaryDataTenantId, ct))
            {
                if (mgr != null && cust != null)
                {
                    var msgs = new List<ChatMessage>
                    {
                        new()
                        {
                            SenderId = cust.Id,
                            ReceiverId = mgr.Id,
                            Message = "Merhaba, aracımın iş emri ne zaman teslim?",
                            MessageType = ChatMessageType.Text,
                            WorkOrderId = 7,
                            CustomerId = 7,
                            SentDate = now.AddDays(-6),
                            IsRead = true,
                            ReadDate = now.AddDays(-6).AddHours(1),
                            ClientId = PrimaryDataTenantId,
                            Status = Status.Active,
                            CreatedDate = now,
                            CreatedBy = SeedUserId
                        },
                        new()
                        {
                            SenderId = mgr.Id,
                            ReceiverId = cust.Id,
                            Message = "Merhaba, tahmini yarın öğleden sonra teslime hazır olacak.",
                            MessageType = ChatMessageType.Text,
                            WorkOrderId = 7,
                            CustomerId = 7,
                            SentDate = now.AddDays(-6).AddHours(2),
                            IsRead = true,
                            ReadDate = now.AddDays(-6).AddHours(3),
                            ClientId = PrimaryDataTenantId,
                            Status = Status.Active,
                            CreatedDate = now,
                            CreatedBy = SeedUserId
                        }
                    };

                    await context.Set<ChatMessage>().AddRangeAsync(msgs, ct);
                    await context.SaveChangesAsync(ct);
                    logger.LogInformation("Extended seed: chat messages.");
                }
                else
                {
                    logger.LogWarning("Extended seed: chat skipped (manager@demo.com / musteri@demo.com not found).");
                }
            }

            if (await context.Set<ServiceRating>().AnyAsync(r => r.ClientId == PrimaryDataTenantId, ct)) return;

            var ratings = new List<ServiceRating>
            {
                new()
                {
                    WorkOrderId = 1,
                    CustomerId = 1,
                    Rating = 5,
                    ServiceQuality = 5,
                    PriceValue = 4,
                    OnTimeDelivery = 5,
                    StaffBehavior = 5,
                    Comment = "Çok ilgili ekip, işini bilen usta. Teşekkürler.",
                    Status = RatingStatus.Approved,
                    ServiceReply = "Değerli yorumunuz için teşekkür ederiz.",
                    ServiceReplyDate = now.AddDays(-27),
                    ClientId = PrimaryDataTenantId,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                },
                new()
                {
                    WorkOrderId = 2,
                    CustomerId = 2,
                    Rating = 4,
                    ServiceQuality = 4,
                    PriceValue = 4,
                    OnTimeDelivery = 4,
                    StaffBehavior = 5,
                    Comment = "Fiyat biraz yüksek ama kaliteli hizmet.",
                    Status = RatingStatus.Approved,
                    ClientId = PrimaryDataTenantId,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                }
            };

            await context.Set<ServiceRating>().AddRangeAsync(ratings, ct);
            await context.SaveChangesAsync(ct);
            logger.LogInformation("Extended seed: service ratings.");
        }

        private static async Task SeedPortfolioAuditUsageAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            var now = DateTime.UtcNow;
            var period = $"{now.Year}-{now.Month:D2}";

            if (clientIds.Contains(PrimaryDataTenantId) &&
                !await context.Set<ServicePortfolio>().AnyAsync(p => p.ClientId == PrimaryDataTenantId, ct))
            {
                var portfolio = new List<ServicePortfolio>
                {
                    new()
                    {
                        WorkOrderId = 1,
                        Title = "Toyota Corolla — fren revizyonu",
                        Description = "Ön fren balata ve disk bakımı (müşteri bilgisi gizli)",
                        Categories = """["Fren","Güvenlik"]""",
                        IsPublished = true,
                        PublishedDate = now.AddDays(-25),
                        CustomerApprovalStatus = CustomerApprovalStatus.Approved,
                        CustomerApprovalDate = now.AddDays(-26),
                        ViewCount = 128,
                        LikeCount = 14,
                        DisplayOrder = 1,
                        ClientId = PrimaryDataTenantId,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    },
                    new()
                    {
                        WorkOrderId = 2,
                        Title = "Honda Civic — periyodik bakım",
                        Description = "Yağ, filtre, genel kontrol",
                        Categories = """["Bakım"]""",
                        IsPublished = true,
                        PublishedDate = now.AddDays(-20),
                        CustomerApprovalStatus = CustomerApprovalStatus.Approved,
                        ViewCount = 64,
                        LikeCount = 6,
                        DisplayOrder = 2,
                        ClientId = PrimaryDataTenantId,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    }
                };

                await context.Set<ServicePortfolio>().AddRangeAsync(portfolio, ct);
            }

            foreach (var cid in clientIds)
            {
                if (await context.Set<AuditLog>().AnyAsync(
                        a => a.ClientId == cid && a.Description == "Demo denetim kaydı (extended seed)",
                        ct))
                    continue;

                var audit = new AuditLog
                {
                    UserId = SeedUserId,
                    EntityName = "WorkOrder",
                    EntityId = cid,
                    Action = "Update",
                    OldValues = """{"Status":"InRepair"}""",
                    NewValues = """{"Status":"Delivered"}""",
                    Description = "Demo denetim kaydı (extended seed)",
                    IpAddress = "192.168.1.10",
                    RequestPath = $"/api/workorders/demo-ext-{cid}",
                    RequestMethod = "PUT",
                    IsSuccess = true,
                    DurationMs = 30 + cid,
                    ClientId = cid,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                };

                await context.Set<AuditLog>().AddAsync(audit, ct);
            }

            foreach (var cid in clientIds)
            {
                if (await context.Set<UsageTracking>().AnyAsync(
                        u => u.ClientId == cid && u.CurrentPeriod == period && u.LimitType == LimitType.WorkOrder,
                        ct))
                    continue;

                var usage = new List<UsageTracking>
                {
                    new()
                    {
                        LimitType = LimitType.WorkOrder,
                        CurrentPeriod = period,
                        UsageCount = 5 + cid * 2,
                        LimitAmount = 400 + cid * 20,
                        ResetDate = now.AddMonths(1).Date,
                        ClientId = cid,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    },
                    new()
                    {
                        LimitType = LimitType.AIAnalysis,
                        CurrentPeriod = period,
                        UsageCount = 2 + cid,
                        LimitAmount = 40 + cid * 5,
                        ResetDate = now.AddMonths(1).Date,
                        ClientId = cid,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    }
                };

                await context.Set<UsageTracking>().AddRangeAsync(usage, ct);
            }

            await context.SaveChangesAsync(ct);
            logger.LogInformation("Extended seed: portfolio (tenant 1), audit + usage per tenant.");
        }

        private static async Task SeedSubscriptionAndSalaryAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            var now = DateTime.UtcNow;
            var subsAdded = 0;
            foreach (var cid in clientIds)
            {
                if (await context.Set<Subscription>().AnyAsync(s => s.ClientId == cid, ct))
                    continue;

                var monthly = 1499m + cid * 175m;
                var plan = (SubscriptionPlan)(cid % 3); // Free / Basic / Professional cycle
                var sub = new Subscription
                {
                    ClientId = cid,
                    Plan = plan,
                    Status = SubscriptionStatus.Active,
                    StartDate = now.AddMonths(-6),
                    EndDate = now.AddMonths(6),
                    MonthlyPrice = monthly,
                    YearlyPrice = monthly * 10,
                    MaxWorkOrders = plan == SubscriptionPlan.Free ? 50 : -1,
                    MaxUsers = plan == SubscriptionPlan.Free ? 2 : 10 + cid,
                    MaxAIAnalyses = plan == SubscriptionPlan.Free ? 10 : 100 + cid * 20,
                    Features = """["AdvancedReports","AI"]""",
                    AutoRenew = true,
                    LastPaymentDate = now.AddDays(-5),
                    NextPaymentDate = now.AddMonths(1),
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                };

                await context.Set<Subscription>().AddAsync(sub, ct);
                await context.SaveChangesAsync(ct);

                var spay = new SubscriptionPayment
                {
                    SubscriptionId = sub.Id,
                    PaymentNumber = SubscriptionPayment.GeneratePaymentNumber(),
                    Amount = monthly,
                    PaymentStatus = PaymentStatus.Paid,
                    PaymentMethod = PaymentMethod.CreditCard,
                    PaymentDate = now.AddDays(-5),
                    PaymentGateway = PaymentGateway.Iyzico,
                    GatewayPaymentId = $"sub-iyz-demo-{cid}",
                    PaymentPeriod = "Monthly",
                    Description = $"Aylık abonelik — işletme {cid}",
                    ClientId = cid,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                };

                await context.Set<SubscriptionPayment>().AddAsync(spay, ct);
                subsAdded++;
            }

            if (subsAdded > 0)
                await context.SaveChangesAsync(ct);

            var salaryRows = 0;
            foreach (var cid in clientIds)
            {
                var emps = await context.Set<Employee>().Where(e => e.ClientId == cid).OrderBy(e => e.Id).Take(5).ToListAsync(ct);
                var idx = 0;
                foreach (var emp in emps)
                {
                    if (await context.Set<SalaryPayment>().AnyAsync(
                            s => s.EmployeeId == emp.Id && s.Month == 3 && s.Year == 2026,
                            ct))
                    {
                        idx++;
                        continue;
                    }

                    var gross = 14000m + idx * 1100m + cid * 40m;
                    var s = new SalaryPayment
                    {
                        EmployeeId = emp.Id,
                        Month = 3,
                        Year = 2026,
                        GrossSalary = gross,
                        SocialSecurityDeduction = gross * 0.14m,
                        UnemploymentInsuranceDeduction = gross * 0.01m,
                        IncomeTaxDeduction = gross * 0.15m,
                        StampTax = 75.33m,
                        OtherDeductions = 0m,
                        NetSalary = 0m,
                        PaymentDate = now.AddDays(-3),
                        PaymentMethod = PaymentMethod.BankTransfer,
                        PaymentReferenceNumber = $"MAAS-2026-03-C{cid}-E{emp.Id}",
                        Description = $"Mart 2026 maaş — tenant {cid}",
                        ClientId = cid,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    };
                    s.CalculateNetSalary();
                    await context.Set<SalaryPayment>().AddAsync(s, ct);
                    salaryRows++;
                    idx++;
                }
            }

            if (salaryRows > 0)
                await context.SaveChangesAsync(ct);
            logger.LogInformation("Extended seed: {Subs} subscription(s), {Sal} salary row(s).", subsAdded, salaryRows);
        }

        private static async Task SeedHelpLoyaltyRemindersAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            var now = DateTime.UtcNow;

            foreach (var cid in clientIds)
            {
                if (await context.Set<HelpArticle>().AnyAsync(h => h.ClientId == cid, ct))
                    continue;

                var articles = new List<HelpArticle>
                {
                    new()
                    {
                        Title = $"İş emri oluşturma ({cid})",
                        Content = "## Adımlar\n1. Müşteri seç\n2. Araç seç\n3. Kaydet",
                        Category = "Work Orders",
                        Order = 1,
                        ViewCount = 20 + cid,
                        ClientId = cid,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    },
                    new()
                    {
                        Title = $"Stok uyarılarını yönetme ({cid})",
                        Content = "Stok **minimum** seviyenin altına düştüğünde bildirim alırsınız.",
                        Category = "Inventory",
                        Order = 2,
                        ViewCount = 10 + cid,
                        ClientId = cid,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    }
                };

                await context.Set<HelpArticle>().AddRangeAsync(articles, ct);
            }

            foreach (var cid in clientIds)
            {
                if (await context.Set<Reward>().AnyAsync(r => r.ClientId == cid, ct))
                    continue;

                var reward = new Reward
                {
                    Name = $"Sadakat indirimi %10 — işletme {cid}",
                    Description = "500 puan ile bir sonraki işçilikte %10 indirim",
                    RequiredPoints = 500,
                    Type = RewardType.Discount,
                    DiscountPercentage = 10m,
                    IsActive = true,
                    StockQuantity = 100,
                    ValidFrom = now.AddMonths(-1),
                    ValidTo = now.AddMonths(11),
                    ClientId = cid,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                };

                await context.Set<Reward>().AddAsync(reward, ct);
            }

            await context.SaveChangesAsync(ct);

            if (clientIds.Contains(PrimaryDataTenantId) &&
                !await context.Set<LoyaltyPoint>().AnyAsync(l => l.ClientId == PrimaryDataTenantId, ct))
            {
                var rewardT1 = await context.Set<Reward>().FirstAsync(r => r.ClientId == PrimaryDataTenantId, ct);
                var lp = new List<LoyaltyPoint>
                {
                    new()
                    {
                        CustomerId = 1,
                        Points = 120,
                        Type = LoyaltyPointType.Earned,
                        Description = "WO-1 tamamlandı — puan kazanımı",
                        WorkOrderId = 1,
                        ExpiryDate = now.AddYears(1),
                        ClientId = PrimaryDataTenantId,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    },
                    new()
                    {
                        CustomerId = 1,
                        Points = -50,
                        Type = LoyaltyPointType.Redeemed,
                        Description = "Kampanya indirimi için kullanıldı",
                        RewardId = rewardT1.Id,
                        ClientId = PrimaryDataTenantId,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    }
                };

                await context.Set<LoyaltyPoint>().AddRangeAsync(lp, ct);
                await context.SaveChangesAsync(ct);
            }

            foreach (var cid in clientIds)
            {
                if (await context.Set<Reminder>().AnyAsync(r => r.ClientId == cid && r.Title == "MTV ödemesi", ct))
                    continue;

                var email = ManagerEmailForClient(cid);
                var mgr = email != null
                    ? await context.Users.FirstOrDefaultAsync(u => u.Email == email, ct)
                    : null;
                if (mgr == null)
                    continue;

                var rem = new Reminder
                {
                    UserId = mgr.Id,
                    Title = "MTV ödemesi",
                    Content = $"Şirket araçları için MTV takibi — işletme {cid}",
                    ReminderDate = now.AddDays(7),
                    Type = ReminderType.Payment,
                    RelatedEntityType = "Tax",
                    IsSent = false,
                    ClientId = cid,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                };
                await context.Set<Reminder>().AddAsync(rem, ct);
            }

            await context.SaveChangesAsync(ct);
            logger.LogInformation("Extended seed: help, rewards, loyalty (tenant 1), reminders per tenant.");
        }

        private static async Task SeedPartPhotosAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            var now = DateTime.UtcNow;
            var total = 0;
            foreach (var cid in clientIds)
            {
                if (await context.Set<PartPhoto>().AnyAsync(p => p.ClientId == cid, ct))
                    continue;

                var partIds = await context.Set<Part>()
                    .Where(p => p.ClientId == cid)
                    .OrderBy(p => p.Id)
                    .Take(2)
                    .Select(p => p.Id)
                    .ToListAsync(ct);
                if (partIds.Count == 0)
                    continue;

                var photos = new List<PartPhoto>();
                for (var i = 0; i < partIds.Count; i++)
                {
                    photos.Add(new PartPhoto
                    {
                        PartId = partIds[i],
                        FilePath = i == 0
                            ? "https://images.unsplash.com/photo-1487754180451-c456f7a7c4b7?w=400"
                            : "https://images.unsplash.com/photo-1563720223185-11003d516935?w=400",
                        Description = i == 0 ? "Parça kutusu / etiket" : "Ürün görseli",
                        UploadDate = now,
                        DisplayOrder = 1,
                        ClientId = cid,
                        Status = Status.Active,
                        CreatedDate = now,
                        CreatedBy = SeedUserId
                    });
                }

                await context.Set<PartPhoto>().AddRangeAsync(photos, ct);
                total += photos.Count;
            }

            if (total > 0)
                await context.SaveChangesAsync(ct);
            logger.LogInformation("Extended seed: {Count} part photo row(s).", total);
        }

        private static async Task SeedUserDevicesAsync(BaseDbContext context, ILogger logger, IReadOnlyList<int> clientIds, CancellationToken ct)
        {
            var now = DateTime.UtcNow;
            var seeded = 0;
            foreach (var cid in clientIds)
            {
                if (await context.Set<UserDevice>().AnyAsync(d => d.ClientId == cid, ct))
                    continue;

                var email = ManagerEmailForClient(cid);
                if (email == null)
                    continue;

                var mgr = await context.Users.FirstOrDefaultAsync(u => u.Email == email, ct);
                if (mgr == null)
                    continue;

                var dev = new UserDevice
                {
                    UserId = mgr.Id,
                    DeviceId = $"expo-demo-device-{cid:D3}",
                    DeviceName = "Samsung Galaxy S23",
                    IsTrusted = true,
                    LastLoginAt = now,
                    ClientId = cid,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                };

                await context.Set<UserDevice>().AddAsync(dev, ct);

                var session = new UserSession
                {
                    UserId = mgr.Id,
                    TokenId = Guid.NewGuid().ToString("N"),
                    DeviceId = dev.DeviceId,
                    DeviceName = dev.DeviceName,
                    IpAddress = "10.0.2.2",
                    UserAgent = "Expo/49.0.0",
                    ExpiresAt = now.AddDays(7),
                    LastActivityAt = now,
                    ClientId = cid,
                    Status = Status.Active,
                    CreatedDate = now,
                    CreatedBy = SeedUserId
                };

                await context.Set<UserSession>().AddAsync(session, ct);
                seeded++;
            }

            if (seeded > 0)
                await context.SaveChangesAsync(ct);
            logger.LogInformation("Extended seed: user device/session for {Count} tenant(s).", seeded);
        }
    }
}
