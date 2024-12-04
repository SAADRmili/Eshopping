using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entites;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;
public class GetAllBrandsHandler(IBrandRepository brandRepository) : IRequestHandler<GetAllBrandsQuery, IList<BrandsResponse>>
{
    public async Task<IList<BrandsResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var result = await brandRepository.GetBrands();
        var brandslist = ProductMapper
            .Mapper
            .Map<IList<ProductBrand>, IList<BrandsResponse>>(result.ToList());
        return brandslist;
    }
}
