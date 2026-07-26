namespace SezerAiWeb.Application.DTOs;

public class WebsiteDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Domain { get; set; } = null!;
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public string? FaviconUrl { get; set; }
    public bool IsActive { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? Address { get; set; }
    public string? SocialMediaLinks { get; set; }
    public string? GoogleAnalyticsId { get; set; }
    public string? GoogleSearchConsoleId { get; set; }
    public string? GoogleTagManagerId { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaKeywords { get; set; }
    public string? Theme { get; set; }
    public string? Language { get; set; }
    public string? Currency { get; set; }
    public string? TimeZone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
