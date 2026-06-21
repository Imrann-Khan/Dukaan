namespace Dukaan.Application.Features.Auth.Commands.Register;

using MediatR;
using Dukaan.Application.Dtos;
using Microsoft.AspNetCore.Identity;
using Dukaan.Domain.Data.Model;
using Dukaan.Application.Services;
using Microsoft.Extensions.Configuration;

public class RegisterCommandHandler(UserManager<Merchant> userManager, TenantService tenantService, AuthService authService) : IRequestHandler<RegisterCommand, AuthResponseDTO>
{
    public async Task<AuthResponseDTO> Handle(RegisterCommand request, CancellationToken token)
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
        var Token = authService.GenerateJwtToken(user);
        return new AuthResponseDTO(Token, user.UserName, user.TenantId);
    }
}