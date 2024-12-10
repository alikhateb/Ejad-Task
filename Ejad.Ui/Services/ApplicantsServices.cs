using Ejad.Ui.Models;
using Ejad.Ui.Pages.Applicants.Add;

namespace Ejad.Ui.Services;

public class ApplicantsServices(IHttpService httpService) : IApplicantsServices
{
    public async Task<List<ApplicantResult>> GetListAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await httpService.GetAsync<List<ApplicantResult>>(ApiRouts.Applicants.List, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ApplicantResult> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var url = $"{ApiRouts.Applicants.Details.Replace("{id}", id.ToString())}";
            var result = await httpService.GetAsync<ApplicantResult>(url, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Guid> AddAsync(CreateApplicantCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            return await httpService.PostFormDataAsync<CreateApplicantCommand, Guid>(ApiRouts.Applicants.Add, command, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}