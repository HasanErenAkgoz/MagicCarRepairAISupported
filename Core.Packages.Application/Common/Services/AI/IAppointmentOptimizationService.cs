using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Application.Common.Services.AI
{
    /// <summary>
    /// AI destekli randevu optimizasyon servisi
    /// </summary>
    public interface IAppointmentOptimizationService
    {
        /// <summary>
        /// En uygun randevu saatlerini önerir
        /// </summary>
        Task<AppointmentOptimizationResponseDto> OptimizeAppointmentsAsync(
            AppointmentOptimizationRequestDto request, 
            CancellationToken cancellationToken = default);
    }
}
