using Strongly;

namespace Ejad.Domain.Ids;

[Strongly(backingType: StronglyType.Guid,
    converters: StronglyConverter.SystemTextJson |
                StronglyConverter.EfValueConverter |
                StronglyConverter.TypeConverter)]
public partial struct ApplicantId;