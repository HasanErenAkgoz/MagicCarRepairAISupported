using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Appointments.Commands.Create
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, CreateAppointmentResponse>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITenantService _tenantService;

        public CreateAppointmentCommandHandler(
            IAppointmentRepository appointmentRepository,
            ICustomerRepository customerRepository,
            ITenantService tenantService)
        {
            _appointmentRepository = appointmentRepository;
            _customerRepository = customerRepository;
            _tenantService = tenantService;
        }

        public async Task<CreateAppointmentResponse> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Customer kontrolü
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null || customer.ClientId != clientId)
            {
                throw new InvalidOperationException("Customer not found");
            }

            var appointment = new Appointment
            {
                AppointmentNumber = Appointment.GenerateAppointmentNumber(),
                CustomerId = request.CustomerId,
                VehicleId = request.VehicleId,
                AppointmentDate = request.AppointmentDate.Date,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                AppointmentType = request.AppointmentType,
                Description = request.Description,
                AssignedEmployeeId = request.AssignedEmployeeId,
                Status = Domain.Enums.AppointmentStatus.Scheduled,
                ClientId = clientId
            };

            await _appointmentRepository.AddAsync(appointment, cancellationToken);
            await _appointmentRepository.SaveChangesAsync();

            return new CreateAppointmentResponse
            {
                Id = appointment.Id,
                AppointmentNumber = appointment.AppointmentNumber
            };
        }
    }
}






