using System.ComponentModel.DataAnnotations;

namespace SezerAiWeb.Application.DTOs;

public class IletisimFormDto
{
    [Required(ErrorMessage = "Ad soyad zorunludur.")]
    [MaxLength(150)]
    public string AdSoyad { get; set; } = null!;

    [Required(ErrorMessage = "E-posta zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
    [MaxLength(200)]
    public string Email { get; set; } = null!;

    [MaxLength(30)]
    public string? Telefon { get; set; }

    [Required(ErrorMessage = "Konu zorunludur.")]
    [MaxLength(200)]
    public string Konu { get; set; } = null!;

    [Required(ErrorMessage = "Mesaj zorunludur.")]
    [MaxLength(4000)]
    public string Mesaj { get; set; } = null!;
}
