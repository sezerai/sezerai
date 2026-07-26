using AutoMapper;
using SezerAiWeb.Application.DTOs;
using SezerAiWeb.Application.Interfaces;
using SezerAiWeb.Application.Repositories;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Application.Services;

public class WebsiteService : IWebsiteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WebsiteService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WebsiteDto>> GetAllAsync()
    {
        var websites = await _unitOfWork.Websites.GetAllAsync();
        return _mapper.Map<IEnumerable<WebsiteDto>>(websites);
    }

    public async Task<WebsiteDto?> GetByIdAsync(Guid id)
    {
        var website = await _unitOfWork.Websites.GetByIdAsync(id);
        return website == null ? null : _mapper.Map<WebsiteDto>(website);
    }

    public async Task<WebsiteDto?> GetByDomainAsync(string domain)
    {
        var website = await _unitOfWork.Websites.GetByDomainAsync(domain);
        return website == null ? null : _mapper.Map<WebsiteDto>(website);
    }

    public async Task<WebsiteDto> CreateAsync(WebsiteCreateDto dto)
    {
        var website = _mapper.Map<Website>(dto);
        website.Id = Guid.NewGuid();
        website.CreatedAt = DateTime.UtcNow;

        await _unitOfWork.Websites.AddAsync(website);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<WebsiteDto>(website);
    }

    public async Task<WebsiteDto> UpdateAsync(WebsiteUpdateDto dto)
    {
        var existingWebsite = await _unitOfWork.Websites.GetByIdAsync(dto.Id);
        if (existingWebsite == null)
            throw new KeyNotFoundException($"Website with ID {dto.Id} not found.");

        _mapper.Map(dto, existingWebsite);
        existingWebsite.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Websites.UpdateAsync(existingWebsite);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<WebsiteDto>(existingWebsite);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var website = await _unitOfWork.Websites.GetByIdAsync(id);
        if (website == null)
            return false;

        await _unitOfWork.Websites.DeleteAsync(website);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ToggleActiveStatusAsync(Guid id)
    {
        var website = await _unitOfWork.Websites.GetByIdAsync(id);
        if (website == null)
            return false;

        website.IsActive = !website.IsActive;
        website.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Websites.UpdateAsync(website);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
