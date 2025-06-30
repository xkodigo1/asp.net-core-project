using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Common.DTOs.Users;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly IPasswordHasher _passwordHasher;

    public AuthController(IUnitOfWork unitOfWork, IConfiguration configuration, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UsersExtended.GetByEmailAsync(loginDto.Email, cancellationToken);
        
        if (user == null)
            return Unauthorized(new { message = "Email o contraseña incorrectos" });

        if (!_passwordHasher.VerifyPassword(loginDto.Password, user.Password))
            return Unauthorized(new { message = "Email o contraseña incorrectos" });

        var token = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken();

        // Guardar el refresh token
        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiryDate = DateTime.UtcNow.AddDays(30),
            CreatedBy = user.Email,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshTokenEntity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(new AuthResponseDto
        {
            Token = token,
            RefreshToken = refreshToken,
            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Roles = user.UserRoles.Select(ur => new RoleDto 
                { 
                    Id = ur.Role.Id, 
                    Name = ur.Role.Name, 
                    Description = ur.Role.Description 
                }).ToList()
            }
        });
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthResponseDto>> RefreshToken(RefreshTokenDto refreshTokenDto, CancellationToken cancellationToken)
    {
        var refreshToken = await _unitOfWork.Repository<RefreshToken>()
            .FindOneAsync(rt => rt.Token == refreshTokenDto.RefreshToken && !rt.IsRevoked, cancellationToken);

        if (refreshToken == null)
            return Unauthorized(new { message = "Invalid refresh token" });

        if (refreshToken.ExpiryDate < DateTime.UtcNow)
        {
            refreshToken.IsRevoked = true;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unauthorized(new { message = "Refresh token expired" });
        }

        var user = await _unitOfWork.Users.GetByIdAsync(refreshToken.UserId, cancellationToken);
        if (user == null)
            return Unauthorized(new { message = "User not found" });

        var token = GenerateJwtToken(user);
        var newRefreshToken = GenerateRefreshToken();

        // Revocar el refresh token anterior
        refreshToken.IsRevoked = true;

        // Crear nuevo refresh token
        var newRefreshTokenEntity = new RefreshToken
        {
            Token = newRefreshToken,
            UserId = user.Id,
            ExpiryDate = DateTime.UtcNow.AddDays(30),
            CreatedBy = user.Email,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Repository<RefreshToken>().AddAsync(newRefreshTokenEntity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(new AuthResponseDto
        {
            Token = token,
            RefreshToken = newRefreshToken,
            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Roles = user.UserRoles.Select(ur => new RoleDto 
                { 
                    Id = ur.Role.Id, 
                    Name = ur.Role.Name, 
                    Description = ur.Role.Description 
                }).ToList()
            }
        });
    }

    [Authorize]
    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken(RefreshTokenDto refreshTokenDto, CancellationToken cancellationToken)
    {
        var refreshToken = await _unitOfWork.Repository<RefreshToken>()
            .FindOneAsync(rt => rt.Token == refreshTokenDto.RefreshToken && !rt.IsRevoked, cancellationToken);

        if (refreshToken == null)
            return NotFound(new { message = "Token not found" });

        refreshToken.IsRevoked = true;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(new { message = "Token revoked" });
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found in configuration")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username)
        };

        foreach (var userRole in user.UserRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}

public class LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RefreshTokenDto
{
    public string RefreshToken { get; set; } = string.Empty;
}

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public UserDto User { get; set; } = null!;
} 