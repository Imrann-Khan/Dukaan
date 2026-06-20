using Dukaan.Domain.Interfaces;
using Dukaan.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace Dukaan.Infrastructure.Interceptors;


public class TenantInterceptor(ITenantProvider tenantProvider) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        SetTenantId(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, 
    InterceptionResult<int> result, CancellationToken cancellationToken)
    {
        SetTenantId(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void SetTenantId(DbContext? context)
    {
        if(context == null) return;
        var tenant_id = tenantProvider.GetTenantId();
        if(tenant_id == null) return;

        foreach(var entry in context.ChangeTracker.Entries<ITenantEntity>())
        {
            if(entry.State == EntityState.Added)
            {
                entry.Entity.TenantId = tenant_id.Value;
            }
        }
    }
}