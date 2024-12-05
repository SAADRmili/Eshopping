using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.QueryHandlers;
public class GetProductByNameHandler(IProductRepository productRepository) : IRequestHandler<GetProductByNameQuery, IList<ProductResponse>>
{
    public async Task<IList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        var result = await productRepository.GetAllProductByName(request.Name);
        if (result == null) return null;
        var product = ProductMapper.Mapper.Map<IList<ProductResponse>>(result);
        return product;
    }
}
