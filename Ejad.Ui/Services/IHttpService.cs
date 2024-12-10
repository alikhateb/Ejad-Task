namespace Ejad.Ui.Services;

public interface IHttpService
{
    Task<T> GetAsync<T>(string url, CancellationToken cancellationToken = default);

    Task<byte[]> GetImageAsync(string url, CancellationToken cancellationToken = default);

    Task<T> PostAsync<T>(string url, T payload, CancellationToken cancellationToken = default);

    Task<T> PutAsync<T>(string url, T payload, CancellationToken cancellationToken = default);

    Task<TResponse> PostFormDataAsync<TRequest, TResponse>(string url, TRequest payload,
        CancellationToken cancellationToken = default);
}