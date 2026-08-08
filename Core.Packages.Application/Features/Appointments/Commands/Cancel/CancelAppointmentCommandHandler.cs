using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Appointments.Commands.Cancel
{
    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, CancelAppointmentResponse>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITenantService _tenantService;

        public CancelAppointmentCommandHandler(
            IAppointmentRepository appointmentRepository,
            ITenantService tenantService)
        {
            _appointmentRepository = appointmentRepository;
            _tenantService = tenantService;
        }

        public async Task<CancelAppointmentResponse> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);
            if (appointment == null || appointment.ClientId != clientId)
            {
                return new CancelAppointmentResponse
                {
                    Success = false,
                    Message = "Appointment not found"
                };
            }

            if (appointment.Status == AppointmentStatus.Cancelled)
            {
                return new CancelAppointmentResponse
                {
                    Success = false,
                    Message = "Appointment is already cancelled"
                };
            }

            appointment.Status = AppointmentStatus.Cancelled;
            appointment.CancellationReason = request.CancellationReason;

            _appointmentRepository.Update(appointment);
            await _appointmentRepository.SaveChangesAsync();

            return new CancelAppointmentResponse
            {
                Success = true,
                Message = "Appointment cancelled successfully"
            };
        }
    }
}






