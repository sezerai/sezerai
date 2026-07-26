using System.ComponentModel.DataAnnotations;

namespace SezerAiWeb.Application.DTOs;

public class DemoTalebiFormDto
{
    [Required]
    public Guid ProjeId { get; set; }

    [Required(ErrorMessage = "Ad soyad zorunludur.")]
    [MaxLength(150)]
    public string AdSoyad { get; set; } = null!;

    [Required(ErrorMessage = "E-posta zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
    [MaxLength(200)]
    public string Email { get; set; } = null!;

    [MaxLength(30)]
    public string? Telefon { get; set; }

    [MaxLength(200)]
    public string? KurumAdi { get; set; }

    [MaxLength(2000)]
    public string? Mesaj { get; set; }
}
