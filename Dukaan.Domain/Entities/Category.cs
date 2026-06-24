using Dukaan.Domain.Interfaces;

namespace Dukaan.Domain.Entities;

public class Category : ITenantEntity
{
    public Guid Id {get; set;}
    public Guid TenantId {get; set;}
    public string Name {get; set;} = string.Empty;
    public string? Description {get; set;}
    public bool IsActive {get; set;} = true;

    public Guid? ParentCategoryId {get; set;}
    public virtual Category? ParentCategory {get; set;}
    public virtual ICollection<Category> SubCategories {get; set;} = [];
    public virtual ICollection<CategorizedProduct> ProductLinks {get; set;} = [];
}