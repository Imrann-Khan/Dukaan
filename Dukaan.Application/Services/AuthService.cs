using Dukaan.Application.Dtos;
using Dukaan.Domain.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Dukaan.Application.Services;

public class AuthService(UserManager<Merchant> userManager, TenantService tenantService, IConfiguration config)
{
    public async Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO request)
    {
        var tenant = await tenantService.CreateTenantAsync(request.StoreName, request.StoreSlug);
        var user = new Merchant
        {
            UserName = request.Username,
            Email = request.Email,
            TenantId = tenant.Id
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if(!result.Succeeded)
        {
            throw new Exception("Failed to create user: "+string.Join(", ",result.Errors.Select(e => e.Description)));
        }
        var token = GenerateJwtToken(user);
        return new AuthResponseDTO(token, user.UserName, user.TenantId);
    }

    public async Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if(user == null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new Exception("Invalid credentials");
        }       

        var token = GenerateJwtToken(user);

        var result = await userManager.CheckPasswordAsync(user, request.Password);
        return new AuthResponseDTO(token, user.UserName, user.TenantId);
    }

    private string GenerateJwtToken(Merchant user)
    {
        var claims = new []
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim("tenant_id", user.TenantId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}