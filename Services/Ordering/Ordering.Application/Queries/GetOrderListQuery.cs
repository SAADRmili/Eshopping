using MediatR;
using Ordering.Application.Response;

namespace Ordering.Application.Queries;
public class GetOrderListQuery : IRequest<IList<OrderResponse>>
{
    public string UserName { get; set; }
    public GetOrderListQuery(string userName)
    {
        UserName = userName;
    }
}
