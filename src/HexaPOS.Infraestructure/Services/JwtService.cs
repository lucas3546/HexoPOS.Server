using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HexaPOS.Infraestructure.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;
    private readonly UserManager<Account> _userManager;

    public JwtService(
        IConfiguration config,
        UserManager<Account> userManager)
    {
        _config = config;
        _userManager = userManager;
    }

    public async Task<string> GenerateAccessTokenAsync(
        Account account)
    {
        var roles = await _userManager.GetRolesAsync(account);

        var claims = new List<Claim>
        {
            new Claim(
                JwtRegisteredClaimNames.Sub,
                account.Id.ToString()),

            new Claim(
                JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()),

            new Claim(
                JwtRegisteredClaimNames.Iat,
                DateTimeOffset.UtcNow
                    .ToUnixTimeSeconds()
                    .ToString(),
                ClaimValueTypes.Integer64),

            new Claim(
                ClaimTypes.NameIdentifier,
                account.Id.ToString()),

            new Claim(
                ClaimTypes.Name,
                account.UserName ?? string.Empty),

            new Claim(
                ClaimTypes.Email,
                account.Email ?? string.Empty),

            new Claim(
                "device_id",
                account.DeviceId),

            new Claim(
                "auth_type",
                "user")
        };

        foreach (var role in roles)
        {
            claims.Add(
                new Claim(ClaimTypes.Role, role));
        }

        var expiresAt = DateTime.UtcNow.AddDays(7);

        var jwtKey = _config["JWT:Key"]!;

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenKey = Encoding.UTF8.GetBytes(jwtKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),

            Expires = expiresAt,

            Issuer = _config["JWT:Issuer"],

            Audience = _config["JWT:Audience"],

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var accessToken = tokenHandler.WriteToken(token);

        return accessToken;
    }
}