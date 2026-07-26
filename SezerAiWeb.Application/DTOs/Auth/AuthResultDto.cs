using SezerAiWeb.Application.DTOs;

namespace SezerAiWeb.Application.DTOs.Auth;

public class AuthResultDto
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public UserDto? User { get; set; }
    public string? Token { get; set; } // For future JWT implementation
}
