using Ejad.Ui.Models;
using Ejad.Ui.Services;
using Microsoft.AspNetCore.Components;

namespace Ejad.Ui.Pages.Applicants.List;

public partial class GetApplicantsCollection
{
    private int _count;

    [Inject]
    public IApplicantsServices ApplicantsServices { get; set; }

    [Inject]
    public NavigationManager Manager { get; set; }

    public List<ApplicantResult> Applicants { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Applicants = await ApplicantsServices.GetListAsync();
    }

    protected void GetDetails(Guid id)
    {
        Manager.NavigateTo($"/applicants/{id}");
    }
}