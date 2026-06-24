namespace Dukaan.Application.Features.Categories.Queries.GetCategories;

using MediatR;
using Dukaan.Application.Dtos;

public record GetCategoriesQuery() : IRequest<List<CategoryResponseDto>>;
