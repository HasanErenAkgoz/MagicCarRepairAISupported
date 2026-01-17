using MagicCarRepairAISupported.Application.Common.Services.Import;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.Import;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.GetImportTemplate
{
    public class GetPartImportTemplateQueryHandler : IRequestHandler<GetPartImportTemplateQuery, byte[]>
    {
        private readonly IImportService _importService;

        public GetPartImportTemplateQueryHandler(IImportService importService)
        {
            _importService = importService;
        }

        public async Task<byte[]> Handle(GetPartImportTemplateQuery request, CancellationToken cancellationToken)
        {
            return await _importService.GenerateImportTemplateAsync<ImportPartsCommandHandler.PartImportDto>(cancellationToken);
        }
    }
}
