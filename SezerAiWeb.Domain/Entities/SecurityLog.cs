using SezerAiWeb.Domain.Common;

namespace SezerAiWeb.Domain.Entities;

public class SecurityLog : BaseEntity
{
    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public string EventType { get; set; } = null!; // Login, Logout, PasswordChange, FailedLogin, etc.
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? Location { get; set; } // GeoIP data
    public bool IsSuccess { get; set; } = false;
    public string? FailureReason { get; set; }
    public string? AdditionalData { get; set; } // JSON format
    public string Severity { get; set; } = "Info"; // Info, Warning, Critical
}
