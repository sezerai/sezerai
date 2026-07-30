using SezerAiWeb.Domain.Common;

namespace SezerAiWeb.Domain.Entities;

public class AIAgentLog : BaseEntity
{
    public string AgentName { get; set; } = null!; // ContentWriter, SEOAnalyzer, ImageGenerator, etc.
    public string TaskType { get; set; } = null!; // GenerateContent, AnalyzeSEO, OptimizeImage, etc.
    public string? InputData { get; set; } // JSON format
    public string? OutputData { get; set; } // JSON format
    public bool IsSuccess { get; set; } = false;
    public string? ErrorMessage { get; set; }
    public TimeSpan? Duration { get; set; }
    public int? TokensUsed { get; set; }
    public decimal? Cost { get; set; }
    public Guid? WebsiteId { get; set; }
    public Website? Website { get; set; }
}
