using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Persistence.Configurations;

public class WebsiteMenuConfiguration : IEntityTypeConfiguration<WebsiteMenu>
{
    public void Configure(EntityTypeBuilder<WebsiteMenu> builder)
    {
        builder.ToTable("WebsiteMenus");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Url)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(m => m.Icon)
            .HasMaxLength(100);

        builder.Property(m => m.Order)
            .HasDefaultValue(0);

        builder.Property(m => m.IsActive)
            .HasDefaultValue(true);

        builder.Property(m => m.OpenInNewTab)
            .HasDefaultValue(false);

        builder.Property(m => m.RequiresAuth)
            .HasDefaultValue(false);

        builder.Property(m => m.AllowedRoles)
            .HasMaxLength(500);

        builder.Property(m => m.CssClass)
            .HasMaxLength(100);

        builder.Property(m => m.Target)
            .HasMaxLength(20);

        // Indexes
        builder.HasIndex(m => new { m.WebsiteId, m.Order });
        builder.HasIndex(m => m.ParentId);

        // Self-referencing relationship
        builder.HasOne(m => m.Parent)
            .WithMany(m => m.Children)
            .HasForeignKey(m => m.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Website relationship
        builder.HasOne(m => m.Website)
            .WithMany(w => w.Menus)
            .HasForeignKey(m => m.WebsiteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
