using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Persistence.Configurations;

public class SeoReportConfiguration : IEntityTypeConfiguration<SeoReport>
{
    public void Configure(EntityTypeBuilder<SeoReport> builder)
    {
        builder.ToTable("SeoReports");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.OverallScore).HasDefaultValue(0);
        builder.Property(s => s.TechnicalScore).HasDefaultValue(0);
        builder.Property(s => s.ContentScore).HasDefaultValue(0);
        builder.Property(s => s.PerformanceScore).HasDefaultValue(0);
        builder.Property(s => s.MobileScore).HasDefaultValue(0);
        builder.Property(s => s.HasSitemap).HasDefaultValue(false);
        builder.Property(s => s.HasRobotsTxt).HasDefaultValue(false);
        builder.Property(s => s.HasSSL).HasDefaultValue(false);
        builder.Property(s => s.BrokenLinks).HasDefaultValue(0);
        builder.Property(s => s.TotalPages).HasDefaultValue(0);
        builder.Property(s => s.IndexedPages).HasDefaultValue(0);
        builder.Property(s => s.DuplicateContent).HasDefaultValue(0);
        builder.Property(s => s.MissingMetaTitles).HasDefaultValue(0);
        builder.Property(s => s.MissingMetaDescriptions).HasDefaultValue(0);
        builder.Property(s => s.PageLoadTime).HasDefaultValue(0).HasPrecision(18, 2);
        builder.Property(s => s.TotalPageSize).HasDefaultValue(0);

        builder.Property(s => s.RecommendationsJson)
            .HasColumnType("jsonb");

        builder.Property(s => s.IssuesJson)
            .HasColumnType("jsonb");

        // Indexes
        builder.HasIndex(s => new { s.WebsiteId, s.ReportDate }).IsUnique();
        builder.HasIndex(s => s.ReportDate);
        builder.HasIndex(s => s.OverallScore);

        // Relationships
        builder.HasOne(s => s.Website)
            .WithMany(w => w.SeoReports)
            .HasForeignKey(s => s.WebsiteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
