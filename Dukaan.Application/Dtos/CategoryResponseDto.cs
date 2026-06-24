namespace Dukaan.Application.Dtos;

public record CategoryResponseDto(
    Guid Id,
    string Name,
    string? Description,
    Guid? ParentCategoryId,
    List<CategoryResponseDto> SubCategories
);
