namespace Dukaan.Infrastructure.Services;
using Dukaan.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;

public class TenantProvider(IHttpContextAccessor httpContextAccessor) : ITenantProvider
{
    public Guid? GetTenantId()
    {
        var tenantClaim = httpContextAccessor.HttpContext?.User?.FindFirst("tenant_id");
        return tenantClaim != null ? Guid.Parse(tenantClaim.Value) : null;
    }
}