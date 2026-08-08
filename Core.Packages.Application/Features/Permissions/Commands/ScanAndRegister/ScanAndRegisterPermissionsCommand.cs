using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Permissions.Commands.ScanAndRegister
{
    public class ScanAndRegisterPermissionsCommand : IRequest<Unit>
    {
        /// <summary>
        /// Startup/background jobs: explicit tenant. Null uses current HTTP tenant (requires ClientId).
        /// </summary>
        public int? ClientId { get; set; }

        /// <summary>
        /// When true, only inserts global Permission rows (no role/user bootstrap).
        /// </summary>
        public bool PermissionsOnly { get; set; }

        /// <summary>
        /// When true, skips creating the default System Admin user (startup must not duplicate global email).
        /// </summary>
        public bool SkipBootstrapUser { get; set; }
    }
}
