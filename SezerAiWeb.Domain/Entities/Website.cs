using SezerAiWeb.Domain.Common;
using SezerAiWeb.Domain.Enums;

namespace SezerAiWeb.Domain.Entities;

public class Website : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Domain { get; set; } = null!;
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public string? FaviconUrl { get; set; }
    public bool IsActive { get; set; } = true;

    // Website Type & Configuration
    public WebsiteTipi WebsiteTipi { get; set; } = WebsiteTipi.Kurumsal;
    public string? ConnectionString { get; set; } // Eğer kendi DB'si varsa
    public string? ApiEndpoint { get; set; } // API base URL
    public string? ApiKey { get; set; } // API authentication key

    // SSL & Domain Tracking
    public DateTime? SslExpiryDate { get; set; }
    public DateTime? DomainExpiryDate { get; set; }
    public string? SslProvider { get; set; } // Let's Encrypt, DigiCert, etc.

    // Contact Information
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? Address { get; set; }
    public string? SocialMediaLinks { get; set; } // JSON format

    // Google Services
    public string? GoogleAnalyticsId { get; set; }
    public string? GoogleSearchConsoleId { get; set; }
    public string? GoogleTagManagerId { get; set; }

    // SEO & Metadata
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaKeywords { get; set; }

    // Localization
    public string? Theme { get; set; }
    public string? Language { get; set; } = "tr-TR";
    public string? Currency { get; set; } = "TRY";
    public string? TimeZone { get; set; } = "Europe/Istanbul";

    // Foreign Keys
    public Guid OwnerId { get; set; }

    // Navigation Properties
    public User Owner { get; set; } = null!;
    public ICollection<WebsiteMenu> Menus { get; set; } = new List<WebsiteMenu>();
    public ICollection<SiteMetrics> Metrics { get; set; } = new List<SiteMetrics>();
    public ICollection<SeoReport> SeoReports { get; set; } = new List<SeoReport>();
}
