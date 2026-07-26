using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Application.Repositories;

public interface IWebsiteRepository : IRepository<Website>
{
    Task<Website?> GetByDomainAsync(string domain, CancellationToken cancellationToken = default);
    Task<IEnumerable<Website>> GetActiveWebsitesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Website>> GetWebsitesWithMenusAsync(CancellationToken cancellationToken = default);
}
