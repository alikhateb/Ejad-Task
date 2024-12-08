using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Ejad.Domain.Models;
using Ejad.Domain.Repositories;

namespace Ejad.Persistence.Repositories;

public class ApplicantRepository : RepositoryBase<Applicant>, IApplicantRepository
{
    public ApplicantRepository(AppDbContext appDbContext)
        : base(appDbContext)
    {
    }

    public ApplicantRepository(AppDbContext appDbContext, ISpecificationEvaluator specificationEvaluator)
        : base(appDbContext, specificationEvaluator)
    {
    }
}