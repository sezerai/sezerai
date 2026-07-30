using SezerAiWeb.Domain.Common;

namespace SezerAiWeb.Domain.Entities;

public class SystemHealth : BaseEntity
{
    public DateTime CheckedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Healthy"; // Healthy, Warning, Critical
    
    // Server Health
    public decimal CpuUsage { get; set; } = 0; // percentage
    public decimal MemoryUsage { get; set; } = 0; // percentage
    public decimal DiskUsage { get; set; } = 0; // percentage
    
    // Application Health
    public bool IsDatabaseOnline { get; set; } = true;
    public decimal DatabaseResponseTime { get; set; } = 0; // ms
    public bool IsCacheOnline { get; set; } = true;
    public decimal CacheResponseTime { get; set; } = 0; // ms
    
    // Service Health
    public bool IsEmailServiceOnline { get; set; } = true;
    public bool IsStorageServiceOnline { get; set; } = true;
    public bool IsGoogleServicesOnline { get; set; } = true;
    
    // Metrics
    public int ActiveUsers { get; set; } = 0;
    public int QueuedJobs { get; set; } = 0;
    public int FailedJobs { get; set; } = 0;
    
    public string? AdditionalDataJson { get; set; } // JSON for extensibility
}
