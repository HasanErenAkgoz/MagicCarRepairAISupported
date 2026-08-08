using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Email;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IEmailService _emailService;
        private readonly ITenantService _tenantService;
        private readonly IConfiguration _configuration;
        private readonly IEmailTemplateRenderer _emailTemplateRenderer;

        public CreateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            UserManager<UserEntity> userManager,
            IEmailService emailService,
            ITenantService tenantService,
            IConfiguration configuration,
            IEmailTemplateRenderer emailTemplateRenderer)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _tenantService = tenantService;
            _configuration = configuration;
            _emailTemplateRenderer = emailTemplateRenderer;
        }

        public async Task<CreateEmployeeResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                EmployeeNo = Employee.GenerateEmployeeNo(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalId = request.NationalId,
                Phone = request.Phone ?? string.Empty,
                Email = request.Email,
                Position = request.Position,
                Salary = request.Salary,
                HireDate = request.HireDate,
                EmploymentStatus = request.EmploymentStatus,
                Address = request.Address,
                BloodType = request.BloodType,
                EmergencyContact = request.EmergencyContact,
                EmergencyPhone = request.EmergencyPhone,
                Notes = request.Notes,
                Biography = request.Biography,
                ProfilePhotoUrl = request.ProfilePhotoUrl,
                IsPublic = request.IsPublic,
                DisplayOrder = request.DisplayOrder,
            };

            if (request.Specializations != null && request.Specializations.Any())
            {
                employee.Specializations = JsonSerializer.Serialize(request.Specializations);
            }

            await _employeeRepository.AddAsync(employee, cancellationToken);
            await _employeeRepository.SaveChangesAsync();

            // Identity user oluştur (şifresiz — davet emailiyle belirleyecek)
            var clientId = _tenantService.GetRequiredClientId();
            var user = new UserEntity
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserType = UserType.Employee,
                ClientId = clientId,
                EmailConfirmed = false,
            };

            var createResult = await _userManager.CreateAsync(user, Guid.NewGuid().ToString("N") + "Aa1!");
            var response = _mapper.Map<CreateEmployeeResponse>(employee);
            if (!createResult.Succeeded)
            {
                var reasons = string.Join(" ", createResult.Errors.Select(e => e.Description));
                response.UserAccountCreated = false;
                response.InviteEmailSent = false;
                response.Warnings.Add(
                    "Giriş hesabı oluşturulamadı; şifre davet e-postası gönderilmedi. " + reasons);
                return response;
            }

            response.UserAccountCreated = true;
            employee.UserId = user.Id;
            _employeeRepository.Update(employee);
            await _employeeRepository.SaveChangesAsync();

            var rawToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(rawToken);
            var encodedEmail = Uri.EscapeDataString(request.Email);
            // Email clients often block custom schemes. Use an HTTP(S) URL that serves the HTML redirect page.
            // For invite testing on a physical phone, set AIOptions:ServerBaseUrl to this machine's LAN IP or a tunnel URL — not localhost.
            var serverBase = (_configuration["AIOptions:ServerBaseUrl"] ?? "http://localhost:5169").TrimEnd('/');
            var link = $"{serverBase}/api/Auth/set-password?email={encodedEmail}&token={encodedToken}";
            var appLink = $"magiccarrepair://set-password?email={encodedEmail}&token={encodedToken}";

            var body = _emailTemplateRenderer.RenderEmployeeInvite(request.FirstName, link, appLink);
            var emailSent = await _emailService.SendEmailAsync(
                request.Email, "Magic Car Repair — Şifrenizi belirleyin", body);
            response.InviteEmailSent = emailSent;
            if (!emailSent)
            {
                response.Warnings.Add(
                    "Kullanıcı hesabı oluşturuldu ancak e-posta gönderilemedi. Sunucudaki EmailSettings (SMTP) ayarlarını ve günlük kayıtlarını kontrol edin.");
            }

            return response;
        }
    }
}

