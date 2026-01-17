using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicFacilities
{
    public class GetPublicFacilitiesQueryHandler : IRequestHandler<GetPublicFacilitiesQuery, IDataResult<List<GetPublicFacilitiesResponse>>>
    {
        private readonly IFacilityPhotoRepository _facilityPhotoRepository;
        private readonly IFileStorageService _fileStorageService;

        public GetPublicFacilitiesQueryHandler(
            IFacilityPhotoRepository facilityPhotoRepository,
            IFileStorageService fileStorageService)
        {
            _facilityPhotoRepository = facilityPhotoRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<IDataResult<List<GetPublicFacilitiesResponse>>> Handle(GetPublicFacilitiesQuery request, CancellationToken cancellationToken)
        {
            var photos = string.IsNullOrEmpty(request.Category)
                ? await _facilityPhotoRepository.GetPublishedPhotosAsync(request.ClientId, cancellationToken)
                : await _facilityPhotoRepository.GetByCategoryAsync(request.ClientId, request.Category, cancellationToken);

            var responses = new List<GetPublicFacilitiesResponse>();

            foreach (var photo in photos)
            {
                var photoUrl = photo.PhotoPath; // File path zaten URL formatında olmalı

                var response = new GetPublicFacilitiesResponse
                {
                    Id = photo.Id,
                    PhotoPath = photoUrl,
                    Title = photo.Title,
                    Description = photo.Description,
                    Category = photo.Category,
                    UploadDate = photo.UploadDate
                };

                responses.Add(response);
            }

            return new SuccessDataResult<List<GetPublicFacilitiesResponse>>(responses);
        }
    }
}
