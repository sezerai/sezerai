using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Persistence.Configurations;

public class PerformanceMetricConfiguration : IEntityTypeConfiguration<PerformanceMetric>
{
    public void Configure(EntityTypeBuilder<PerformanceMetric> builder)
    {
        builder.ToTable("PerformanceMetrics");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.MeasuredAt)
            .HasDefaultValueSql("NOW()");

        builder.Property(p => p.MetricType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.EndpointOrPage)
            .HasMaxLength(500);

        builder.Property(p => p.ResponseTime).HasDefaultValue(0).HasPrecision(18, 2);
        builder.Property(p => p.MinResponseTime).HasDefaultValue(0).HasPrecision(18, 2);
        builder.Property(p => p.MaxResponseTime).HasDefaultValue(0).HasPrecision(18, 2);
        builder.Property(p => p.AvgResponseTime).HasDefaultValue(0).HasPrecision(18, 2);
        builder.Property(p => p.TotalRequests).HasDefaultValue(0);
        builder.Property(p => p.SuccessfulRequests).HasDefaultValue(0);
        builder.Property(p => p.FailedRequests).HasDefaultValue(0);

        builder.Property(p => p.AdditionalDataJson)
            .HasColumnType("jsonb");

        // Indexes
        builder.HasIndex(p => p.WebsiteId);
        builder.HasIndex(p => p.MetricType);
        builder.HasIndex(p => p.MeasuredAt);
        builder.HasIndex(p => new { p.WebsiteId, p.MetricType, p.MeasuredAt });

        // Relationships
        builder.HasOne(p => p.Website)
            .WithMany()
            .HasForeignKey(p => p.WebsiteId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
