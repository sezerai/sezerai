using System.Text.Json;
using Microsoft.Extensions.Options;
using SezerAiWeb.Application.DTOs;
using SezerAiWeb.Application.Interfaces;
using SezerAiWeb.Persistence.Extensions;

namespace SezerAiWeb.Persistence.Servisler;

public class DemoTalebiServisi(IOptions<DosyaAyarlari> ayarlar) : IDemoTalebiServisi
{
    private static readonly object YazmaKilidi = new();

    public Task GonderAsync(DemoTalebiFormDto dto)
    {
        var kayit = new
        {
            Id = Guid.NewGuid(),
            dto.ProjeId,
            dto.AdSoyad,
            dto.Email,
            dto.Telefon,
            dto.KurumAdi,
            dto.Mesaj,
            OlusturmaTarihi = DateTime.UtcNow,
        };

        var yol = Path.Combine(ayarlar.Value.AppDataKlasoru, "demo-talepleri.jsonl");
        Directory.CreateDirectory(ayarlar.Value.AppDataKlasoru);

        var satir = JsonSerializer.Serialize(kayit) + Environment.NewLine;
        lock (YazmaKilidi)
        {
            File.AppendAllText(yol, satir);
        }

        return Task.CompletedTask;
    }
}
