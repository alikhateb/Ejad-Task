using Ejad.Ui.Models;
using Ejad.Ui.Pages.Applicants.Add;

namespace Ejad.Ui.Services;

public interface IApplicantsServices
{
    Task<List<ApplicantResult>> GetListAsync(CancellationToken cancellationToken = default);

    Task<ApplicantResult> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Guid> AddAsync(CreateApplicantCommand command, CancellationToken cancellationToken = default);
}