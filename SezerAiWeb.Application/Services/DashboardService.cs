using AutoMapper;
using SezerAiWeb.Application.DTOs;
using SezerAiWeb.Application.Interfaces;
using SezerAiWeb.Application.Repositories;

namespace SezerAiWeb.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DashboardService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DashboardMetricsDto> GetMetricsAsync()
    {
        var websites = await _unitOfWork.Websites.GetAllAsync();
        var blogs = await _unitOfWork.BlogYazilari.GetAllAsync();
        var menus = await _unitOfWork.WebsiteMenus.GetAllAsync();

        return new DashboardMetricsDto
        {
            TotalWebsites = websites.Count(),
            ActiveWebsites = websites.Count(w => w.IsActive),
            InactiveWebsites = websites.Count(w => !w.IsActive),
            TotalBlogs = blogs.Count(),
            PublishedBlogs = blogs.Count(b => b.YayinTarihi <= DateTime.UtcNow),
            DraftBlogs = blogs.Count(b => b.YayinTarihi > DateTime.UtcNow),
            TotalMenuItems = menus.Count(),
            TotalProjects = 0,
            ActiveProjects = 0,
            LastUpdated = DateTime.UtcNow
        };
    }

    public async Task<SiteHealthDto> GetHealthStatusAsync()
    {
        var allHealths = await _unitOfWork.SystemHealths.GetAllAsync();
        var latestHealth = allHealths.OrderByDescending(h => h.CheckedAt).FirstOrDefault();

        if (latestHealth == null)
        {
            return new SiteHealthDto
            {
                Status = "Unknown",
                CheckedAt = DateTime.UtcNow,
                HealthChecks = new List<HealthCheckItem>
                {
                    new HealthCheckItem { Name = "System", Status = "Unknown", Message = "No health check data available" }
                }
            };
        }

        var healthChecks = new List<HealthCheckItem>
        {
            new HealthCheckItem { Name = "Database", Status = latestHealth.IsDatabaseOnline ? "Healthy" : "Critical", Message = latestHealth.IsDatabaseOnline ? "Connected" : "Offline", Details = $"Response time: {latestHealth.DatabaseResponseTime}ms" },
            new HealthCheckItem { Name = "Cache", Status = latestHealth.IsCacheOnline ? "Healthy" : "Warning", Message = latestHealth.IsCacheOnline ? "Connected" : "Offline", Details = $"Response time: {latestHealth.CacheResponseTime}ms" },
            new HealthCheckItem { Name = "CPU", Status = latestHealth.CpuUsage < 70 ? "Healthy" : latestHealth.CpuUsage < 90 ? "Warning" : "Critical", Message = $"{latestHealth.CpuUsage}% usage" },
            new HealthCheckItem { Name = "Memory", Status = latestHealth.MemoryUsage < 80 ? "Healthy" : latestHealth.MemoryUsage < 95 ? "Warning" : "Critical", Message = $"{latestHealth.MemoryUsage}% usage" },
            new HealthCheckItem { Name = "Disk", Status = latestHealth.DiskUsage < 80 ? "Healthy" : latestHealth.DiskUsage < 95 ? "Warning" : "Critical", Message = $"{latestHealth.DiskUsage}% usage" }
        };

        return new SiteHealthDto { Status = latestHealth.Status, HealthChecks = healthChecks, CheckedAt = latestHealth.CheckedAt };
    }

    public async Task<IEnumerable<AlertDto>> GetRecentAlertsAsync(int count = 10)
    {
        var alerts = (await _unitOfWork.AlertNotifications.GetAllAsync()).OrderByDescending(a => a.CreatedAt).Take(count);
        return _mapper.Map<IEnumerable<AlertDto>>(alerts);
    }

    public async Task<bool> MarkAlertAsReadAsync(Guid alertId)
    {
        var alert = await _unitOfWork.AlertNotifications.GetByIdAsync(alertId);
        if (alert == null) return false;

        alert.IsRead = true;
        alert.ReadAt = DateTime.UtcNow;
        alert.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.AlertNotifications.UpdateAsync(alert);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
