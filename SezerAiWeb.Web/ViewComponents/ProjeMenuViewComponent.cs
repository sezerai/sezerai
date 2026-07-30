using Microsoft.AspNetCore.Mvc;
using SezerAiWeb.Application.DTOs;
using SezerAiWeb.Application.Interfaces;

namespace SezerAiWeb.Web.ViewComponents;

public class ProjeMenuViewComponent(IProjeServisi projeServisi) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(bool mobil = false)
    {
        var projeler = await projeServisi.TumProjeleriGetirAsync();
        return View(new ProjeMenuModel(projeler, mobil));
    }
}

public record ProjeMenuModel(List<ProjeKartDto> Projeler, bool Mobil);
