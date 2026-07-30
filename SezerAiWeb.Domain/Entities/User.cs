using SezerAiWeb.Domain.Common;

namespace SezerAiWeb.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? ProfilePicture { get; set; }
    public bool IsEmailVerified { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    // Navigation Properties
    public ICollection<Website> Websites { get; set; } = new List<Website>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<SecurityLog> SecurityLogs { get; set; } = new List<SecurityLog>();
}
