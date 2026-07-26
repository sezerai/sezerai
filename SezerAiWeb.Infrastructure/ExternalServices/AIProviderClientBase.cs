using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SezerAiWeb.Infrastructure.ExternalServices;

/// <summary>
/// AI Sağlayıcıları (OpenAI, Gemini, Claude vb.) için temel istemci sınıfı
/// </summary>
public abstract class AIProviderClientBase
{
    protected readonly HttpClient HttpClient;
    protected readonly ILogger Logger;
    protected readonly string ApiKey;
    protected readonly string BaseUrl;

    protected AIProviderClientBase(
        HttpClient httpClient,
        ILogger logger,
        string apiKey,
        string baseUrl)
    {
        HttpClient = httpClient;
        Logger = logger;
        ApiKey = apiKey;
        BaseUrl = baseUrl;

        // API key'i header'a ekle
        if (!string.IsNullOrEmpty(apiKey))
        {
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        }
    }

    /// <summary>
    /// AI'dan metin tamamlama ister
    /// </summary>
    public async Task<AICompletionResponse?> GetCompletionAsync(
        string prompt,
        AICompletionOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            options ??= new AICompletionOptions();

            var request = BuildCompletionRequest(prompt, options);
            var endpoint = GetCompletionEndpoint();

            Logger.LogInformation("AI completion isteği gönderiliyor - Model: {Model}", options.Model);

            var response = await HttpClient.PostAsJsonAsync(endpoint, request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                Logger.LogError("AI API hatası: {StatusCode} - {Error}",
                    response.StatusCode, errorContent);
                return null;
            }

            var result = await response.Content.ReadFromJsonAsync<AICompletionResponse>(cancellationToken: cancellationToken);

            Logger.LogInformation("AI completion yanıtı alındı");

            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "AI completion hatası");
            return null;
        }
    }

    /// <summary>
    /// AI'dan stream olarak yanıt alır
    /// </summary>
    public async IAsyncEnumerable<string> GetStreamingCompletionAsync(
        string prompt,
        AICompletionOptions? options = null)
    {
        options ??= new AICompletionOptions();

        var request = BuildCompletionRequest(prompt, options);
        var endpoint = GetCompletionEndpoint();

        Logger.LogInformation("AI streaming completion isteği gönderiliyor");

        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, endpoint)
        {
            Content = JsonContent.Create(request)
        };

        using var response = await HttpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);

        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();

            if (!string.IsNullOrWhiteSpace(line))
            {
                yield return line;
            }
        }
    }

    /// <summary>
    /// Görsel analizi yapar
    /// </summary>
    public async Task<AIVisionResponse?> AnalyzeImageAsync(
        string imageUrl,
        string prompt,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = BuildVisionRequest(imageUrl, prompt);
            var endpoint = GetVisionEndpoint();

            Logger.LogInformation("AI vision isteği gönderiliyor");

            var response = await HttpClient.PostAsJsonAsync(endpoint, request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AIVisionResponse>(cancellationToken: cancellationToken);

            Logger.LogInformation("AI vision yanıtı alındı");

            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "AI vision hatası");
            return null;
        }
    }

    /// <summary>
    /// Metin embedding'i oluşturur
    /// </summary>
    public async Task<AIEmbeddingResponse?> GetEmbeddingAsync(
        string text,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = BuildEmbeddingRequest(text);
            var endpoint = GetEmbeddingEndpoint();

            Logger.LogInformation("AI embedding isteği gönderiliyor");

            var response = await HttpClient.PostAsJsonAsync(endpoint, request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AIEmbeddingResponse>(cancellationToken: cancellationToken);

            Logger.LogInformation("AI embedding yanıtı alındı");

            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "AI embedding hatası");
            return null;
        }
    }

    // Abstract metodlar - Her sağlayıcı kendi implementasyonunu yapmalı
    protected abstract object BuildCompletionRequest(string prompt, AICompletionOptions options);
    protected abstract object BuildVisionRequest(string imageUrl, string prompt);
    protected abstract object BuildEmbeddingRequest(string text);
    protected abstract string GetCompletionEndpoint();
    protected abstract string GetVisionEndpoint();
    protected abstract string GetEmbeddingEndpoint();
}

public class AICompletionOptions
{
    public string Model { get; set; } = "gpt-3.5-turbo";
    public double Temperature { get; set; } = 0.7;
    public int MaxTokens { get; set; } = 1000;
    public double TopP { get; set; } = 1.0;
    public int N { get; set; } = 1;
    public bool Stream { get; set; } = false;
    public string[]? Stop { get; set; }
}

public class AICompletionResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("object")]
    public string? Object { get; set; }

    [JsonPropertyName("created")]
    public long Created { get; set; }

    [JsonPropertyName("model")]
    public string? Model { get; set; }

    [JsonPropertyName("choices")]
    public List<AIChoice>? Choices { get; set; }

    [JsonPropertyName("usage")]
    public AIUsage? Usage { get; set; }
}

public class AIChoice
{
    [JsonPropertyName("index")]
    public int Index { get; set; }

    [JsonPropertyName("message")]
    public AIMessage? Message { get; set; }

    [JsonPropertyName("finish_reason")]
    public string? FinishReason { get; set; }
}

public class AIMessage
{
    [JsonPropertyName("role")]
    public string? Role { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }
}

public class AIUsage
{
    [JsonPropertyName("prompt_tokens")]
    public int PromptTokens { get; set; }

    [JsonPropertyName("completion_tokens")]
    public int CompletionTokens { get; set; }

    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; set; }
}

public class AIVisionResponse
{
    public string? Description { get; set; }
    public List<string>? Labels { get; set; }
    public double Confidence { get; set; }
}

public class AIEmbeddingResponse
{
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    [JsonPropertyName("data")]
    public List<AIEmbeddingData>? Data { get; set; }

    [JsonPropertyName("model")]
    public string? Model { get; set; }

    [JsonPropertyName("usage")]
    public AIUsage? Usage { get; set; }
}

public class AIEmbeddingData
{
    [JsonPropertyName("object")]
    public string? Object { get; set; }

    [JsonPropertyName("embedding")]
    public float[]? Embedding { get; set; }

    [JsonPropertyName("index")]
    public int Index { get; set; }
}
