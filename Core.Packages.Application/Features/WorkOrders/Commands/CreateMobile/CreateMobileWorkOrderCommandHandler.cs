using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileDetail;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.CreateMobile
{
    public class CreateMobileWorkOrderCommandHandler : IRequestHandler<CreateMobileWorkOrderCommand, CreateMobileWorkOrderResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEntityRepository<WorkOrderItem, int> _workOrderItemRepository;
        private readonly IEntityRepository<WorkOrderLabor, int> _workOrderLaborRepository;
        private readonly ITenantService _tenantService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMediator _mediator;

        public CreateMobileWorkOrderCommandHandler(
            IWorkOrderRepository workOrderRepository,
            ICustomerRepository customerRepository,
            IVehicleRepository vehicleRepository,
            IEmployeeRepository employeeRepository,
            IEntityRepository<WorkOrderItem, int> workOrderItemRepository,
            IEntityRepository<WorkOrderLabor, int> workOrderLaborRepository,
            ITenantService tenantService,
            IHttpContextAccessor httpContextAccessor,
            UserManager<UserEntity> userManager,
            IMediator mediator)
        {
            _workOrderRepository = workOrderRepository;
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
            _employeeRepository = employeeRepository;
            _workOrderItemRepository = workOrderItemRepository;
            _workOrderLaborRepository = workOrderLaborRepository;
            _tenantService = tenantService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<CreateMobileWorkOrderResponse> Handle(CreateMobileWorkOrderCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;
            var userId = GetCurrentUserId();

            // Validation
            if (string.IsNullOrWhiteSpace(request.CustomerName))
            {
                throw new DomainException("CUSTOMER_NAME_REQUIRED", new { Message = "Müşteri adı zorunludur." });
            }

            if (string.IsNullOrWhiteSpace(request.CustomerPhone))
            {
                throw new DomainException("CUSTOMER_PHONE_REQUIRED", new { Message = "Müşteri telefonu zorunludur." });
            }

            if (string.IsNullOrWhiteSpace(request.VehiclePlate))
            {
                throw new DomainException("VEHICLE_PLATE_REQUIRED", new { Message = "Araç plakası zorunludur." });
            }

            if (string.IsNullOrWhiteSpace(request.VehicleBrand))
            {
                throw new DomainException("VEHICLE_BRAND_REQUIRED", new { Message = "Araç markası zorunludur." });
            }

            if (string.IsNullOrWhiteSpace(request.VehicleModel))
            {
                throw new DomainException("VEHICLE_MODEL_REQUIRED", new { Message = "Araç modeli zorunludur." });
            }

            if (request.VehicleYear < 1900 || request.VehicleYear > 2030)
            {
                throw new DomainException("INVALID_VEHICLE_YEAR", new { Message = "Araç yılı 1900-2030 arasında olmalıdır." });
            }

            if (string.IsNullOrWhiteSpace(request.ServiceTitle))
            {
                throw new DomainException("SERVICE_TITLE_REQUIRED", new { Message = "Hizmet başlığı zorunludur." });
            }

            if (request.TaxRate < 0 || request.TaxRate > 100)
            {
                throw new DomainException("INVALID_TAX_RATE", new { Message = "KDV oranı 0-100 arasında olmalıdır." });
            }

            // Status string'den enum'a çevir
            var status = MapStringToStatus(request.Status);
            if (status == null)
            {
                throw new DomainException("INVALID_STATUS", new { Status = request.Status });
            }

            // Müşteri eşleştirme veya oluşturma
            Customer customer;
            if (request.CustomerId.HasValue)
            {
                // Mobil listeden seçildi — direkt ID ile çek
                customer = await _customerRepository.Query()
                    .FirstOrDefaultAsync(c => c.Id == request.CustomerId.Value && c.ClientId == clientId, cancellationToken)
                    ?? throw new DomainException("CUSTOMER_NOT_FOUND", new { CustomerId = request.CustomerId.Value });
            }
            else
            {
                // Manuel giriş senaryosu — telefona göre bul veya oluştur
                var customerPhone = NormalizePhoneNumber(request.CustomerPhone);
                customer = await _customerRepository.Query()
                    .FirstOrDefaultAsync(c => c.PhoneNumber == customerPhone && c.ClientId == clientId, cancellationToken);

                if (customer == null)
                {
                    var nameParts = request.CustomerName.Trim().Split(' ', 2);
                    customer = new Customer
                    {
                        FirstName = nameParts[0],
                        LastName = nameParts.Length > 1 ? nameParts[1] : string.Empty,
                        PhoneNumber = customerPhone,
                        Email = request.CustomerEmail ?? string.Empty,
                        Address = string.Empty,
                        ClientId = clientId,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = userId,
                        Status = Domain.Enums.Status.Active
                    };
                    await _customerRepository.AddAsync(customer, cancellationToken);
                    await _customerRepository.SaveChangesAsync();
                }
                else if (!string.IsNullOrEmpty(request.CustomerEmail) && string.IsNullOrEmpty(customer.Email))
                {
                    customer.Email = request.CustomerEmail;
                    customer.ModifiedDate = DateTime.UtcNow;
                    customer.ModifiedBy = userId;
                    _customerRepository.Update(customer);
                    await _customerRepository.SaveChangesAsync();
                }
            }

            // Araç eşleştirme veya oluşturma
            var normalizedPlate = NormalizeLicensePlate(request.VehiclePlate);

            Vehicle vehicle;
            if (request.VehicleId.HasValue)
            {
                // Mobil listeden seçildi — direkt ID ile çek
                vehicle = await _vehicleRepository.Query()
                    .FirstOrDefaultAsync(v => v.Id == request.VehicleId.Value && v.ClientId == clientId, cancellationToken)
                    ?? throw new DomainException("VEHICLE_NOT_FOUND", new { VehicleId = request.VehicleId.Value });
            }
            else
            {
                vehicle = await _vehicleRepository.GetByLicensePlateAsync(normalizedPlate, cancellationToken);

                if (vehicle == null || vehicle.ClientId != clientId)
                {
                    // Yeni araç oluştur
                    vehicle = new Vehicle
                    {
                        CustomerId = customer.Id,
                        LicensePlate = normalizedPlate,
                        Brand = request.VehicleBrand,
                        Model = request.VehicleModel,
                        Year = request.VehicleYear,
                        Vin = request.VehicleVin,
                        FuelType = request.VehicleFuelType,
                        ClientId = clientId,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = userId,
                        Status = VehicleStatus.Registered
                    };
                    await _vehicleRepository.AddAsync(vehicle, cancellationToken);
                    await _vehicleRepository.SaveChangesAsync();
                }
                else
                {
                    // Mevcut araç müşteriye ait değilse kontrol et
                    if (vehicle.CustomerId != customer.Id)
                    {
                        // Araç başka bir müşteriye ait, yeni araç oluştur
                        vehicle = new Vehicle
                        {
                            CustomerId = customer.Id,
                            LicensePlate = normalizedPlate + "-" + Guid.NewGuid().ToString("N").Substring(0, 4),
                            Brand = request.VehicleBrand,
                            Model = request.VehicleModel,
                            Year = request.VehicleYear,
                            Vin = request.VehicleVin,
                            FuelType = request.VehicleFuelType,
                            ClientId = clientId,
                            CreatedDate = DateTime.UtcNow,
                            CreatedBy = userId,
                            Status = VehicleStatus.Registered
                        };
                        await _vehicleRepository.AddAsync(vehicle, cancellationToken);
                        await _vehicleRepository.SaveChangesAsync();
                    }
                    else
                    {
                        // Mevcut araç - FuelType boşsa güncelle
                        if (string.IsNullOrWhiteSpace(vehicle.FuelType) && !string.IsNullOrWhiteSpace(request.VehicleFuelType))
                        {
                            vehicle.FuelType = request.VehicleFuelType;
                            vehicle.ModifiedDate = DateTime.UtcNow;
                            vehicle.ModifiedBy = userId;
                            _vehicleRepository.Update(vehicle);
                            await _vehicleRepository.SaveChangesAsync();
                        }
                    }
                }
            } // end VehicleId else

            // Technician kontrolü
            if (request.TechnicianId.HasValue)
            {
                var employee = await _employeeRepository.GetByIdAsync(request.TechnicianId.Value);
                if (employee == null || employee.ClientId != clientId)
                {
                    throw new DomainException("EMPLOYEE_NOT_FOUND", new { EmployeeId = request.TechnicianId.Value });
                }
            }

            // İş emri numarası oluştur
            var orderNo = GenerateWorkOrderNumber();

            // WorkOrder oluştur
            var workOrder = new WorkOrder
            {
                WorkOrderNumber = orderNo,
                VehicleId = vehicle.Id,
                CustomerId = customer.Id,
                EntryDate = DateTime.UtcNow,
                AssignedEmployeeId = request.TechnicianId,
                Notes = request.TechnicianNotes,
                SpecialRequests = request.ServiceTitle, // ServiceTitle'ı buraya kaydet
                CustomerComplaints = request.ServiceDescription, // ServiceDescription'ı buraya kaydet
                Status = status.Value,
                ClientId = clientId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = userId
            };

            await _workOrderRepository.AddAsync(workOrder, cancellationToken);
            await _workOrderRepository.SaveChangesAsync();

            // Parts ekle
            foreach (var partDto in request.Parts)
            {
                var item = new WorkOrderItem
                {
                    WorkOrderId = workOrder.Id,
                    ItemType = WorkOrderItemType.Part,
                    Description = partDto.Name,
                    Quantity = partDto.Quantity,
                    UnitPrice = partDto.UnitPrice,
                    TaxRate = request.TaxRate,
                    ClientId = clientId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = userId,
                    Status = Domain.Enums.Status.Active
                };
                item.CalculateTotal();
                await _workOrderItemRepository.AddAsync(item, cancellationToken);
            }

            // Labor ekle
            foreach (var laborDto in request.Labor)
            {
                var employeeId = request.TechnicianId ?? workOrder.AssignedEmployeeId;
                if (!employeeId.HasValue)
                {
                    // Current user'dan employee bul
                    var user = await _userManager.FindByIdAsync(userId.ToString());
                    if (user != null)
                    {
                        var employee = await _employeeRepository.Query()
                            .FirstOrDefaultAsync(e => e.UserId == userId && e.ClientId == clientId, cancellationToken);
                        if (employee != null)
                        {
                            employeeId = employee.Id;
                        }
                    }
                }

                if (!employeeId.HasValue)
                {
                    throw new DomainException("EMPLOYEE_REQUIRED_FOR_LABOR", new { Message = "İşçilik eklemek için teknisyen atanmalıdır." });
                }

                var labor = new WorkOrderLabor
                {
                    WorkOrderId = workOrder.Id,
                    EmployeeId = employeeId.Value,
                    OperationName = laborDto.Description,
                    DurationHours = laborDto.Hours,
                    HourlyRate = laborDto.HourlyRate,
                    ClientId = clientId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = userId,
                    Status = Domain.Enums.Status.Active
                };
                labor.CalculateTotal();
                await _workOrderLaborRepository.AddAsync(labor, cancellationToken);
            }

            // WorkOrder toplamını hesapla
            workOrder.CalculateTotal();
            _workOrderRepository.Update(workOrder);
            await _workOrderRepository.SaveChangesAsync();

            // Timeline kaydı oluştur
            var createdByName = await GetCreatedByNameAsync(userId);
            var timeline = new WorkOrderTimeline
            {
                WorkOrderId = workOrder.Id,
                EventDate = DateTime.UtcNow,
                EventType = "created",
                Description = "İş emri oluşturuldu.",
                EmployeeId = request.TechnicianId,
                ClientId = clientId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = userId,
                Status = Domain.Enums.Status.Active
            };
            await _workOrderRepository.AddTimelineAsync(timeline, cancellationToken);

            // Response için detay query'sini çağır
            var detailQuery = new GetMobileWorkOrderDetailQuery { Id = workOrder.Id.ToString() };
            var detailResponse = await _mediator.Send(detailQuery, cancellationToken);

            return new CreateMobileWorkOrderResponse
            {
                Data = detailResponse
            };
        }

        private string NormalizePhoneNumber(string phone)
        {
            // Türkiye telefon formatını normalize et
            phone = phone.Trim().Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
            if (phone.StartsWith("0"))
            {
                return phone;
            }
            if (phone.StartsWith("+90"))
            {
                return "0" + phone.Substring(3);
            }
            if (phone.StartsWith("90"))
            {
                return "0" + phone.Substring(2);
            }
            return phone;
        }

        private string NormalizeLicensePlate(string plate)
        {
            // Plakayı büyük harfe çevir ve boşlukları normalize et
            return plate.Trim().ToUpperInvariant().Replace(" ", " ");
        }

        private string GenerateWorkOrderNumber()
        {
            // Format: WO-{YIL}-{SIRADAKI_NUMARA_4_BASAMAK}
            var year = DateTime.UtcNow.Year;
            
            // Aynı yıl içindeki son iş emri numarasını bul
            var lastOrder = _workOrderRepository.Query()
                .Where(wo => wo.WorkOrderNumber.StartsWith($"WO-{year}-"))
                .OrderByDescending(wo => wo.WorkOrderNumber)
                .FirstOrDefault();

            int nextNumber = 1;
            if (lastOrder != null)
            {
                var parts = lastOrder.WorkOrderNumber.Split('-');
                if (parts.Length == 3 && int.TryParse(parts[2], out var lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"WO-{year}-{nextNumber:D4}";
        }

        private WorkOrderStatus? MapStringToStatus(string status)
        {
            return status.ToLower() switch
            {
                "pending" => WorkOrderStatus.VehicleEntered, // Varsayılan pending durumu
                "inprogress" => WorkOrderStatus.InProgress,
                "completed" => WorkOrderStatus.Delivered,
                "cancelled" => WorkOrderStatus.Cancelled,
                _ => WorkOrderStatus.VehicleEntered
            };
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }
            return 0; // System user
        }

        private async Task<string> GetCreatedByNameAsync(int userId)
        {
            if (userId == 0)
            {
                return "Sistem";
            }

            // Önce Employee'den dene
            var employee = await _employeeRepository.Query()
                .FirstOrDefaultAsync(e => e.UserId == userId);
            if (employee != null)
            {
                return employee.FullName;
            }

            // Sonra User'dan dene
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                var fullName = $"{user.FirstName} {user.LastName}".Trim();
                return !string.IsNullOrEmpty(fullName) ? fullName : (user.UserName ?? "Sistem");
            }

            return "Sistem";
        }
    }
}
