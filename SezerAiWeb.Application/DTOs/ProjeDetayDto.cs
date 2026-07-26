namespace SezerAiWeb.Application.DTOs;

public class ProjeDetayDto
{
    public string Ad { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string KisaAciklama { get; set; } = null!;
    public string Aciklama { get; set; } = null!;
    public string KullanimSenaryolari { get; set; } = null!;
    public string BannerGorseli { get; set; } = null!;
    public string MetaBaslik { get; set; } = null!;
    public string MetaAciklama { get; set; } = null!;
    public string MetaAnahtarKelimeler { get; set; } = null!;
    public List<ProjeOzellikDto> Ozellikler { get; set; } = [];
}
