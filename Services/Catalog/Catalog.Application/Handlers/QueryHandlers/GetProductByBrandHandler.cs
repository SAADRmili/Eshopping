using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.QueryHandlers;
public class GetProductByBrandHandler(IProductRepository productRepository) : IRequestHandler<GetProductByBrandQuery, IList<ProductResponse>>
{
    public async Task<IList<ProductResponse>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
    {
        var result = await productRepository.GetAllProductByBrand(request.Name);
        if (result == null) return null;
        var products = ProductMapper.Mapper.Map<IList<ProductResponse>>(result);
        return products;
    }
}
