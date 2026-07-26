using SezerAiWeb.Application.DTOs;
using SezerAiWeb.Application.Interfaces;

namespace SezerAiWeb.Application.Services;

public class MenuService : IMenuService
{
    public Task<IEnumerable<MenuDto>> GetAllByWebsiteIdAsync(Guid websiteId)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    public Task<MenuDto?> GetByIdAsync(Guid id)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MenuDto>> GetHierarchicalMenuAsync(Guid websiteId)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    public Task<MenuDto> CreateAsync(MenuCreateDto dto)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    public Task<MenuDto> UpdateAsync(MenuUpdateDto dto)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }

    public Task<bool> ReorderAsync(Guid id, int newOrder)
    {
        // TODO: Implement
        throw new NotImplementedException();
    }
}
