using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Customers.Commands.Create;
using MagicCarRepairAISupported.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Sadece yetkili kullanıcılar müşteri hesabı oluşturabilir
    public class CustomerAccountController : BaseApiController
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMediator _mediator;
        private readonly ITenantService _tenantService;

        public CustomerAccountController(
            UserManager<UserEntity> userManager,
            RoleManager<Role> roleManager,
            IMediator mediator,
            ITenantService tenantService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mediator = mediator;
            _tenantService = tenantService;
        }

        /// <summary>
        /// Müşteri hesabı oluşturur (User + Customer + Customer Role)
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomerAccount([FromBody] CreateCustomerAccountRequest request)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                // 1. Email kontrolü
                var existingUser = await _userManager.FindByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    return BadRequest(new { message = "Bu email adresi zaten kullanılıyor." });
                }

                // 2. User oluştur
                var user = new UserEntity
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    IdentityNo = request.IdentityNo,
                    Address = request.Address,
                    Email = request.Email,
                    UserName = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    UserType = Domain.Enums.UserType.Customer,
                    ClientId = clientId
                };

                var createUserResult = await _userManager.CreateAsync(user, request.Password);
                if (!createUserResult.Succeeded)
                {
                    return BadRequest(new { 
                        message = "Kullanıcı oluşturulamadı.", 
                        errors = createUserResult.Errors 
                    });
                }

                // 3. Customer rolünü kontrol et ve yoksa oluştur
                var customerRoleName = "Customer";
                var customerRole = await _roleManager.FindByNameAsync(customerRoleName);
                
                if (customerRole == null)
                {
                    // Role yoksa oluştur
                    customerRole = new Role
                    {
                        Name = customerRoleName,
                        NormalizedName = customerRoleName.ToUpper(),
                        ClientId = clientId
                    };
                    var createRoleResult = await _roleManager.CreateAsync(customerRole);
                    if (!createRoleResult.Succeeded)
                    {
                        // User'ı sil
                        await _userManager.DeleteAsync(user);
                        return BadRequest(new { 
                            message = "Customer rolü oluşturulamadı.", 
                            errors = createRoleResult.Errors 
                        });
                    }
                }

                // 4. User'a Customer rolünü ata
                var addToRoleResult = await _userManager.AddToRoleAsync(user, customerRoleName);
                if (!addToRoleResult.Succeeded)
                {
                    // User'ı sil
                    await _userManager.DeleteAsync(user);
                    return BadRequest(new { 
                        message = "Kullanıcıya rol atanamadı.", 
                        errors = addToRoleResult.Errors 
                    });
                }

                // 5. Customer entity oluştur
                var createCustomerCommand = new CreateCustomerCommand
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    IdentityNo = request.IdentityNo,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    DateTimeOfBirth = request.DateTimeOfBirth,
                    Language = request.Language ?? "tr",
                    IsVip = request.IsVip ?? false,
                    UserId = user.Id
                };

                CreateCustomerResponse customerResult;
                try
                {
                    customerResult = await _mediator.Send(createCustomerCommand);
                }
                catch (Exception ex)
                {
                    // Customer oluşturulamazsa User'ı sil
                    await _userManager.DeleteAsync(user);
                    return BadRequest(new { message = $"Müşteri oluşturulamadı: {ex.Message}" });
                }

                if (customerResult == null)
                {
                    // Customer oluşturulamazsa User'ı sil
                    await _userManager.DeleteAsync(user);
                    return BadRequest(new { message = "Müşteri oluşturulamadı." });
                }

                return Ok(new
                {
                    success = true,
                    message = "Müşteri hesabı başarıyla oluşturuldu.",
                    userId = user.Id,
                    customerId = customerResult.Id,
                    email = user.Email
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
            }
        }
    }

    public class CreateCustomerAccountRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdentityNo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime DateTimeOfBirth { get; set; } = new DateTime(1990, 1, 1);
        public string? Language { get; set; } = "tr";
        public bool? IsVip { get; set; } = false;
    }
}
