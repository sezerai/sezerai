using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Persistence.Configurations;

public class WebsiteConfiguration : IEntityTypeConfiguration<Website>
{
    public void Configure(EntityTypeBuilder<Website> builder)
    {
        builder.ToTable("Websites");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(w => w.Domain)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(w => w.Description)
            .HasMaxLength(1000);

        builder.Property(w => w.Language)
            .HasMaxLength(10)
            .HasDefaultValue("tr-TR");

        builder.Property(w => w.Currency)
            .HasMaxLength(3)
            .HasDefaultValue("TRY");

        builder.Property(w => w.TimeZone)
            .HasMaxLength(50)
            .HasDefaultValue("Europe/Istanbul");

        builder.Property(w => w.IsActive)
            .HasDefaultValue(true);

        // Indexes
        builder.HasIndex(w => w.Domain).IsUnique();
        builder.HasIndex(w => w.IsActive);

        // Relationships
        builder.HasMany(w => w.Menus)
            .WithOne(m => m.Website)
            .HasForeignKey(m => m.WebsiteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(w => w.Metrics)
            .WithOne(m => m.Website)
            .HasForeignKey(m => m.WebsiteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(w => w.SeoReports)
            .WithOne(s => s.Website)
            .HasForeignKey(s => s.WebsiteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
