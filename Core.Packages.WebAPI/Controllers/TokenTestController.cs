#if DEBUG
using MagicCarRepairAISupported.Infrastructure.Configurations.Token;
using Core.Utilities.Security.Encyption;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "SystemAdmin")]
    public class TokenTestController : ControllerBase
    {
        private readonly TokenOptions _tokenOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenTestController(IOptions<TokenOptions> tokenOptions, IHttpContextAccessor httpContextAccessor)
        {
            _tokenOptions = tokenOptions.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("validate")]
        public IActionResult ValidateToken([FromBody] string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    return BadRequest(new { Error = "Token de�eri bo� olamaz." });
                }

                if (token.Count(c => c == '.') != 2)
                {
                    return BadRequest(new { Error = "Ge�ersiz JWT format�. Token �� par�adan olu�mal�d�r." });
                }

                if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    token = token.Substring(7).Trim();
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParams = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _tokenOptions.Issuer,
                    ValidAudience = _tokenOptions.Audience,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey),
                    ClockSkew = TimeSpan.Zero
                };

                var userClaims = _httpContextAccessor.HttpContext?.User.Claims;
                var principal = tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);

                return Ok(new
                {
                    IsValid = principal.Identity.IsAuthenticated,
                    Claims = principal.Claims.Select(c => new { c.Type, c.Value })
                });
            }
            catch (SecurityTokenException ex)
            {
                return BadRequest(new { Error = "JWT do�rulama hatas�: " + ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = "Beklenmeyen hata: " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpGet("test-token")]
        public IActionResult TestToken()
        {
            var headers = Request.Headers;
            return Ok(headers.ToDictionary(h => h.Key, h => h.Value.ToString()));
        }
    }
}
#endif
