using Ejad.Ui.Models;
using Ejad.Ui.Services;
using Microsoft.AspNetCore.Components;

namespace Ejad.Ui.Pages.Applicants.Details;

public partial class GetApplicantDetails
{
    [Parameter]
    public Guid Id { get; set; }

    [Inject]
    public IApplicantsServices ApplicantsServices { get; set; }

    [Inject]
    public IHttpService HttpService { get; set; }

    public ApplicantResult Applicant { get; set; } = new();
    private string _imageUrl;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        Applicant = await ApplicantsServices.GetDetailsAsync(Id);

        if (!string.IsNullOrEmpty(Applicant.PersonalImage))
        {
            var url = ApiRouts.Applicants.Image.Replace("{imageName}", Applicant.PersonalImage);
            var imageBytes = await HttpService.GetImageAsync(url);
            _imageUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(imageBytes)}";
        }
    }
}