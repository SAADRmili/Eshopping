using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entites;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.CommandHandlers;
public class CreateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<CreateProductCommand, ProductResponse>
{
    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productEnity = ProductMapper.Mapper.Map<Product>(request);
        if (productEnity == null)
            throw new ApplicationException("There is an issue with mapping while creation new product");
        var product = await productRepository.CreateProduct(productEnity);
        return ProductMapper.Mapper.Map<ProductResponse>(product);
    }
}
