using Dukaan.Application.Features.Products.Queries.GetProductById;
using Dukaan.Application.Features.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dukaan.Application.Features.Products.Queries.GetProductById;
using Dukaan.Domain.Entities;
using Dukaan.Application.Features.Products.Commands.CreateProduct;

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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await sender.Send(new GetProductByIdQuery(id));
        if(product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var product = await sender.Send(command);
        return Created($"/products/{product.Id}", product);
    } 
}