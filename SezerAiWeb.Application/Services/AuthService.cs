using AutoMapper;
using SezerAiWeb.Application.DTOs;
using SezerAiWeb.Application.DTOs.Auth;
using SezerAiWeb.Application.Interfaces;
using SezerAiWeb.Application.Repositories;
using SezerAiWeb.Domain.Entities;

namespace SezerAiWeb.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuthService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthResultDto> RegisterAsync(RegisterDto dto)
    {
        // Validate password match
        if (dto.Password != dto.ConfirmPassword)
        {
            return new AuthResultDto
            {
                Success = false,
                Message = "Passwords do not match."
            };
        }

        // Validate password length
        if (dto.Password.Length < 8)
        {
            return new AuthResultDto
            {
                Success = false,
                Message = "Password must be at least 8 characters long."
            };
        }

        // Check if email already exists
        if (await _unitOfWork.Users.EmailExistsAsync(dto.Email))
        {
            return new AuthResultDto
            {
                Success = false,
                Message = "Email already registered."
            };
        }

        // Hash password
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        // Create user
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            PasswordHash = hashedPassword,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            IsEmailVerified = false,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Assign default "Viewer" role
        var viewerRole = await _unitOfWork.Roles.GetByNameAsync("Viewer");
        if (viewerRole != null)
        {
            var userRole = new UserRole
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                RoleId = viewerRole.Id,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.UserRoles.AddAsync(userRole);
            await _unitOfWork.SaveChangesAsync();
        }

        var userDto = _mapper.Map<UserDto>(user);
        userDto.Roles = viewerRole != null ? new List<string> { viewerRole.Name } : new List<string>();

        return new AuthResultDto
        {
            Success = true,
            Message = "Registration successful!",
            User = userDto
        };
    }

    public async Task<AuthResultDto> LoginAsync(LoginDto dto)
    {
        // Find user by email
        var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
        if (user == null)
        {
            return new AuthResultDto
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }

        // Verify password
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            return new AuthResultDto
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }

        // Check if user is active
        if (!user.IsActive)
        {
            return new AuthResultDto
            {
                Success = false,
                Message = "Your account has been deactivated. Please contact support."
            };
        }

        // Get user with roles
        var userWithRoles = await _unitOfWork.Users.GetUserWithRolesAsync(user.Id);
        var userDto = _mapper.Map<UserDto>(userWithRoles);

        return new AuthResultDto
        {
            Success = true,
            Message = "Login successful!",
            User = userDto
        };
    }

    public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
        if (user == null)
        {
            // Return true even if user doesn't exist for security reasons
            // (Don't reveal whether email exists)
            return true;
        }

        // TODO: Generate password reset token
        // TODO: Send password reset email
        // For now, just return true

        return true;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _unitOfWork.Users.EmailExistsAsync(email);
    }
}
