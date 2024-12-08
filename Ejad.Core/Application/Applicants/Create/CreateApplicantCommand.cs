using Ejad.Domain.Ids;
using Ejad.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ejad.Core.Application.Applicants.Create;

public class CreateApplicantCommand : IRequest<ApplicantId>
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public IFormFile PersonalImage { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string LinkedInUrl { get; set; }
    public string JobTitle { get; set; }
    public decimal ExpectedSalary { get; set; }
    public List<WorkExperienceCommand> Experiences { get; set; } = [];
}