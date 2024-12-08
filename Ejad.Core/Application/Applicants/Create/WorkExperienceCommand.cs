namespace Ejad.Core.Application.Applicants.Create;

public class WorkExperienceCommand
{
    public string JobTitle { get; set; }
    public string CompanyName { get; set; }
    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }
    public decimal Salary { get; set; }
}