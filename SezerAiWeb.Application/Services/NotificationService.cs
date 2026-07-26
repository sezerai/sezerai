using SezerAiWeb.Application.DTOs;
using SezerAiWeb.Application.Interfaces;

namespace SezerAiWeb.Application.Services;

public class NotificationService : INotificationService
{
    public Task<AlertDto> CreateAlertAsync(string title, string message, string type, string? source = null)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AlertDto>> GetUnreadAlertsAsync()
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    public Task<bool> MarkAsReadAsync(Guid alertId)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    public Task<bool> MarkAllAsReadAsync()
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAlertAsync(Guid alertId)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
}
