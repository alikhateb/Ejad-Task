using Microsoft.AspNetCore.Components.Forms;

namespace Ejad.Ui.Pages.Applicants.Add;

public class CreateApplicantCommand
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public IBrowserFile PersonalImage { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string LinkedInUrl { get; set; }
    public string Address { get; set; }
    public string JobTitle { get; set; }
    public decimal ExpectedSalary { get; set; }
    public List<WorkExperienceCommand> Experiences { get; set; } = [];
}