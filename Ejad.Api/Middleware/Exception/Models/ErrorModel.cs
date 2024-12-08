using System.Net;

namespace Ejad.Api.Middleware.Exception.Models;

/// <summary>
///     the exception response model
/// </summary>
public class ErrorModel
{
    /// <summary>
    ///     the error code should implement the business model dictionary
    /// </summary>
    public string ErrorCode { get; set; }

    /// <summary>
    ///     the response status code
    /// </summary>
    public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;

    /// <summary>
    ///     the exception type normally it will be nameof(YourCustomException)
    /// </summary>
    public string ExceptionType { get; set; }

    /// <summary>
    ///     exception message
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    ///     error list
    /// </summary>
    public List<ValidationError> Errors { get; set; } = new();

    /// <summary>
    ///     inner exception
    /// </summary>
    public System.Exception InnerException { get; set; }

    /// <summary>
    ///     StackTrace
    /// </summary>
    public string StackTrace { get; set; }
}