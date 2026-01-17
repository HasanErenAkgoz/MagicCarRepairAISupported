using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Queries.GetById
{
    public class GetTaxByIdQuery : IRequest<GetTaxByIdResponse>
    {
        public int Id { get; set; }
    }
}
