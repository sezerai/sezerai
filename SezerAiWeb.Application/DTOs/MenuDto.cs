namespace SezerAiWeb.Application.DTOs;

public class MenuDto
{
    public Guid Id { get; set; }
    public Guid WebsiteId { get; set; }
    public string Title { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? Icon { get; set; }
    public int Order { get; set; }
    public bool IsActive { get; set; }
    public bool OpenInNewTab { get; set; }
    public Guid? ParentId { get; set; }
    public string? CssClass { get; set; }
    public string? Target { get; set; }
    public bool RequiresAuth { get; set; }
    public string? AllowedRoles { get; set; }
    public List<MenuDto> Children { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}

public class MenuCreateDto
{
    public Guid WebsiteId { get; set; }
    public string Title { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? Icon { get; set; }
    public int Order { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public bool OpenInNewTab { get; set; } = false;
    public Guid? ParentId { get; set; }
    public string? CssClass { get; set; }
    public string? Target { get; set; }
    public bool RequiresAuth { get; set; } = false;
    public string? AllowedRoles { get; set; }
}

public class MenuUpdateDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? Icon { get; set; }
    public int Order { get; set; }
    public bool IsActive { get; set; }
    public bool OpenInNewTab { get; set; }
    public Guid? ParentId { get; set; }
    public string? CssClass { get; set; }
    public string? Target { get; set; }
    public bool RequiresAuth { get; set; }
    public string? AllowedRoles { get; set; }
}
