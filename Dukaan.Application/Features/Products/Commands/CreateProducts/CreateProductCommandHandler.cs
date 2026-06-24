using Dukaan.Application.Interfaces;
using Dukaan.Domain.Entities;
using MediatR;

namespace Dukaan.Application.Features.Products.Commands.CreateProduct;


public class CreateProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, Product>
{
    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        var repo = unitOfWork.Repository<Product>();
        await repo.AddAsync(product);
        await unitOfWork.SaveChangesAsync();

        return product;
    }
}