using SezerAiWeb.Domain.Common;

namespace SezerAiWeb.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsSystemRole { get; set; } = false;

    // Navigation Properties
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
