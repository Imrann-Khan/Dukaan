using Dukaan.Domain.Entities;
using MediatR;

namespace Dukaan.Application.Features.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<Product?>;