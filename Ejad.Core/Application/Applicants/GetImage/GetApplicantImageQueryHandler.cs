using Ejad.Core.Services;
using MediatR;

namespace Ejad.Core.Application.Applicants.GetImage;

public class GetApplicantImageQueryHandler(IFileService fileService)
    : IRequestHandler<GetApplicantImageQuery, byte[]>
{
    public async Task<byte[]> Handle(GetApplicantImageQuery request, CancellationToken cancellationToken)
    {
        var bytes = await fileService.GetFileAsync(request.ImageName, cancellationToken);
        return bytes;
    }
}