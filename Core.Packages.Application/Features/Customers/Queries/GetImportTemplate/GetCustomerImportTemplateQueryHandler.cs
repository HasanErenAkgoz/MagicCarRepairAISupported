using MagicCarRepairAISupported.Application.Common.Services.Import;
using MagicCarRepairAISupported.Application.Features.Customers.Commands.Import;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Customers.Queries.GetImportTemplate
{
    public class GetCustomerImportTemplateQueryHandler : IRequestHandler<GetCustomerImportTemplateQuery, byte[]>
    {
        private readonly IImportService _importService;

        public GetCustomerImportTemplateQueryHandler(IImportService importService)
        {
            _importService = importService;
        }

        public async Task<byte[]> Handle(GetCustomerImportTemplateQuery request, CancellationToken cancellationToken)
        {
            return await _importService.GenerateImportTemplateAsync<ImportCustomersCommandHandler.CustomerImportDto>(cancellationToken);
        }
    }
}
