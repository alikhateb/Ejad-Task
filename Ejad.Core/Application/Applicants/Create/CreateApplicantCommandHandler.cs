using Ejad.Core.Services;
using Ejad.Domain.Ids;
using Ejad.Domain.Models;
using Ejad.Domain.Repositories;
using MediatR;

namespace Ejad.Core.Application.Applicants.Create;

internal class CreateApplicantCommandHandler(IApplicantRepository applicantRepository, IFileService fileService)
    : IRequestHandler<CreateApplicantCommand, ApplicantId>
{
    public async Task<ApplicantId> Handle(CreateApplicantCommand request, CancellationToken cancellationToken)
    {
        var applicant = new Applicant(fullName: request.FullName,
            email: request.Email,
            mobile: request.Mobile,
            dateOfBirth: request.DateOfBirth,
            maritalStatus: request.MaritalStatus,
            linkedInUrl: request.LinkedInUrl,
            jobTitle: request.JobTitle,
            expectedSalary: request.ExpectedSalary);

        var fileName = await fileService.SaveFileAsync(request.PersonalImage, applicant.Id, cancellationToken);

        applicant.PersonalImage = fileName;

        if (request.Experiences.Count != 0)
        {
            var experiences = request.Experiences.Select(x => new WorkExperience(jobTitle: x.JobTitle,
                    companyName: x.CompanyName,
                    start: x.Start,
                    end: x.End,
                    salary: x.Salary))
                .ToList();

            applicant.AddWorkExperience(experiences);
        }

        await applicantRepository.AddAsync(applicant, cancellationToken);

        return applicant.Id;
    }
}