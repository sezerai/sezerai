using Microsoft.AspNetCore.Mvc;
using SezerAiWeb.Application.DTOs;
using SezerAiWeb.Application.Interfaces;

namespace SezerAiWeb.Web.ViewComponents;

public class BlogMenuViewComponent(IBlogServisi blogServisi) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(bool mobil = false)
    {
        var yazilar = (await blogServisi.TumYazilariGetirAsync()).Take(3).ToList();
        return View(new BlogMenuModel(yazilar, mobil));
    }
}

public record BlogMenuModel(List<BlogKartDto> Yazilar, bool Mobil);
