using Microsoft.EntityFrameworkCore;
using SezerAiWeb.Domain.Entities;
using SezerAiWeb.Domain.Common;

namespace SezerAiWeb.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Entity DbSets
    public DbSet<Website> Websites { get; set; } = null!;
    public DbSet<WebsiteMenu> WebsiteMenus { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    public DbSet<SiteMetrics> SiteMetrics { get; set; } = null!;
    public DbSet<GoogleServiceLog> GoogleServiceLogs { get; set; } = null!;
    public DbSet<SecurityLog> SecurityLogs { get; set; } = null!;
    public DbSet<AIAgentLog> AIAgentLogs { get; set; } = null!;
    public DbSet<AlertNotification> AlertNotifications { get; set; } = null!;
    public DbSet<SeoReport> SeoReports { get; set; } = null!;
    public DbSet<SystemHealth> SystemHealths { get; set; } = null!;
    public DbSet<PerformanceMetric> PerformanceMetrics { get; set; } = null!;
    public DbSet<BackupLog> BackupLogs { get; set; } = null!;
    public DbSet<BlogYazisi> BlogYazilari { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the Configurations folder
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Global query filter for soft delete
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "e");
                var property = System.Linq.Expressions.Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                var falseConstant = System.Linq.Expressions.Expression.Constant(false);
                var body = System.Linq.Expressions.Expression.Equal(property, falseConstant);
                var lambda = System.Linq.Expressions.Expression.Lambda(body, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Update UpdatedAt timestamp for modified entities
        var entries = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            entry.Entity.UpdatedAt = DateTime.UtcNow;
        }

        // Set CreatedAt for new entities
        var newEntries = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Added);

        foreach (var entry in newEntries)
        {
            if (entry.Entity.CreatedAt == default)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
