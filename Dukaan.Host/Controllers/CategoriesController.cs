using Dukaan.Application.Features.Categories.Commands.CreateCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dukaan.Host.Controllers;

[Authorize]
[ApiController]
[Route("/[controller]")]
public class CategoriesController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        var category = await sender.Send(command);
        return Created($"/Categories/{category.Id}", category);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await sender.Send(new Dukaan.Application.Features.Categories.Queries.GetCategories.GetCategoriesQuery());
        return Ok(categories);
    }
}