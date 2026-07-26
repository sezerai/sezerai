namespace SezerAiWeb.Application.DTOs;

public class AlertDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string Type { get; set; } = "Info"; // Info, Warning, Error, Success
    public string? Source { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? WebsiteName { get; set; }
}
