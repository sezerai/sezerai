using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Persistence.Configurations;

public class SiteMetricsConfiguration : IEntityTypeConfiguration<SiteMetrics>
{
    public void Configure(EntityTypeBuilder<SiteMetrics> builder)
    {
        builder.ToTable("SiteMetrics");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.PageViews).HasDefaultValue(0);
        builder.Property(m => m.UniqueVisitors).HasDefaultValue(0);
        builder.Property(m => m.BounceRate).HasDefaultValue(0);
        builder.Property(m => m.AverageSessionDuration).HasDefaultValue(0).HasPrecision(18, 2);
        builder.Property(m => m.NewUsers).HasDefaultValue(0);
        builder.Property(m => m.ReturningUsers).HasDefaultValue(0);
        builder.Property(m => m.OrganicSearchTraffic).HasDefaultValue(0);
        builder.Property(m => m.DirectTraffic).HasDefaultValue(0);
        builder.Property(m => m.ReferralTraffic).HasDefaultValue(0);
        builder.Property(m => m.SocialTraffic).HasDefaultValue(0);
        builder.Property(m => m.GoalCompletions).HasDefaultValue(0);
        builder.Property(m => m.ConversionRate).HasDefaultValue(0).HasPrecision(18, 2);

        // Indexes
        builder.HasIndex(m => new { m.WebsiteId, m.MetricDate }).IsUnique();
        builder.HasIndex(m => m.MetricDate);

        // Relationships
        builder.HasOne(m => m.Website)
            .WithMany(w => w.Metrics)
            .HasForeignKey(m => m.WebsiteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
