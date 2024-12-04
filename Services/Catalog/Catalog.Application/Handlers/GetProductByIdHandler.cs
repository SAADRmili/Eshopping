using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;
public class GetProductByIdHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await productRepository.GetProduct(request.Id);
        if (result == null) return null;
        var product = ProductMapper.Mapper.Map<ProductResponse>(result);
        return product;
    }
}
