using System.Text;
using Microsoft.AspNetCore.Mvc;
using SezerAiWeb.Application.Interfaces;

namespace SezerAiWeb.Web.Controllers;

public class SeoController(IProjeServisi projeServisi, IBlogServisi blogServisi) : Controller
{
    private const string SiteUrl = "https://www.sezerai.tr";

    [HttpGet("/sitemap.xml")]
    public async Task<IActionResult> Sitemap()
    {
        var yollar = new List<(string Yol, string Degisim, string Oncelik)>
        {
            ("/", "weekly", "1.0"),
            ("/hakkimizda", "monthly", "0.8"),
            ("/projelerimiz", "weekly", "0.9"),
            ("/blog", "weekly", "0.7"),
            ("/iletisim", "monthly", "0.5"),
        };

        var projeler = await projeServisi.TumProjeleriGetirAsync();
        yollar.AddRange(projeler.Select(p => ($"/projelerimiz/{p.Slug}", "monthly", "0.8")));

        var yazilar = await blogServisi.TumYazilariGetirAsync();
        yollar.AddRange(yazilar.Select(y => ($"/blog/{y.Slug}", "monthly", "0.6")));

        var sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
        foreach (var (yol, degisim, oncelik) in yollar)
        {
            sb.AppendLine("  <url>");
            sb.AppendLine($"    <loc>{SiteUrl}{yol}</loc>");
            sb.AppendLine($"    <changefreq>{degisim}</changefreq>");
            sb.AppendLine($"    <priority>{oncelik}</priority>");
            sb.AppendLine("  </url>");
        }
        sb.AppendLine("</urlset>");

        return Content(sb.ToString(), "application/xml", Encoding.UTF8);
    }

    [HttpGet("/robots.txt")]
    public IActionResult Robots()
    {
        var icerik = $"""
            User-agent: *
            Allow: /

            Sitemap: {SiteUrl}/sitemap.xml
            """;

        return Content(icerik, "text/plain", Encoding.UTF8);
    }
}
