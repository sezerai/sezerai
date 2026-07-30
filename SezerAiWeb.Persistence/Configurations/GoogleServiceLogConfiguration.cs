using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Persistence.Configurations;

public class GoogleServiceLogConfiguration : IEntityTypeConfiguration<GoogleServiceLog>
{
    public void Configure(EntityTypeBuilder<GoogleServiceLog> builder)
    {
        builder.ToTable("GoogleServiceLogs");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.ServiceName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(g => g.ActionType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(g => g.RequestData)
            .HasColumnType("jsonb");

        builder.Property(g => g.ResponseData)
            .HasColumnType("jsonb");

        builder.Property(g => g.IsSuccess)
            .HasDefaultValue(false);

        builder.Property(g => g.ErrorMessage)
            .HasMaxLength(2000);

        // Indexes
        builder.HasIndex(g => g.ServiceName);
        builder.HasIndex(g => g.ActionType);
        builder.HasIndex(g => g.IsSuccess);
        builder.HasIndex(g => g.CreatedAt);
        builder.HasIndex(g => g.WebsiteId);

        // Relationships
        builder.HasOne(g => g.Website)
            .WithMany()
            .HasForeignKey(g => g.WebsiteId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
