using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;
public class GetAllTypesHandler(ITypesRepository typesRepository) : IRequestHandler<GetAllTypesQuery, IList<TypeReponse>>
{
    public async Task<IList<TypeReponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await typesRepository.GetProductTypes();
        var types = ProductMapper.Mapper.Map<List<TypeReponse>>(result.ToList());
        return types;
    }
}
