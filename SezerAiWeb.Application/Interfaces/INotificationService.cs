using SezerAiWeb.Application.DTOs;

namespace SezerAiWeb.Application.Interfaces;

public interface INotificationService
{
    Task<AlertDto> CreateAlertAsync(string title, string message, string type, string? source = null);
    Task<IEnumerable<AlertDto>> GetUnreadAlertsAsync();
    Task<bool> MarkAsReadAsync(Guid alertId);
    Task<bool> MarkAllAsReadAsync();
    Task<bool> DeleteAlertAsync(Guid alertId);
}
