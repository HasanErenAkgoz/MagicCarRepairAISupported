using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Employees.Queries.GenerateQrCode
{
    public class GenerateEmployeeQrCodeQuery : IRequest<byte[]>
    {
        public int EmployeeId { get; set; }
    }
}
