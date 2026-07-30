using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SezerAiWeb.Application.DTOs.Auth;
using SezerAiWeb.Application.Interfaces;
using System.Security.Claims;

namespace SezerAiWeb.Web.Areas.MasterPanel.Controllers
{
    [Area("MasterPanel")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("panel/auth/login")]
        public IActionResult Login() => View();

        [HttpPost("panel/auth/secret-login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SecretLogin(string secretCode)
        {
            if (secretCode == "alt+15alt+28alt+19alt+219alt+28")
            {
                var admin = await _authService.LoginAsync(new LoginDto { Email = "admin@sezerai.tr", Password = "Admin123!" });
                if (admin.Success) { await SignInUserAsync(admin.User!); return RedirectToAction("Index", "Dashboard"); }
            }
            TempData["Error"] = "Invalid access code. Access denied.";
            return RedirectToAction(nameof(Login));
        }

        [HttpPost("panel/auth/login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (!result.Success) { TempData["Error"] = result.Message; return View(dto); }
            await SignInUserAsync(result.User!);
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet("panel/auth/register")]
        public IActionResult Register() => View();

        [HttpPost("panel/auth/register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (!result.Success) { TempData["Error"] = result.Message; return View(dto); }
            TempData["Success"] = "Registration successful! Please login.";
            return RedirectToAction(nameof(Login));
        }

        [HttpGet("panel/auth/forgot-password")]
        public IActionResult ForgotPassword() => View();

        [HttpPost("panel/auth/forgot-password")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            await _authService.ForgotPasswordAsync(dto);
            TempData["Success"] = "If your email exists, you will receive a password reset link.";
            return View();
        }

        [HttpGet("panel/auth/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        [HttpGet("panel/auth/google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action(nameof(GoogleCallback)) };
            return Challenge(properties, "Google");
        }

        [HttpGet("panel/auth/google-callback")]
        public async Task<IActionResult> GoogleCallback()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded) { TempData["Error"] = "Google authentication failed."; return RedirectToAction(nameof(Login)); }

            var email = result.Principal?.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email)) { TempData["Error"] = "Could not retrieve email from Google."; return RedirectToAction(nameof(Login)); }

            var user = await _authService.EmailExistsAsync(email) ?
                (await _authService.LoginAsync(new LoginDto { Email = email, Password = "" })).User :
                await RegisterGoogleUserAsync(result.Principal!);

            if (user != null) await SignInUserAsync(user);
            return RedirectToAction("Index", "Dashboard");
        }

        private async Task<Application.DTOs.UserDto?> RegisterGoogleUserAsync(ClaimsPrincipal principal)
        {
            var email = principal.FindFirstValue(ClaimTypes.Email)!;
            var name = principal.FindFirstValue(ClaimTypes.Name) ?? email;
            var nameParts = name.Split(' ', 2);
            var registerDto = new RegisterDto { Email = email, FirstName = nameParts[0], LastName = nameParts.Length > 1 ? nameParts[1] : "", Password = Guid.NewGuid().ToString(), ConfirmPassword = Guid.NewGuid().ToString() };
            var result = await _authService.RegisterAsync(registerDto);
            return result.Success ? result.User : null;
        }

        private async Task SignInUserAsync(Application.DTOs.UserDto user)
        {
            var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, user.Id.ToString()), new(ClaimTypes.Email, user.Email), new(ClaimTypes.Name, user.FullName) };
            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }
    }
}
