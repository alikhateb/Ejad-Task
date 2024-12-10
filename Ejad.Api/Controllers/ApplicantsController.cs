using Ejad.Core;
using Ejad.Core.Application.Applicants.Create;
using Ejad.Core.Application.Applicants.Details;
using Ejad.Core.Application.Applicants.GetImage;
using Ejad.Core.Application.Applicants.List;
using Ejad.Domain.Ids;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ejad.Api.Controllers;

[ApiController]
public class ApplicantsController(ISender mediator) : ControllerBase
{
    private const string MimeType = "image/png";
    private const string ReturnedFileName = "image.png";

    [HttpPost(ApiRouts.Applicants.Add)]
    public async Task<ActionResult<ApplicantId>> Add([FromForm] CreateApplicantCommand command,
        CancellationToken cancellationToken = default)
    {
        return Ok(await mediator.Send(command, cancellationToken));
    }

    [HttpGet(ApiRouts.Applicants.Details)]
    public async Task<ActionResult<GetApplicantDetailsQueryResult>> Details([FromRoute] ApplicantId id,
        CancellationToken cancellationToken = default)
    {
        return Ok(await mediator.Send(new GetApplicantDetailsQuery(id), cancellationToken));
    }

    [HttpGet(ApiRouts.Applicants.List)]
    public async Task<ActionResult<List<GetApplicantsListQueryResult>>> List(
        CancellationToken cancellationToken = default)
    {
        return Ok(await mediator.Send(new GetApplicantsListQuery(), cancellationToken));
    }

    [HttpGet(ApiRouts.Applicants.Image)]
    public async Task<FileContentResult> ReturnByteArray([FromRoute] string imageName,
        CancellationToken cancellationToken = default)
    {
        var bytes = await mediator.Send(new GetApplicantImageQuery(imageName), cancellationToken);
        return File(bytes, MimeType, ReturnedFileName);
    }
}