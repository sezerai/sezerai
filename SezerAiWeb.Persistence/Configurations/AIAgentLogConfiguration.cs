using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Persistence.Configurations;

public class AIAgentLogConfiguration : IEntityTypeConfiguration<AIAgentLog>
{
    public void Configure(EntityTypeBuilder<AIAgentLog> builder)
    {
        builder.ToTable("AIAgentLogs");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.AgentName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.TaskType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.InputData)
            .HasColumnType("jsonb");

        builder.Property(a => a.OutputData)
            .HasColumnType("jsonb");

        builder.Property(a => a.IsSuccess)
            .HasDefaultValue(false);

        builder.Property(a => a.ErrorMessage)
            .HasMaxLength(2000);

        builder.Property(a => a.Cost)
            .HasPrecision(18, 4);

        // Indexes
        builder.HasIndex(a => a.AgentName);
        builder.HasIndex(a => a.TaskType);
        builder.HasIndex(a => a.IsSuccess);
        builder.HasIndex(a => a.CreatedAt);
        builder.HasIndex(a => a.WebsiteId);

        // Relationships
        builder.HasOne(a => a.Website)
            .WithMany()
            .HasForeignKey(a => a.WebsiteId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
