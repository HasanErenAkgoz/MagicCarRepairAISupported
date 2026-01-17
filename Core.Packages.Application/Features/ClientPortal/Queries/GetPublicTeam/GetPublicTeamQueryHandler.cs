using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicTeam
{
    public class GetPublicTeamQueryHandler : IRequestHandler<GetPublicTeamQuery, IDataResult<List<GetPublicTeamResponse>>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFileStorageService _fileStorageService;

        public GetPublicTeamQueryHandler(
            IEmployeeRepository employeeRepository,
            IFileStorageService fileStorageService)
        {
            _employeeRepository = employeeRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<IDataResult<List<GetPublicTeamResponse>>> Handle(GetPublicTeamQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetListAsync(
                cancellationToken, 
                e => e.ClientId == request.ClientId && 
                     e.IsPublic && 
                     e.EmploymentStatus == EmploymentStatus.Active);

            // Sort by DisplayOrder, then by HireDate
            var sortedEmployees = employees.OrderBy(e => e.DisplayOrder).ThenByDescending(e => e.HireDate).ToList();

            var responses = new List<GetPublicTeamResponse>();

            foreach (var employee in sortedEmployees)
            {
                var specializations = new List<string>();
                if (!string.IsNullOrEmpty(employee.SpecializationsJson))
                {
                    try
                    {
                        specializations = JsonSerializer.Deserialize<List<string>>(employee.SpecializationsJson) ?? new List<string>();
                    }
                    catch
                    {
                        // Ignore JSON parse errors
                    }
                }

                var photoUrl = employee.ProfilePhotoUrl ?? string.Empty;
                // File path zaten URL formatında olmalı, direkt kullanabiliriz

                var response = new GetPublicTeamResponse
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    Position = employee.Position.ToString(),
                    Biography = employee.Biography,
                    ProfilePhotoUrl = photoUrl,
                    Specializations = specializations,
                    HireDate = employee.HireDate
                };

                responses.Add(response);
            }

            return new SuccessDataResult<List<GetPublicTeamResponse>>(responses);
        }
    }
}
