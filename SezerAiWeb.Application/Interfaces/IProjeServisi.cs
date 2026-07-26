using SezerAiWeb.Application.DTOs;

namespace SezerAiWeb.Application.Interfaces;

public interface IProjeServisi
{
    Task<List<ProjeKartDto>> TumProjeleriGetirAsync();
    Task<ProjeDetayDto?> SlugIleGetirAsync(string slug);
    Task<Guid?> IdSlugIleGetirAsync(string slug);
}
