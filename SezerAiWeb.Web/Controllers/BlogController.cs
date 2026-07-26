using Microsoft.AspNetCore.Mvc;
using SezerAiWeb.Application.Interfaces;

namespace SezerAiWeb.Web.Controllers;

[Route("blog")]
public class BlogController(IBlogServisi blogServisi, ISeoService seoService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        ViewData["Seo"] = seoService.GetBlogIndexSeo();
        var yazilar = await blogServisi.TumYazilariGetirAsync();
        return View(yazilar);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> Detay(string slug)
    {
        var yazi = await blogServisi.SlugIleGetirAsync(slug);
        if (yazi is null) return NotFound();

        ViewData["Seo"] = seoService.GetBlogDetaySeo(yazi);
        return View(yazi);
    }
}
