using Dukaan.Application.Features.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dukaan.Host.Controllers;

[Authorize]
[ApiController]
[Route("/[controller]")]
public class ProductsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await sender.Send(new GetProductsQuery());
        return Ok(products);
    }
}