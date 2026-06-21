namespace Dukaan.Application.Features.Products.Queries.GetProducts;

using MediatR;
using Dukaan.Application.Interfaces;
using Dukaan.Domain.Entities;

public class GetProductsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetProductsQuery, List<Product>>
{
    public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.Repository<Product>();
        return await repo.GetAllAsync();
    }
}
