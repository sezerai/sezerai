using Microsoft.EntityFrameworkCore;
using SezerAiWeb.Domain.Entities;
using SezerAiWeb.Persistence.Context;
using SezerAiWeb.Application.Repositories;

namespace SezerAiWeb.Persistence.Repositories;

public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserRole>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(ur => ur.Role)
            .Where(ur => ur.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}
