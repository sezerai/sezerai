using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using SezerAiWeb.Application.DTOs;
using SezerAiWeb.Application.Interfaces;
using SezerAiWeb.Persistence.Extensions;

namespace SezerAiWeb.Persistence.Servisler;

public class ProjeServisi(IOptions<DosyaAyarlari> ayarlar) : IProjeServisi
{
    private static readonly JsonSerializerOptions JsonAyarlari = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public Task<List<ProjeKartDto>> TumProjeleriGetirAsync()
    {
        var projeler = ProjeleriOku()
            .OrderBy(p => p.SiraNo)
            .Select(p => new ProjeKartDto
            {
                Ad = p.Ad,
                Slug = p.Slug,
                KisaAciklama = p.KisaAciklama,
                KapakGorseli = p.KapakGorseli,
            })
            .ToList();

        return Task.FromResult(projeler);
    }

    public Task<ProjeDetayDto?> SlugIleGetirAsync(string slug)
    {
        var proje = ProjeleriOku().FirstOrDefault(p => p.Slug == slug);
        if (proje is null)
        {
            return Task.FromResult<ProjeDetayDto?>(null);
        }

        return Task.FromResult<ProjeDetayDto?>(new ProjeDetayDto
        {
            Ad = proje.Ad,
            Slug = proje.Slug,
            KisaAciklama = proje.KisaAciklama,
            Aciklama = proje.Aciklama,
            KullanimSenaryolari = proje.KullanimSenaryolari,
            BannerGorseli = proje.BannerGorseli,
            MetaBaslik = proje.MetaBaslik,
            MetaAciklama = proje.MetaAciklama,
            MetaAnahtarKelimeler = proje.MetaAnahtarKelimeler,
            Ozellikler = proje.Ozellikler
                .Select(o => new ProjeOzellikDto { Baslik = o.Baslik, Aciklama = o.Aciklama })
                .ToList(),
        });
    }

    public Task<Guid?> IdSlugIleGetirAsync(string slug)
    {
        var proje = ProjeleriOku().FirstOrDefault(p => p.Slug == slug);
        return Task.FromResult(proje is null ? null : (Guid?)DeterministikId(proje.Slug));
    }

    private List<ProjeJsonKaydi> ProjeleriOku()
    {
        var yol = ayarlar.Value.ProjelerJsonYolu;
        if (!File.Exists(yol))
        {
            return [];
        }

        var icerik = File.ReadAllText(yol);
        return JsonSerializer.Deserialize<List<ProjeJsonKaydi>>(icerik, JsonAyarlari) ?? [];
    }

    // Slug sabit oldugu icin proje kimligi slug'dan turetilen deterministik bir Guid'dir;
    // dosya tabanli depoda ayrica bir kimlik alani tutmaya gerek birakmaz.
    internal static Guid DeterministikId(string slug)
    {
        var bytes = System.Security.Cryptography.MD5.HashData(System.Text.Encoding.UTF8.GetBytes(slug));
        return new Guid(bytes);
    }

    private class ProjeJsonKaydi
    {
        public string Ad { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public int SiraNo { get; set; }
        public string KisaAciklama { get; set; } = null!;
        public string Aciklama { get; set; } = null!;
        public string KullanimSenaryolari { get; set; } = null!;
        public string KapakGorseli { get; set; } = null!;
        public string BannerGorseli { get; set; } = null!;
        public string MetaBaslik { get; set; } = null!;
        public string MetaAciklama { get; set; } = null!;
        public string MetaAnahtarKelimeler { get; set; } = null!;
        public List<ProjeOzellikJsonKaydi> Ozellikler { get; set; } = [];
    }

    private class ProjeOzellikJsonKaydi
    {
        public string Baslik { get; set; } = null!;
        public string Aciklama { get; set; } = null!;
    }
}
