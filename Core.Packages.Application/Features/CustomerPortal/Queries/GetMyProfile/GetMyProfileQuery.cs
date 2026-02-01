using MediatR;
using MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.UpdateMyProfile;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyProfile
{
    public class GetMyProfileQuery : IRequest<UpdateMyProfileResponse>
    {
    }
}
