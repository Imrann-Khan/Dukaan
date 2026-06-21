namespace Dukaan.Application.Features.Products.Queries.GetProducts;

using MediatR;
using Dukaan.Domain.Entities;

public record GetProductsQuery() : IRequest<List<Product>>;
