using SezerAiWeb.Application.DTOs.Auth;

namespace SezerAiWeb.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResultDto> RegisterAsync(RegisterDto dto);
    Task<AuthResultDto> LoginAsync(LoginDto dto);
    Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto);
    Task<bool> EmailExistsAsync(string email);
}
