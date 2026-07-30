using SezerAiWeb.Domain.Common;

namespace SezerAiWeb.Domain.Entities;

public class SeoReport : BaseEntity
{
    public Guid WebsiteId { get; set; }
    public Website Website { get; set; } = null!;

    public DateTime ReportDate { get; set; }
    public int OverallScore { get; set; } = 0; // 0-100
    public int TechnicalScore { get; set; } = 0;
    public int ContentScore { get; set; } = 0;
    public int PerformanceScore { get; set; } = 0;
    public int MobileScore { get; set; } = 0;

    // Technical SEO
    public bool HasSitemap { get; set; } = false;
    public bool HasRobotsTxt { get; set; } = false;
    public bool HasSSL { get; set; } = false;
    public int BrokenLinks { get; set; } = 0;

    // Content SEO
    public int TotalPages { get; set; } = 0;
    public int IndexedPages { get; set; } = 0;
    public int DuplicateContent { get; set; } = 0;
    public int MissingMetaTitles { get; set; } = 0;
    public int MissingMetaDescriptions { get; set; } = 0;

    // Performance
    public decimal PageLoadTime { get; set; } = 0; // seconds
    public int TotalPageSize { get; set; } = 0; // KB
    
    public string? RecommendationsJson { get; set; } // JSON array of recommendations
    public string? IssuesJson { get; set; } // JSON array of issues
}
