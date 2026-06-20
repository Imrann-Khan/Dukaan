using Dukaan.Application.Interfaces;
using Dukaan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Dukaan.Infrastructure.UnitOfWork;
using Dukaan.Domain.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Dukaan.Infrastructure.Services.Interfaces;
using Dukaan.Infrastructure.Services;
using Dukaan.Infrastructure.Interceptors;
using Dukaan.Application.Services;

var builder = WebApplication.CreateBuilder();

// Database Connection
builder.Services.AddDbContext<AppDbContext>((sp, options) => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .AddInterceptors(sp.GetRequiredService<TenantInterceptor>());
});

// User Management (Identity)
builder.Services.AddIdentity<Merchant, IdentityRole<Guid>>().AddEntityFrameworkStores<AppDbContext>();

// Authentication
builder.Services.AddAuthentication( options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddHttpContextAccessor();

// DI Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<ITenantProvider, TenantProvider>();
builder.Services.AddScoped<TenantInterceptor>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TenantService>();

var app = builder.Build();

// Middleware pipeline
app.MapOpenApi();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// // API routes
// app.MapGet("/products", async (IUnitOfWork uow) =>
// {
//     var repo = uow.Repository<Product>();
//     return Results.Ok(await repo.GetAllAsync());
// });

// app.MapPost("/products", async (IUnitOfWork uow, Product product) =>
// {
//     var repo = uow.Repository<Product>();
//     await repo.AddAsync(product);
//     await uow.SaveChangesAsync();
//     return Results.Created($"/product/{product.Id}", product);
// });

await app.RunAsync();