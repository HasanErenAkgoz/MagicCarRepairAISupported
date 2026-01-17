using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Appointments.Queries.GetAll
{
    public class GetAllAppointmentsQueryHandler : IRequestHandler<GetAllAppointmentsQuery, List<GetAllAppointmentsResponse>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITenantService _tenantService;

        public GetAllAppointmentsQueryHandler(
            IAppointmentRepository appointmentRepository,
            ITenantService tenantService)
        {
            _appointmentRepository = appointmentRepository;
            _tenantService = tenantService;
        }

        public async Task<List<GetAllAppointmentsResponse>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            var appointments = await _appointmentRepository.GetListAsync(cancellationToken, 
                a => a.ClientId == clientId);

            // Filtreleme
            if (request.CustomerId.HasValue)
            {
                appointments = appointments.Where(a => a.CustomerId == request.CustomerId.Value).ToList();
            }

            if (request.EmployeeId.HasValue)
            {
                appointments = appointments.Where(a => a.AssignedEmployeeId == request.EmployeeId.Value).ToList();
            }

            if (request.StartDate.HasValue)
            {
                appointments = appointments.Where(a => a.AppointmentDate >= request.StartDate.Value).ToList();
            }

            if (request.EndDate.HasValue)
            {
                appointments = appointments.Where(a => a.AppointmentDate <= request.EndDate.Value).ToList();
            }

            if (request.Status.HasValue)
            {
                appointments = appointments.Where(a => a.Status == request.Status.Value).ToList();
            }

            return appointments.Select(a => new GetAllAppointmentsResponse
            {
                Id = a.Id,
                AppointmentNumber = a.AppointmentNumber,
                CustomerId = a.CustomerId,
                CustomerName = a.Customer?.FullName ?? "",
                VehicleId = a.VehicleId,
                VehicleLicensePlate = a.Vehicle?.LicensePlate,
                AppointmentDate = a.AppointmentDate,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                AppointmentType = a.AppointmentType.ToString(),
                Status = a.Status.ToString(),
                AssignedEmployeeId = a.AssignedEmployeeId,
                AssignedEmployeeName = a.AssignedEmployee?.FullName
            }).ToList();
        }
    }
}






