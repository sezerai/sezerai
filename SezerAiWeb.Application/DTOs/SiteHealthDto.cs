namespace SezerAiWeb.Application.DTOs;

public class SiteHealthDto
{
    public string Status { get; set; } = "Healthy"; // Healthy, Warning, Critical
    public List<HealthCheckItem> HealthChecks { get; set; } = new();
    public DateTime CheckedAt { get; set; }
}

public class HealthCheckItem
{
    public string Name { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string? Message { get; set; }
    public string? Details { get; set; }
}
