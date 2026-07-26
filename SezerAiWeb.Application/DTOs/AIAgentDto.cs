namespace SezerAiWeb.Application.DTOs;

public class AIAgentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!; // Content, SEO, Analytics, Security, etc.
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public string? Configuration { get; set; } // JSON configuration
    public DateTime? LastRunAt { get; set; }
    public string? LastRunStatus { get; set; }
    public string? LastRunResult { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class AIAgentRunRequestDto
{
    public Guid AgentId { get; set; }
    public string? Parameters { get; set; } // JSON parameters
}

public class AIAgentRunResultDto
{
    public Guid RunId { get; set; }
    public string Status { get; set; } = null!;
    public string? Result { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
