using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Persistence.Configurations;

public class AlertNotificationConfiguration : IEntityTypeConfiguration<AlertNotification>
{
    public void Configure(EntityTypeBuilder<AlertNotification> builder)
    {
        builder.ToTable("AlertNotifications");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Message)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(a => a.Type)
            .HasMaxLength(20)
            .HasDefaultValue("Info");

        builder.Property(a => a.Category)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.IsRead)
            .HasDefaultValue(false);

        builder.Property(a => a.Priority)
            .HasMaxLength(20)
            .HasDefaultValue("Normal");

        builder.Property(a => a.ActionUrl)
            .HasMaxLength(500);

        builder.Property(a => a.ActionText)
            .HasMaxLength(100);

        // Indexes
        builder.HasIndex(a => a.UserId);
        builder.HasIndex(a => a.WebsiteId);
        builder.HasIndex(a => a.IsRead);
        builder.HasIndex(a => a.Category);
        builder.HasIndex(a => a.Priority);
        builder.HasIndex(a => a.CreatedAt);

        // Relationships
        builder.HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Website)
            .WithMany()
            .HasForeignKey(a => a.WebsiteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
