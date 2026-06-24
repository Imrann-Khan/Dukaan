namespace Dukaan.Application.Features.Categories.Commands.CreateCategory;

using MediatR;
using Dukaan.Application.Interfaces;
using Dukaan.Domain.Entities;

public class CreateCategoryCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateCategoryCommand, Category>
{
    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            ParentCategoryId = request.ParentCategoryId,
            IsActive = true
        };

        var repo = unitOfWork.Repository<Category>();
        await repo.AddAsync(category);
        await unitOfWork.SaveChangesAsync(); 

        return category;
    }
}
