using Microsoft.EntityFrameworkCore;
using SezerAiWeb.Domain.Entities;
using SezerAiWeb.Persistence.Context;
using SezerAiWeb.Application.Repositories;

namespace SezerAiWeb.Persistence.Repositories;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
    }
}
