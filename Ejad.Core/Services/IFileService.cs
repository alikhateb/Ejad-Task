using Ejad.Domain.Ids;
using Microsoft.AspNetCore.Http;

namespace Ejad.Core.Services;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile imageFile, ApplicantId id, CancellationToken cancellationToken = default);

    void DeleteFile(string fileNameWithExtension);

    Task<byte[]> GetFileAsync(string fileName, CancellationToken cancellationToken = default);
}