namespace Dukaan.Domain.Entities;

using Dukaan.Domain.Interfaces;

public class CategorizedProduct : ITenantEntity
{
    public Guid ProductId { get; set; }   
    public Guid CategoryId { get; set; }   
    
    public Guid TenantId { get; set; }    

    public virtual Product Product { get; set; } = null!;
    public virtual Category Category { get; set; } = null!;
}
