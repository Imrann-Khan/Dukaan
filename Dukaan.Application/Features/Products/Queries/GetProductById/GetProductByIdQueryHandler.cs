using Dukaan.Application.Interfaces;
using Dukaan.Domain.Entities;
using MediatR;

namespace Dukaan.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetProductByIdQuery, Product?>
{
    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.Repository<Product>();
        return await repo.GetByIdAsync(request.Id);
    }
}