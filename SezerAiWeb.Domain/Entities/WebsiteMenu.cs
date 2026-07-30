using SezerAiWeb.Domain.Common;

namespace SezerAiWeb.Domain.Entities;

public class WebsiteMenu : BaseEntity
{
    public Guid WebsiteId { get; set; }
    public Website Website { get; set; } = null!;

    public string Title { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? Icon { get; set; }
    public int Order { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public bool OpenInNewTab { get; set; } = false;

    // Self-referencing for hierarchical menu structure
    public Guid? ParentId { get; set; }
    public WebsiteMenu? Parent { get; set; }
    public ICollection<WebsiteMenu> Children { get; set; } = new List<WebsiteMenu>();

    public string? CssClass { get; set; }
    public string? Target { get; set; }
    public bool RequiresAuth { get; set; } = false;
    public string? AllowedRoles { get; set; } // Comma-separated role names
}
