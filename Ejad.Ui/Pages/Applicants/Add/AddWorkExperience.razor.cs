using FluentValidation;
using Microsoft.AspNetCore.Components;

namespace Ejad.Ui.Pages.Applicants.Add;

public partial class AddWorkExperience
{
    [Inject]
    public IValidator<WorkExperienceCommand> Validator { get; set; }
    [Parameter]
    public int InstanceId { get; set; }

    public WorkExperienceCommand Model { get; set; }
    protected DateTime? Start { get; set; }
    protected DateTime? End { get; set; }
    protected string[] ErrorMessages { get; set; }

    protected override Task OnInitializedAsync()
    {
        Model = new WorkExperienceCommand();
        Start = DateTime.UtcNow;
        End = DateTime.UtcNow;
        ErrorMessages = [];

        return base.OnInitializedAsync();
    }

    public async Task<bool> IsValid()
    {
        ErrorMessages = [];

        Model.Start = DateOnly.FromDateTime(Start.Value);
        Model.End = DateOnly.FromDateTime(End.Value);

        var validateAsync = await Validator.ValidateAsync(Model);
        if (!validateAsync.IsValid)
        {
            ErrorMessages = validateAsync.Errors.Select(x => x.ErrorMessage).ToArray();
            return false;
        }

        return true;
    }
}