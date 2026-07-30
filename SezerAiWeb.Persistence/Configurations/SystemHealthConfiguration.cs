using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Persistence.Configurations;

public class SystemHealthConfiguration : IEntityTypeConfiguration<SystemHealth>
{
    public void Configure(EntityTypeBuilder<SystemHealth> builder)
    {
        builder.ToTable("SystemHealths");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.CheckedAt)
            .HasDefaultValueSql("NOW()");

        builder.Property(s => s.Status)
            .HasMaxLength(20)
            .HasDefaultValue("Healthy");

        builder.Property(s => s.CpuUsage).HasDefaultValue(0).HasPrecision(18, 2);
        builder.Property(s => s.MemoryUsage).HasDefaultValue(0).HasPrecision(18, 2);
        builder.Property(s => s.DiskUsage).HasDefaultValue(0).HasPrecision(18, 2);
        builder.Property(s => s.IsDatabaseOnline).HasDefaultValue(true);
        builder.Property(s => s.DatabaseResponseTime).HasDefaultValue(0).HasPrecision(18, 2);
        builder.Property(s => s.IsCacheOnline).HasDefaultValue(true);
        builder.Property(s => s.CacheResponseTime).HasDefaultValue(0).HasPrecision(18, 2);
        builder.Property(s => s.IsEmailServiceOnline).HasDefaultValue(true);
        builder.Property(s => s.IsStorageServiceOnline).HasDefaultValue(true);
        builder.Property(s => s.IsGoogleServicesOnline).HasDefaultValue(true);
        builder.Property(s => s.ActiveUsers).HasDefaultValue(0);
        builder.Property(s => s.QueuedJobs).HasDefaultValue(0);
        builder.Property(s => s.FailedJobs).HasDefaultValue(0);

        builder.Property(s => s.AdditionalDataJson)
            .HasColumnType("jsonb");

        // Indexes
        builder.HasIndex(s => s.CheckedAt);
        builder.HasIndex(s => s.Status);
    }
}
