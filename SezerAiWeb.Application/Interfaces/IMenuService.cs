using SezerAiWeb.Application.DTOs;

namespace SezerAiWeb.Application.Interfaces;

public interface IMenuService
{
    Task<IEnumerable<MenuDto>> GetAllByWebsiteIdAsync(Guid websiteId);
    Task<MenuDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<MenuDto>> GetHierarchicalMenuAsync(Guid websiteId);
    Task<MenuDto> CreateAsync(MenuCreateDto dto);
    Task<MenuDto> UpdateAsync(MenuUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ReorderAsync(Guid id, int newOrder);
}
