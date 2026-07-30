using SezerAiWeb.Domain.Common;

namespace SezerAiWeb.Domain.Entities;

public class AlertNotification : BaseEntity
{
    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string Type { get; set; } = "Info"; // Info, Success, Warning, Error
    public string Category { get; set; } = null!; // Security, Performance, SEO, System
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    public string Priority { get; set; } = "Normal"; // Low, Normal, High, Critical
    public string? ActionUrl { get; set; }
    public string? ActionText { get; set; }
    public Guid? WebsiteId { get; set; }
    public Website? Website { get; set; }
}
