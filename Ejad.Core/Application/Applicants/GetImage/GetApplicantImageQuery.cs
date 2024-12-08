using MediatR;

namespace Ejad.Core.Application.Applicants.GetImage;

public class GetApplicantImageQuery(string imageName) : IRequest<byte[]>
{
    public string ImageName { get; } = imageName;
}