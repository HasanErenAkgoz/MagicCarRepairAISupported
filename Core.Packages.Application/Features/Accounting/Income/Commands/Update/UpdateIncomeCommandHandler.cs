using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Income.Commands.Update
{
    public class UpdateIncomeCommandHandler : IRequestHandler<UpdateIncomeCommand, UpdateIncomeResponse>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public UpdateIncomeCommandHandler(
            IIncomeRepository incomeRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<UpdateIncomeResponse> Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Geliri bul
            var income = await _incomeRepository.GetByIdAsync(request.Id);
            if (income == null)
            {
                throw new DomainException("INCOME_NOT_FOUND", new { Id = request.Id });
            }

            // ClientId kontrolü
            if (income.ClientId != clientId)
            {
                throw new DomainException("INCOME_NOT_BELONG_TO_CLIENT", new { IncomeId = request.Id });
            }

            // Güncelleme
            if (request.IncomeType.HasValue)
                income.IncomeType = request.IncomeType.Value;
            if (request.Amount.HasValue)
                income.Amount = request.Amount.Value;
            if (request.PaymentMethod.HasValue)
                income.PaymentMethod = request.PaymentMethod.Value;
            if (request.TransactionDate.HasValue)
                income.TransactionDate = request.TransactionDate.Value;
            if (request.Description != null)
                income.Description = request.Description;
            if (request.InvoiceNumber != null)
                income.InvoiceNumber = request.InvoiceNumber;

            _incomeRepository.Update(income);
            await _incomeRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<UpdateIncomeResponse>(income);
            response.IncomeTypeName = income.IncomeType.ToString();
            response.PaymentMethodName = income.PaymentMethod.ToString();
            return response;
        }
    }
}

