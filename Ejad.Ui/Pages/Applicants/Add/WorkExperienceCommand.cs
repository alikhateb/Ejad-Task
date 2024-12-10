namespace Ejad.Ui.Pages.Applicants.Add;

public class WorkExperienceCommand
{
    public string JobTitle { get; set; }
    public string CompanyName { get; set; }
    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }
    public decimal Salary { get; set; }
}