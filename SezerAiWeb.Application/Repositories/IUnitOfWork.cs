namespace SezerAiWeb.Application.Repositories;

/// <summary>
/// Unit of Work pattern for managing transactions
/// </summary>
public interface IUnitOfWork : IDisposable
{
    // Repositories
    IWebsiteRepository Websites { get; }
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    IUserRoleRepository UserRoles { get; }
    IRepository<Domain.Entities.SiteMetrics> SiteMetrics { get; }
    IRepository<Domain.Entities.AlertNotification> AlertNotifications { get; }
    IRepository<Domain.Entities.AIAgentLog> AIAgentLogs { get; }
    IRepository<Domain.Entities.SecurityLog> SecurityLogs { get; }
    IRepository<Domain.Entities.SystemHealth> SystemHealths { get; }
    IRepository<Domain.Entities.BlogYazisi> BlogYazilari { get; }
    IRepository<Domain.Entities.WebsiteMenu> WebsiteMenus { get; }

    // Transaction Management
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
