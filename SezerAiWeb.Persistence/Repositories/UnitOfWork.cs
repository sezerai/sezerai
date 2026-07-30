using Microsoft.EntityFrameworkCore.Storage;
using SezerAiWeb.Domain.Entities;
using SezerAiWeb.Persistence.Context;
using SezerAiWeb.Application.Repositories;

namespace SezerAiWeb.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    // Lazy-loaded repositories
    private IWebsiteRepository? _websites;
    private IUserRepository? _users;
    private IRoleRepository? _roles;
    private IUserRoleRepository? _userRoles;
    private IRepository<SiteMetrics>? _siteMetrics;
    private IRepository<AlertNotification>? _alertNotifications;
    private IRepository<AIAgentLog>? _aiAgentLogs;
    private IRepository<SecurityLog>? _securityLogs;
    private IRepository<SystemHealth>? _systemHealths;
    private IRepository<BlogYazisi>? _blogYazilari;
    private IRepository<WebsiteMenu>? _websiteMenus;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Repository Properties (Lazy Loading)
    public IWebsiteRepository Websites => _websites ??= new WebsiteRepository(_context);
    public IUserRepository Users => _users ??= new UserRepository(_context);
    public IRoleRepository Roles => _roles ??= new RoleRepository(_context);
    public IUserRoleRepository UserRoles => _userRoles ??= new UserRoleRepository(_context);
    public IRepository<SiteMetrics> SiteMetrics => _siteMetrics ??= new Repository<SiteMetrics>(_context);
    public IRepository<AlertNotification> AlertNotifications => _alertNotifications ??= new Repository<AlertNotification>(_context);
    public IRepository<AIAgentLog> AIAgentLogs => _aiAgentLogs ??= new Repository<AIAgentLog>(_context);
    public IRepository<SecurityLog> SecurityLogs => _securityLogs ??= new Repository<SecurityLog>(_context);
    public IRepository<SystemHealth> SystemHealths => _systemHealths ??= new Repository<SystemHealth>(_context);
    public IRepository<BlogYazisi> BlogYazilari => _blogYazilari ??= new Repository<BlogYazisi>(_context);
    public IRepository<WebsiteMenu> WebsiteMenus => _websiteMenus ??= new Repository<WebsiteMenu>(_context);

    // Transaction Management
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            if (_transaction != null)
            {
                await _transaction.CommitAsync(cancellationToken);
            }
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    // Dispose
    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
