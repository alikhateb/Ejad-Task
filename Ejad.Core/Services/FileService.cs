using Ejad.Domain.Ids;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Ejad.Core.Services;

public class FileService(IWebHostEnvironment environment) : IFileService
{
    private readonly string[] _allowedFileExtensions = [".png", ".jpg", ".jpeg"];
        //[".png", ".jpg", ".jpeg", ".webp", ".jfif", ".pjpeg", ".pjp"];

    public async Task<string> SaveFileAsync(IFormFile imageFile, ApplicantId id,
        CancellationToken cancellationToken = default)
    {
        if (imageFile == null)
        {
            throw new ArgumentNullException(paramName: nameof(imageFile));
        }

        var webRootPath = environment.WebRootPath;

        if (!Directory.Exists(webRootPath))
        {
            Directory.CreateDirectory(webRootPath);
        }

        // Check the allowed extenstions
        var ext = Path.GetExtension(imageFile.FileName);
        if (!_allowedFileExtensions.Contains(ext))
        {
            throw new ArgumentException($"Only {string.Join(",", _allowedFileExtensions)} are allowed.");
        }

        // generate a unique filename
        var fileName = $"{id.ToString()}{ext}";
        var fileNameWithPath = Path.Combine(webRootPath, fileName);

        await using var stream = new FileStream(fileNameWithPath, FileMode.Create);
        await imageFile.CopyToAsync(stream, cancellationToken);
        return fileName;
    }

    public void DeleteFile(string fileNameWithExtension)
    {
        if (string.IsNullOrEmpty(fileNameWithExtension))
        {
            throw new ArgumentNullException(nameof(fileNameWithExtension));
        }

        var contentPath = environment.WebRootPath;
        var path = Path.Combine(contentPath, fileNameWithExtension);

        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Invalid file path");
        }

        File.Delete(path);
    }

    public async Task<byte[]> GetFileAsync(string fileName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentNullException(paramName: nameof(fileName));
        }

        // Path to the wwwroot folder
        var wwwRootPath = environment.WebRootPath;

        // Path to the file you want to access
        var filePath = Path.Combine(wwwRootPath, fileName);

        if (!File.Exists(filePath))
        {
            // Read the file content (example: returning it as a string)
            throw new FileNotFoundException("Invalid file path");
        }

        var bytes = await File.ReadAllBytesAsync(filePath, cancellationToken);

        return bytes;
    }

    //public IActionResult DownloadFile()
    //{
    //    // Path to the file
    //    string wwwRootPath = _webHostEnvironment.WebRootPath;
    //    string filePath = Path.Combine(wwwRootPath, "files", "example.txt");

    //    // Check if the file exists
    //    if (System.IO.File.Exists(filePath))
    //    {
    //        byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
    //        string fileName = "example.txt";

    //        return File(fileBytes, "application/octet-stream", fileName);
    //    }

    //    return NotFound();  // If file is not found
    //}
}