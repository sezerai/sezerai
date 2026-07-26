using SezerAiWeb.Application.DTOs;

namespace SezerAiWeb.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardMetricsDto> GetMetricsAsync();
    Task<SiteHealthDto> GetHealthStatusAsync();
    Task<IEnumerable<AlertDto>> GetRecentAlertsAsync(int count = 10);
    Task<bool> MarkAlertAsReadAsync(Guid alertId);
}
