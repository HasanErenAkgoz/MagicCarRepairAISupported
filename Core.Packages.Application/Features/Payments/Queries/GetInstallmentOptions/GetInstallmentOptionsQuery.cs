using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Payments.Queries.GetInstallmentOptions
{
    /// <summary>
    /// Taksit seçenekleri sorgusu
    /// </summary>
    public class GetInstallmentOptionsQuery : IRequest<IDataResult<List<GetInstallmentOptionsResponse>>>
    {
        public decimal Amount { get; set; }
    }
}

