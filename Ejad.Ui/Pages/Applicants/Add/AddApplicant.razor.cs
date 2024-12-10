using Ejad.Ui.Services;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;

namespace Ejad.Ui.Pages.Applicants.Add;

public partial class AddApplicant
{
    [Inject]
    protected IApplicantsServices ApplicantsServices { get; set; }

    [Inject]
    protected IValidator<CreateApplicantCommand> Validator { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject]
    protected HttpClient HttpClient { get; set; }

    protected DateTime? DateOfBirth { get; set; }
    protected CreateApplicantCommand Model { get; set; }
    protected string MaritalStatus { get; set; }
    protected string[] ErrorMessages { get; set; }
    protected List<int> NestedComponents { get; set; }
    private Dictionary<int, AddWorkExperience> ChildRefs { get; set; }

    protected override Task OnInitializedAsync()
    {
        Model = new CreateApplicantCommand();
        DateOfBirth = DateTime.UtcNow;
        ErrorMessages = [];
        NestedComponents = [1];
        ChildRefs = new Dictionary<int, AddWorkExperience>();

        return base.OnInitializedAsync();
    }

    protected async Task OnSubmitAsync()
    {
        ErrorMessages = [];

        if (!Enum.TryParse<MaritalStatus>(MaritalStatus, out var maritalStatus))
        {
            ErrorMessages = ["invalid Marital Status"];
            return;
        }

        Model.MaritalStatus = maritalStatus;
        Model.DateOfBirth = DateOnly.FromDateTime(DateOfBirth.Value);

        var validateAsync = await Validator.ValidateAsync(Model);
        if (!validateAsync.IsValid)
        {
            ErrorMessages = validateAsync.Errors.Select(x => x.ErrorMessage).ToArray();
            return;
        }

        foreach (var childRef in ChildRefs.Values.ToList())
        {
            if (!await childRef.IsValid())
            {
                return;
            }

            Model.Experiences.Add(childRef.Model);
        }

        //var id = await ApplicantsServices.AddAsync(Model);
        var id = await SendRequest(Model);
        NavigationManager.NavigateTo($"/applicants/{id}");
    }

    protected async Task<Guid> SendRequest(CreateApplicantCommand payload)
    {
        try
        {
            using var multipartContent = new MultipartFormDataContent();

            var fullName = new StringContent(payload.FullName);
            var email = new StringContent(payload.Email);
            var mobile = new StringContent(payload.Mobile);
            var address = new StringContent(payload.Address);
            var jobTitle = new StringContent(payload.JobTitle);
            var linkedInUrl = new StringContent(payload.LinkedInUrl);
            var dateOfBirth = new StringContent(payload.DateOfBirth.ToString());
            var expectedSalary = new StringContent(payload.ExpectedSalary.ToString());
            var maritalStatus = new StringContent(payload.MaritalStatus.ToString());

            multipartContent.Add(fullName, nameof(payload.FullName));
            multipartContent.Add(email, nameof(payload.Email));
            multipartContent.Add(mobile, nameof(payload.Mobile));
            multipartContent.Add(address, nameof(payload.Address));
            multipartContent.Add(jobTitle, nameof(payload.JobTitle));
            multipartContent.Add(linkedInUrl, nameof(payload.LinkedInUrl));
            multipartContent.Add(dateOfBirth, nameof(payload.DateOfBirth));
            multipartContent.Add(expectedSalary, nameof(payload.ExpectedSalary));
            multipartContent.Add(maritalStatus, nameof(payload.MaritalStatus));

            var file = payload.PersonalImage;
            var ms = new MemoryStream();
            await file!.OpenReadStream(10 * 1024 * 1024).CopyToAsync(ms);
            var bytes = ms.ToArray();
            var imageContent = new ByteArrayContent(bytes);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(MediaTypeNames.Image.Jpeg);
            multipartContent.Add(imageContent, nameof(payload.PersonalImage), file.Name);

            var index = 0;
            foreach (var experience in payload.Experiences)
            {
                var companyName = new StringContent(experience.CompanyName);
                var title = new StringContent(experience.JobTitle);
                var end = new StringContent(experience.End.ToString());
                var start = new StringContent(experience.Start.ToString());
                var salary = new StringContent(experience.Salary.ToString());

                multipartContent.Add(companyName, $"{nameof(payload.Experiences)}[{index}].{nameof(experience.CompanyName)}");
                multipartContent.Add(title, $"{nameof(payload.Experiences)}[{index}].{nameof(experience.JobTitle)}");
                multipartContent.Add(end, $"{nameof(payload.Experiences)}[{index}].{nameof(experience.End)}");
                multipartContent.Add(start, $"{nameof(payload.Experiences)}[{index}].{nameof(experience.Start)}");
                multipartContent.Add(salary, $"{nameof(payload.Experiences)}[{index}].{nameof(experience.Salary)}");

                index++;
            }

            var response = await HttpClient.PostAsync(ApiRouts.Applicants.Add, multipartContent);
            var content = response.Content;

            Console.WriteLine("content ===> " + content);

            if (!response.IsSuccessStatusCode)
            {
                throw new AggregateException(await content.ReadAsStringAsync());
            }

            return await content.ReadFromJsonAsync<Guid>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected void OnFileCompleted(InputFileChangeEventArgs args)
    {
        Model.PersonalImage = args.File;
    }

    protected void AddNestedComponent()
    {
        NestedComponents.Add(NestedComponents.Count + 1);
    }

    protected void DeleteLastNestedComponent()
    {
        if (NestedComponents.Count != 0)
        {
            NestedComponents.RemoveAt(NestedComponents.Count - 1);
        }
    }
}