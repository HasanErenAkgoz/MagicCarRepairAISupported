using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.RepairUtf8Mojibake
{
    /// <summary>
    /// Mevcut tenant için parça / stok hareketi metinlerinde UTF-8 mojibake onarımı (tek sefer veya tekrar güvenli).
    /// </summary>
    public sealed class RepairUtf8MojibakeCommand : IRequest<RepairUtf8MojibakeResponse>
    {
        /// <summary>
        /// Sadece SystemAdmin (UserType=1): hedef bayi <c>ClientId</c>. Diğer rollerde yok sayılır.
        /// </summary>
        public int? ForClientId { get; init; }
    }
}
