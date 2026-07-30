using SezerAiWeb.Application.DTOs;

namespace SezerAiWeb.Web.Models;

public class AnaSayfaViewModel
{
    public List<ProjeKartDto> Projeler { get; set; } = [];
    public List<BlogKartDto> SonBlogYazilari { get; set; } = [];
}
