using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace SezerAiWeb.Infrastructure.Hubs;

/// <summary>
/// Dashboard için gerçek zamanlı bildirim hub'ı
/// </summary>
public class DashboardHub : Hub
{
    private readonly ILogger<DashboardHub> _logger;

    public DashboardHub(ILogger<DashboardHub> logger)
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation("Client bağlandı: {ConnectionId}", Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("Client ayrıldı: {ConnectionId}", Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// İstemciyi belirli bir gruba ekler
    /// </summary>
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        _logger.LogInformation("Client {ConnectionId} gruba eklendi: {Group}", Context.ConnectionId, groupName);
    }

    /// <summary>
    /// İstemciyi belirli bir gruptan çıkarır
    /// </summary>
    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        _logger.LogInformation("Client {ConnectionId} gruptan çıkarıldı: {Group}", Context.ConnectionId, groupName);
    }

    /// <summary>
    /// Tüm istemcilere metrik güncellemesi gönderir
    /// </summary>
    public async Task BroadcastMetricsUpdate(object metrics)
    {
        await Clients.All.SendAsync("ReceiveMetricsUpdate", metrics);
    }

    /// <summary>
    /// Belirli bir gruba bildirim gönderir
    /// </summary>
    public async Task SendNotificationToGroup(string groupName, string title, string message, string type)
    {
        await Clients.Group(groupName).SendAsync("ReceiveNotification", new
        {
            title,
            message,
            type,
            timestamp = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Belirli bir kullanıcıya bildirim gönderir
    /// </summary>
    public async Task SendNotificationToUser(string userId, string title, string message, string type)
    {
        await Clients.User(userId).SendAsync("ReceiveNotification", new
        {
            title,
            message,
            type,
            timestamp = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Tüm kullanıcılara sistem bildirimi gönderir
    /// </summary>
    public async Task BroadcastSystemNotification(string title, string message, string type)
    {
        await Clients.All.SendAsync("ReceiveSystemNotification", new
        {
            title,
            message,
            type,
            timestamp = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Tüm kullanıcılara kritik alarm gönderir
    /// </summary>
    public async Task SendAlert(string title, string message, string severity, string websiteId)
    {
        await Clients.All.SendAsync("ReceiveAlert", new
        {
            title,
            message,
            severity,
            websiteId,
            timestamp = DateTime.UtcNow
        });
    }
}

/// <summary>
/// SignalR Hub'ı için extension metodları
/// </summary>
public static class DashboardHubExtensions
{
    /// <summary>
    /// Hub üzerinden sistem metriklerini yayınlar
    /// </summary>
    public static async Task PublishMetrics(this IHubContext<DashboardHub> hubContext, object metrics)
    {
        await hubContext.Clients.All.SendAsync("ReceiveMetricsUpdate", metrics);
    }

    /// <summary>
    /// Hub üzerinden bildirim gönderir
    /// </summary>
    public static async Task SendNotification(
        this IHubContext<DashboardHub> hubContext,
        string title,
        string message,
        string type = "info")
    {
        await hubContext.Clients.All.SendAsync("ReceiveNotification", new
        {
            title,
            message,
            type,
            timestamp = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Belirli bir gruba bildirim gönderir
    /// </summary>
    public static async Task SendNotificationToGroup(
        this IHubContext<DashboardHub> hubContext,
        string groupName,
        string title,
        string message,
        string type = "info")
    {
        await hubContext.Clients.Group(groupName).SendAsync("ReceiveNotification", new
        {
            title,
            message,
            type,
            timestamp = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Tüm kullanıcılara kritik alarm gönderir
    /// </summary>
    public static async Task SendAlert(
        this IHubContext<DashboardHub> hubContext,
        string title,
        string message,
        string severity,
        string websiteId)
    {
        await hubContext.Clients.All.SendAsync("ReceiveAlert", new
        {
            title,
            message,
            severity,
            websiteId,
            timestamp = DateTime.UtcNow
        });
    }
}
