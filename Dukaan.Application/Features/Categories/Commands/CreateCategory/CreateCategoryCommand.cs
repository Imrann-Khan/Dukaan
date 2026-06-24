namespace Dukaan.Application.Features.Categories.Commands.CreateCategory;

using MediatR;
using Dukaan.Domain.Entities;

public record CreateCategoryCommand(
    string Name,
    string? Description,
    Guid? ParentCategoryId 
) : IRequest<Category>;
