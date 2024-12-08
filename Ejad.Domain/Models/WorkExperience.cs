namespace Ejad.Domain.Models;

public class WorkExperience
{
    public WorkExperience(string jobTitle, string companyName, DateOnly start, DateOnly end, decimal salary)
    {
        JobTitle = jobTitle;
        CompanyName = companyName;
        Start = start;
        End = end;
        Salary = salary;
    }

    private WorkExperience()
    {
    }

    public string JobTitle { get; set; }
    public string CompanyName { get; set; }
    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }
    public decimal Salary { get; set; }
}