using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Application.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
