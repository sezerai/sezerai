using Markdig;
using Microsoft.Extensions.Options;
using SezerAiWeb.Application.DTOs;
using SezerAiWeb.Application.Interfaces;

namespace SezerAiWeb.Infrastructure.Blog;

public class BlogServisi(IOptions<BlogAyarlari> ayarlar) : IBlogServisi
{
    private static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .Build();

    public Task<List<BlogKartDto>> TumYazilariGetirAsync()
    {
        var yazilar = TumYazilariOku()
            .OrderByDescending(y => y.YayinTarihi)
            .Select(y => new BlogKartDto
            {
                Baslik = y.Baslik,
                Slug = y.Slug,
                Ozet = y.Ozet,
                KapakGorseli = y.KapakGorseli,
                Yazar = y.Yazar,
                YayinTarihi = y.YayinTarihi,
            })
            .ToList();

        return Task.FromResult(yazilar);
    }

    public Task<BlogDetayDto?> SlugIleGetirAsync(string slug)
    {
        var yazi = TumYazilariOku().FirstOrDefault(y => y.Slug == slug);
        if (yazi is null)
        {
            return Task.FromResult<BlogDetayDto?>(null);
        }

        return Task.FromResult<BlogDetayDto?>(new BlogDetayDto
        {
            Baslik = yazi.Baslik,
            Slug = yazi.Slug,
            Ozet = yazi.Ozet,
            IcerikHtml = yazi.IcerikHtml,
            KapakGorseli = yazi.KapakGorseli,
            Yazar = yazi.Yazar,
            YayinTarihi = yazi.YayinTarihi,
            MetaBaslik = yazi.MetaBaslik,
            MetaAciklama = yazi.MetaAciklama,
            MetaAnahtarKelimeler = yazi.MetaAnahtarKelimeler,
        });
    }

    private List<Domain.Entities.BlogYazisi> TumYazilariOku()
    {
        var klasor = ayarlar.Value.IcerikKlasoru;
        var sonuc = new List<Domain.Entities.BlogYazisi>();

        if (!Directory.Exists(klasor))
        {
            return sonuc;
        }

        foreach (var dosyaYolu in Directory.GetFiles(klasor, "*.md"))
        {
            var icerik = File.ReadAllText(dosyaYolu);
            var yazi = DosyayiAyristir(icerik);
            if (yazi is not null)
            {
                sonuc.Add(yazi);
            }
        }

        return sonuc;
    }

    private static Domain.Entities.BlogYazisi? DosyayiAyristir(string icerik)
    {
        const string ayirac = "---";
        if (!icerik.TrimStart().StartsWith(ayirac))
        {
            return null;
        }

        var parcalar = icerik.TrimStart().Split(ayirac, 3, StringSplitOptions.None);
        if (parcalar.Length < 3)
        {
            return null;
        }

        var frontMatter = parcalar[1];
        var govde = parcalar[2].Trim();

        var alanlar = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var satir in frontMatter.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            var ayrilmisSatir = satir.Trim();
            var ikiNoktaIndex = ayrilmisSatir.IndexOf(':');
            if (ikiNoktaIndex <= 0)
            {
                continue;
            }

            var anahtar = ayrilmisSatir[..ikiNoktaIndex].Trim();
            var deger = ayrilmisSatir[(ikiNoktaIndex + 1)..].Trim().Trim('"');
            alanlar[anahtar] = deger;
        }

        var tarih = alanlar.TryGetValue("tarih", out var tarihStr) && DateTime.TryParse(tarihStr, out var ayristirilanTarih)
            ? ayristirilanTarih
            : DateTime.UtcNow;

        return new Domain.Entities.BlogYazisi
        {
            Baslik = alanlar.GetValueOrDefault("baslik", "Başlıksız"),
            Slug = alanlar.GetValueOrDefault("slug", ""),
            Ozet = alanlar.GetValueOrDefault("ozet", ""),
            KapakGorseli = alanlar.GetValueOrDefault("kapak", "/images/logo-sezerai.png"),
            Yazar = alanlar.GetValueOrDefault("yazar", "SEZER AI Technology"),
            YayinTarihi = tarih,
            MetaBaslik = alanlar.GetValueOrDefault("meta_baslik", alanlar.GetValueOrDefault("baslik", "")),
            MetaAciklama = alanlar.GetValueOrDefault("meta_aciklama", alanlar.GetValueOrDefault("ozet", "")),
            MetaAnahtarKelimeler = alanlar.GetValueOrDefault("meta_anahtar", ""),
            IcerikHtml = Markdown.ToHtml(govde, Pipeline),
        };
    }
}
