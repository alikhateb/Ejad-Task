using Ejad.Domain.Ids;
using Ejad.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ejad.Persistence.EntityConfigurations;

public class PersonalInformationConfiguration : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.ToTable("Applicants");

        builder.HasKey(x => x.Id);

        builder.OwnsMany(entity => entity.WorkExperiences, builderAction =>
        {
            builderAction.WithOwner()
                .HasForeignKey("ApplicantId");

            builderAction.Property<int>("Id");
            builderAction.Property<ApplicantId>("ApplicantId");
        });
    }
}