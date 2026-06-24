namespace Dukaan.Application.Features.Categories.Queries.GetCategories;

using MediatR;
using Dukaan.Application.Interfaces;
using Dukaan.Domain.Entities;
using Dukaan.Application.Dtos;
using Microsoft.EntityFrameworkCore;

public class GetCategoriesQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetCategoriesQuery, List<CategoryResponseDto>>
{
    public async Task<List<CategoryResponseDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.Repository<Category>();
        
        var rootCategories = await repo.GetAllAsQueryable()
            .Where(c => c.ParentCategoryId == null && c.IsActive)
            .Include(c => c.SubCategories)
            .ToListAsync(cancellationToken);

        return rootCategories.Select(MapToDto).ToList();
    }

    private CategoryResponseDto MapToDto(Category category)
    {
        return new CategoryResponseDto(
            category.Id,
            category.Name,
            category.Description,
            category.ParentCategoryId,
            category.SubCategories
                .Where(s => s.IsActive)
                .Select(MapToDto) 
                .ToList()
        );
    }
}
