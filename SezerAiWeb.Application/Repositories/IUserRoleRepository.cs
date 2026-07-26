using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Application.Repositories;

public interface IUserRoleRepository : IRepository<UserRole>
{
    Task<IEnumerable<UserRole>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
