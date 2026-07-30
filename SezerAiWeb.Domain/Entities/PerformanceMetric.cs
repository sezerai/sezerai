using SezerAiWeb.Domain.Common;

namespace SezerAiWeb.Domain.Entities;

public class PerformanceMetric : BaseEntity
{
    public Guid? WebsiteId { get; set; }
    public Website? Website { get; set; }

    public DateTime MeasuredAt { get; set; } = DateTime.UtcNow;
    public string MetricType { get; set; } = null!; // PageLoad, ApiResponse, DatabaseQuery, etc.
    public string? EndpointOrPage { get; set; }
    
    // Performance Data
    public decimal ResponseTime { get; set; } = 0; // milliseconds
    public decimal MinResponseTime { get; set; } = 0;
    public decimal MaxResponseTime { get; set; } = 0;
    public decimal AvgResponseTime { get; set; } = 0;
    
    // Request Data
    public int TotalRequests { get; set; } = 0;
    public int SuccessfulRequests { get; set; } = 0;
    public int FailedRequests { get; set; } = 0;
    
    // Resource Usage
    public long? MemoryUsed { get; set; } // bytes
    public int? CpuTime { get; set; } // milliseconds
    
    public string? AdditionalDataJson { get; set; } // JSON for extensibility
}
