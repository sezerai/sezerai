using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace SezerAiWeb.Infrastructure.ExternalServices;

/// <summary>
/// Telegram Bot API için temel istemci sınıfı
/// </summary>
public abstract class TelegramBotClientBase
{
    protected readonly HttpClient HttpClient;
    protected readonly ILogger Logger;
    protected readonly string BotToken;
    protected readonly string BaseUrl;

    protected TelegramBotClientBase(
        HttpClient httpClient,
        ILogger logger,
        string botToken)
    {
        HttpClient = httpClient;
        Logger = logger;
        BotToken = botToken;
        BaseUrl = $"https://api.telegram.org/bot{botToken}";
    }

    /// <summary>
    /// Telegram'a mesaj gönderir
    /// </summary>
    public async Task<bool> SendMessageAsync(
        long chatId,
        string text,
        string? parseMode = "HTML",
        CancellationToken cancellationToken = default)
    {
        try
        {
            var endpoint = $"{BaseUrl}/sendMessage";

            var request = new
            {
                chat_id = chatId,
                text,
                parse_mode = parseMode
            };

            Logger.LogInformation("Telegram mesajı gönderiliyor - ChatId: {ChatId}", chatId);

            var response = await HttpClient.PostAsJsonAsync(endpoint, request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TelegramApiResponse>(cancellationToken: cancellationToken);

            if (result?.Ok == true)
            {
                Logger.LogInformation("Telegram mesajı başarıyla gönderildi");
                return true;
            }

            Logger.LogWarning("Telegram mesajı gönderilemedi: {Error}", result?.Description);
            return false;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Telegram mesaj gönderme hatası");
            return false;
        }
    }

    /// <summary>
    /// Telegram'a fotoğraf gönderir
    /// </summary>
    public async Task<bool> SendPhotoAsync(
        long chatId,
        string photoUrl,
        string? caption = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var endpoint = $"{BaseUrl}/sendPhoto";

            var request = new
            {
                chat_id = chatId,
                photo = photoUrl,
                caption
            };

            Logger.LogInformation("Telegram fotoğrafı gönderiliyor - ChatId: {ChatId}", chatId);

            var response = await HttpClient.PostAsJsonAsync(endpoint, request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TelegramApiResponse>(cancellationToken: cancellationToken);

            return result?.Ok == true;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Telegram fotoğraf gönderme hatası");
            return false;
        }
    }

    /// <summary>
    /// Telegram'a doküman gönderir
    /// </summary>
    public async Task<bool> SendDocumentAsync(
        long chatId,
        Stream document,
        string fileName,
        string? caption = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var endpoint = $"{BaseUrl}/sendDocument";

            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(chatId.ToString()), "chat_id");

            if (!string.IsNullOrEmpty(caption))
            {
                content.Add(new StringContent(caption), "caption");
            }

            var streamContent = new StreamContent(document);
            content.Add(streamContent, "document", fileName);

            Logger.LogInformation("Telegram dokümanı gönderiliyor - ChatId: {ChatId}, FileName: {FileName}",
                chatId, fileName);

            var response = await HttpClient.PostAsync(endpoint, content, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TelegramApiResponse>(cancellationToken: cancellationToken);

            return result?.Ok == true;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Telegram doküman gönderme hatası");
            return false;
        }
    }

    /// <summary>
    /// Telegram webhook'u ayarlar
    /// </summary>
    public async Task<bool> SetWebhookAsync(
        string webhookUrl,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var endpoint = $"{BaseUrl}/setWebhook";

            var request = new { url = webhookUrl };

            Logger.LogInformation("Telegram webhook ayarlanıyor: {WebhookUrl}", webhookUrl);

            var response = await HttpClient.PostAsJsonAsync(endpoint, request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TelegramApiResponse>(cancellationToken: cancellationToken);

            if (result?.Ok == true)
            {
                Logger.LogInformation("Telegram webhook başarıyla ayarlandı");
                return true;
            }

            Logger.LogWarning("Telegram webhook ayarlanamadı: {Error}", result?.Description);
            return false;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Telegram webhook ayarlama hatası");
            return false;
        }
    }

    /// <summary>
    /// Bot bilgilerini alır
    /// </summary>
    public async Task<TelegramBotInfo?> GetMeAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var endpoint = $"{BaseUrl}/getMe";

            Logger.LogInformation("Telegram bot bilgileri alınıyor");

            var response = await HttpClient.GetAsync(endpoint, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TelegramGetMeResponse>(cancellationToken: cancellationToken);

            return result?.Result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Telegram bot bilgileri alma hatası");
            return null;
        }
    }
}

public class TelegramApiResponse
{
    public bool Ok { get; set; }
    public string? Description { get; set; }
    public JsonElement? Result { get; set; }
}

public class TelegramGetMeResponse
{
    public bool Ok { get; set; }
    public TelegramBotInfo? Result { get; set; }
}

public class TelegramBotInfo
{
    public long Id { get; set; }
    public bool IsBot { get; set; }
    public string? FirstName { get; set; }
    public string? Username { get; set; }
    public bool CanJoinGroups { get; set; }
    public bool CanReadAllGroupMessages { get; set; }
    public bool SupportsInlineQueries { get; set; }
}
