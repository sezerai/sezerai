using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace SezerAiWeb.Infrastructure.ExternalServices;

/// <summary>
/// Google API'leri için temel istemci sınıfı
/// </summary>
public abstract class GoogleApiClientBase
{
    protected readonly HttpClient HttpClient;
    protected readonly ILogger Logger;
    protected readonly string ApiKey;

    protected GoogleApiClientBase(
        HttpClient httpClient,
        ILogger logger,
        string apiKey)
    {
        HttpClient = httpClient;
        Logger = logger;
        ApiKey = apiKey;
    }

    protected async Task<TResponse?> SendGetRequestAsync<TResponse>(
        string endpoint,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var url = BuildUrl(endpoint, queryParams);

            Logger.LogInformation("Google API GET isteği gönderiliyor: {Url}", url);

            var response = await HttpClient.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);

            Logger.LogInformation("Google API yanıtı başarıyla alındı");

            return result;
        }
        catch (HttpRequestException ex)
        {
            Logger.LogError(ex, "Google API isteği başarısız: {Endpoint}", endpoint);
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Beklenmeyen hata: {Endpoint}", endpoint);
            throw;
        }
    }

    protected async Task<TResponse?> SendPostRequestAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var url = BuildUrl(endpoint, queryParams);

            Logger.LogInformation("Google API POST isteği gönderiliyor: {Url}", url);

            var response = await HttpClient.PostAsJsonAsync(url, request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);

            Logger.LogInformation("Google API POST yanıtı başarıyla alındı");

            return result;
        }
        catch (HttpRequestException ex)
        {
            Logger.LogError(ex, "Google API POST isteği başarısız: {Endpoint}", endpoint);
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Beklenmeyen hata: {Endpoint}", endpoint);
            throw;
        }
    }

    private string BuildUrl(string endpoint, Dictionary<string, string>? queryParams)
    {
        var baseUrl = endpoint;

        if (!string.IsNullOrEmpty(ApiKey))
        {
            queryParams ??= new Dictionary<string, string>();
            queryParams["key"] = ApiKey;
        }

        if (queryParams != null && queryParams.Any())
        {
            var queryString = string.Join("&", queryParams.Select(kvp =>
                $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));

            baseUrl = $"{baseUrl}?{queryString}";
        }

        return baseUrl;
    }

    protected async Task<string> SendRawRequestAsync(
        string endpoint,
        HttpMethod method,
        HttpContent? content = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new HttpRequestMessage(method, endpoint)
            {
                Content = content
            };

            Logger.LogInformation("Google API ham istek gönderiliyor: {Method} {Endpoint}", method, endpoint);

            var response = await HttpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync(cancellationToken);

            Logger.LogInformation("Google API ham yanıt alındı");

            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Google API ham istek hatası: {Endpoint}", endpoint);
            throw;
        }
    }
}

/// <summary>
/// Google API yanıt modeli için base class
/// </summary>
public abstract class GoogleApiResponse
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}
