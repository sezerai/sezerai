using Microsoft.EntityFrameworkCore;
using SezerAiWeb.Domain.Entities;
using SezerAiWeb.Persistence.Context;
using SezerAiWeb.Application.Repositories;

namespace SezerAiWeb.Persistence.Repositories;

public class WebsiteRepository : Repository<Website>, IWebsiteRepository
{
    public WebsiteRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Website?> GetByDomainAsync(string domain, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(w => w.Domain == domain, cancellationToken);
    }

    public async Task<IEnumerable<Website>> GetActiveWebsitesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(w => w.IsActive)
            .OrderBy(w => w.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Website>> GetWebsitesWithMenusAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(w => w.Menus)
            .OrderBy(w => w.Name)
            .ToListAsync(cancellationToken);
    }
}
