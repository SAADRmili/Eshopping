using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;
public class GetAllProductsHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, IList<ProductResponse>>
{
    public async Task<IList<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await productRepository.GetAllProducts();
        var products = ProductMapper.Mapper.Map<IList<ProductResponse>>(result.ToList());
        return products;
    }
}
