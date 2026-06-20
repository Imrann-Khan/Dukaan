using Dukaan.Application.Interfaces;
using Dukaan.Domain.Entities;

namespace Dukaan.Application.Services;


public class TenantService(IUnitOfWork unitOfWork)
{
    public async Task<Tenant> CreateTenantAsync(string name, string slug)
    {
        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            Name = name,
            Slug = slug
        };

        var repo = unitOfWork.Repository<Tenant>();
        await repo.AddAsync(tenant);
        await unitOfWork.SaveChangesAsync();

        return tenant;
    }
}