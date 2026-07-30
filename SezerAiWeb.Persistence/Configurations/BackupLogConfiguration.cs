using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Persistence.Configurations;

public class BackupLogConfiguration : IEntityTypeConfiguration<BackupLog>
{
    public void Configure(EntityTypeBuilder<BackupLog> builder)
    {
        builder.ToTable("BackupLogs");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.BackupType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(b => b.BackupStartedAt);

        builder.Property(b => b.IsSuccess)
            .HasDefaultValue(false);

        builder.Property(b => b.ErrorMessage)
            .HasMaxLength(2000);

        builder.Property(b => b.BackupLocation)
            .HasMaxLength(1000);

        builder.Property(b => b.BackupFileName)
            .HasMaxLength(500);

        builder.Property(b => b.BackupMethod)
            .HasMaxLength(50)
            .HasDefaultValue("Automatic");

        builder.Property(b => b.CanRestore)
            .HasDefaultValue(true);

        builder.Property(b => b.ChecksumHash)
            .HasMaxLength(128);

        builder.Property(b => b.AdditionalDataJson)
            .HasColumnType("jsonb");

        // Indexes
        builder.HasIndex(b => b.BackupType);
        builder.HasIndex(b => b.BackupStartedAt);
        builder.HasIndex(b => b.IsSuccess);
        builder.HasIndex(b => b.CanRestore);
    }
}
