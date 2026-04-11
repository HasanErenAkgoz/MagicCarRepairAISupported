using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Auth.Commands.Enable2FA
{
    public class Enable2FACommand : IRequest<IDataResult<Enable2FAResponse>>
    {
        public int UserId { get; set; }
    }

    public class Enable2FAResponse
    {
        public bool RequiresTwoFactor { get; set; } = true;
        public string? QrCodeUrl { get; set; } // Optional: QR code for authenticator app
        public List<string>? BackupCodes { get; set; } // Optional: Backup codes for recovery
        public string Secret { get; set; } = string.Empty; // For backward compatibility
        public string ManualEntryKey { get; set; } = string.Empty; // For backward compatibility
        public List<string> RecoveryCodes { get; set; } = new(); // For backward compatibility
        public string Message { get; set; } = string.Empty; // For backward compatibility
    }
}
