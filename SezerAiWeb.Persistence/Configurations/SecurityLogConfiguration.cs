using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Persistence.Configurations;

public class SecurityLogConfiguration : IEntityTypeConfiguration<SecurityLog>
{
    public void Configure(EntityTypeBuilder<SecurityLog> builder)
    {
        builder.ToTable("SecurityLogs");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.EventType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.IpAddress)
            .HasMaxLength(45); // IPv6 max length

        builder.Property(s => s.UserAgent)
            .HasMaxLength(500);

        builder.Property(s => s.Location)
            .HasMaxLength(200);

        builder.Property(s => s.IsSuccess)
            .HasDefaultValue(false);

        builder.Property(s => s.FailureReason)
            .HasMaxLength(500);

        builder.Property(s => s.AdditionalData)
            .HasColumnType("jsonb");

        builder.Property(s => s.Severity)
            .HasMaxLength(20)
            .HasDefaultValue("Info");

        // Indexes
        builder.HasIndex(s => s.UserId);
        builder.HasIndex(s => s.EventType);
        builder.HasIndex(s => s.CreatedAt);
        builder.HasIndex(s => s.IsSuccess);
        builder.HasIndex(s => s.Severity);

        // Relationships
        builder.HasOne(s => s.User)
            .WithMany(u => u.SecurityLogs)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
