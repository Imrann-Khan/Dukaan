using Microsoft.EntityFrameworkCore;
using Dukaan.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Dukaan.Domain.Data.Model;
using Microsoft.AspNetCore.Identity;
using Dukaan.Infrastructure.Services.Interfaces;
using Dukaan.Infrastructure.Services;
using Dukaan.Domain.Interfaces;
using System.Reflection;
namespace Dukaan.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<Merchant, IdentityRole<Guid>, Guid>
{
    private readonly ITenantProvider _tenantProvider;
    public AppDbContext(DbContextOptions<AppDbContext> options, ITenantProvider tenantProvider) : base(options)
    {
        _tenantProvider = tenantProvider;
    }
    public DbSet<Tenant> Tenants {get; set;} 
    public DbSet<Product> Products {get; set;}
    public DbSet<Category> Categories {get; set;}
    public DbSet<CategorizedProduct> CategorizedProducts {get; set;}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Category>()
            .HasOne(c => c.ParentCategory)           
            .WithMany(c => c.SubCategories)          
            .HasForeignKey(c => c.ParentCategoryId)  
            .OnDelete(DeleteBehavior.Restrict);      

        builder.Entity<CategorizedProduct>()
            .HasKey(cp => new { cp.CategoryId, cp.ProductId });

        foreach(var entityType in builder.Model.GetEntityTypes())
        {
            if(typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                if(entityType.ClrType == typeof(Merchant))
                {
                    builder.Entity<Merchant>().HasQueryFilter(u =>
                    _tenantProvider.GetTenantId() == null || u.TenantId == _tenantProvider.GetTenantId());
                }
                else
                {
                    var method = typeof(AppDbContext)
                    .GetMethod(nameof(SetQueryFilter), BindingFlags.NonPublic | BindingFlags.Instance);
                    method!.MakeGenericMethod(entityType.ClrType).Invoke(this, [builder]);
                }
            }
        }
    }
    private void SetQueryFilter<T>(ModelBuilder builder) where T: class, ITenantEntity
    {
        builder.Entity<T>().HasQueryFilter(p => p.TenantId == _tenantProvider.GetTenantId());
    }
}