using SezerAiWeb.Application.DTOs;

namespace SezerAiWeb.Application.Interfaces;

public interface IBlogServisi
{
    Task<List<BlogKartDto>> TumYazilariGetirAsync();
    Task<BlogDetayDto?> SlugIleGetirAsync(string slug);
}
