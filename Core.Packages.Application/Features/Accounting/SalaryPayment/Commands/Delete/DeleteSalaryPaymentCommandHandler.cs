using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Commands.Delete
{
    public class DeleteSalaryPaymentCommandHandler : IRequestHandler<DeleteSalaryPaymentCommand, DeleteSalaryPaymentResponse>
    {
        private readonly ISalaryPaymentRepository _salaryPaymentRepository;
        private readonly ITenantService _tenantService;

        public DeleteSalaryPaymentCommandHandler(
            ISalaryPaymentRepository salaryPaymentRepository,
            ITenantService tenantService)
        {
            _salaryPaymentRepository = salaryPaymentRepository;
            _tenantService = tenantService;
        }

        public async Task<DeleteSalaryPaymentResponse> Handle(DeleteSalaryPaymentCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            // SalaryPayment'ı bul
            var salaryPayment = await _salaryPaymentRepository.GetByIdAsync(request.Id);
            if (salaryPayment == null)
            {
                throw new DomainException("SALARY_PAYMENT_NOT_FOUND", new { Id = request.Id });
            }

            if (salaryPayment.ClientId != clientId)
            {
                throw new DomainException("SALARY_PAYMENT_NOT_BELONG_TO_CLIENT", new { Id = request.Id });
            }

            // Soft delete
            _salaryPaymentRepository.Delete(salaryPayment);
            await _salaryPaymentRepository.SaveChangesAsync();

            return new DeleteSalaryPaymentResponse
            {
                Success = true,
                Message = "Maaş ödemesi başarıyla silindi."
            };
        }
    }
}
