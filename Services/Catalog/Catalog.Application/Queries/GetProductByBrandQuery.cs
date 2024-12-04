using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;
public class GetProductByBrandQuery : IRequest<IList<ProductResponse>>
{
    public string Name { get; set; }
    public GetProductByBrandQuery(string name)
    {
        Name = name;
    }
}
