using Catalog.Application.Commands;
using Catalog.Core.Entites;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CommandHandlers;
public class UpdateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand, bool>
{
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        => await productRepository.UpdateProduct(new Product
        {
            Id = request.Id,
            Name = request.Name,
            Summary = request.Summary,
            Price = request.Price,
            ImageFile = request.ImageFile,
            Description = request.Description,
            Brands = request.Brands,
            Types = request.Types,
        });
}
