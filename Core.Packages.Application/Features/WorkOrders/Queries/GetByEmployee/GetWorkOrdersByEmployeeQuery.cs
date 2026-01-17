using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetByEmployee
{
    /// <summary>
    /// Belirli bir personele atanmış iş emirlerini getirir
    /// </summary>
    public class GetWorkOrdersByEmployeeQuery : IRequest<GetWorkOrdersByEmployeeResponse>
    {
        /// <summary>
        /// Personel ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Durum filtresi (opsiyonel)
        /// </summary>
        public Domain.Enums.WorkOrderStatus? Status { get; set; }

        /// <summary>
        /// Başlangıç tarihi (opsiyonel)
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Bitiş tarihi (opsiyonel)
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Sadece aktif iş emirlerini getir
        /// </summary>
        public bool OnlyActive { get; set; } = false;
    }
}
