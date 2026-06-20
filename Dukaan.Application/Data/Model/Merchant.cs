
using Dukaan.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Dukaan.Domain.Data.Model;

public class Merchant : IdentityUser<Guid>, ITenantEntity
{
    public Guid TenantId {get; set;}
}