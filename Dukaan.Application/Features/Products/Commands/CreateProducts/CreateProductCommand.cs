namespace Dukaan.Application.Features.Products.Commands.CreateProduct;

using Dukaan.Domain.Entities;
using MediatR;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price
) : IRequest<Product>;