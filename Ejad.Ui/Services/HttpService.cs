using Microsoft.AspNetCore.Components.Forms;
using System.Collections;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Ejad.Ui.Services;

public class HttpService(HttpClient httpClient) : IHttpService
{
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true, };

    public async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.GetAsync(url, cancellationToken);
        var content = response.Content;
        if (!response.IsSuccessStatusCode)
        {
            throw new AggregateException(await content.ReadAsStringAsync(cancellationToken));
        }
        return await content.ReadFromJsonAsync<T>(_options, cancellationToken);
    }

    public async Task<byte[]> GetImageAsync(string url, CancellationToken cancellationToken = default)
    {
        var imageBytes = await httpClient.GetByteArrayAsync(url, cancellationToken);
        return imageBytes;
    }

    public async Task<T> PostAsync<T>(string url, T payload, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PostAsJsonAsync(url, payload, cancellationToken: cancellationToken);
        var content = response.Content;
        if (!response.IsSuccessStatusCode)
        {
            throw new AggregateException(await content.ReadAsStringAsync(cancellationToken));
        }
        return await content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
    }

    public async Task<TResponse> PostFormDataAsync<TRequest, TResponse>(string url, TRequest payload,
        CancellationToken cancellationToken = default)
    {
        using var multipartContent = new MultipartFormDataContent();
        var contentType = payload.GetType();

        foreach (var property in contentType.GetProperties())
        {
            if (property.PropertyType.IsAssignableTo(typeof(IBrowserFile)))
            {
                continue;
            }

            if (property.PropertyType.IsAssignableTo(typeof(IList)))
            {
                var list = (IList)property.GetValue(payload);

                foreach (var item in list)
                {
                    var listItem = JsonSerializer.Serialize(item, _options);
                    Console.WriteLine(listItem);

                    var listItemContent = new StringContent(listItem, Encoding.UTF8,
                        MediaTypeNames.Text.Plain);

                    multipartContent.Add(listItemContent, property.Name);
                }

                continue;
            }

            var stringContent = new StringContent(property.GetValue(payload)!.ToString()!, Encoding.UTF8,
                MediaTypeNames.Text.Plain);

            multipartContent.Add(stringContent, property.Name);
        }

        var fileInfo = contentType.GetProperties().FirstOrDefault(x => x.PropertyType.IsAssignableTo(typeof(IBrowserFile)));

        if (fileInfo is not null)
        {
            var file = (IBrowserFile)fileInfo.GetValue(payload);

            var ms = new MemoryStream();
            await file!.OpenReadStream(10 * 1024 * 1024, cancellationToken).CopyToAsync(ms, cancellationToken);
            var bytes = ms.ToArray();

            var imageContent = new ByteArrayContent(bytes);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(MediaTypeNames.Image.Jpeg);

            multipartContent.Add(imageContent, fileInfo.Name, file.Name);
        }

        var response = await httpClient.PostAsync(url, multipartContent, cancellationToken);
        var content = response.Content;
        if (!response.IsSuccessStatusCode)
        {
            throw new AggregateException(await content.ReadAsStringAsync(cancellationToken));
        }
        return await content.ReadFromJsonAsync<TResponse>(_options, cancellationToken);
    }

    public async Task<T> PutAsync<T>(string url, T payload, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync(url, payload, cancellationToken: cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
    }
}