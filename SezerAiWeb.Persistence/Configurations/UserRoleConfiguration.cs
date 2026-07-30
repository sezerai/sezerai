using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Persistence.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(ur => ur.Id);

        builder.Property(ur => ur.AssignedAt)
            .HasDefaultValueSql("NOW()");

        // Composite index
        builder.HasIndex(ur => new { ur.UserId, ur.RoleId }).IsUnique();

        // Relationships configured in User and Role configurations
    }
}
