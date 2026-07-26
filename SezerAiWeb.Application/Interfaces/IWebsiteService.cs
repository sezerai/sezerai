using SezerAiWeb.Application.DTOs;

namespace SezerAiWeb.Application.Interfaces;

public interface IWebsiteService
{
    Task<IEnumerable<WebsiteDto>> GetAllAsync();
    Task<WebsiteDto?> GetByIdAsync(Guid id);
    Task<WebsiteDto?> GetByDomainAsync(string domain);
    Task<WebsiteDto> CreateAsync(WebsiteCreateDto dto);
    Task<WebsiteDto> UpdateAsync(WebsiteUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ToggleActiveStatusAsync(Guid id);
}
