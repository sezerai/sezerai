using SezerAiWeb.Domain.Common;

namespace SezerAiWeb.Domain.Entities;

public class SiteMetrics : BaseEntity
{
    public Guid WebsiteId { get; set; }
    public Website Website { get; set; } = null!;

    public DateTime MetricDate { get; set; }
    public int PageViews { get; set; } = 0;
    public int UniqueVisitors { get; set; } = 0;
    public int BounceRate { get; set; } = 0;
    public decimal AverageSessionDuration { get; set; } = 0;
    public int NewUsers { get; set; } = 0;
    public int ReturningUsers { get; set; } = 0;

    // SEO Metrics
    public int OrganicSearchTraffic { get; set; } = 0;
    public int DirectTraffic { get; set; } = 0;
    public int ReferralTraffic { get; set; } = 0;
    public int SocialTraffic { get; set; } = 0;

    // Conversion Metrics
    public int GoalCompletions { get; set; } = 0;
    public decimal ConversionRate { get; set; } = 0;
}
