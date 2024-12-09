using AutoMapper;
using MediatR;
using Ordering.Application.Queries;
using Ordering.Application.Response;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers.Queries;
public class GetOrderListQueryHandler(IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrderListQuery, IList<OrderResponse>>
{
    public async Task<IList<OrderResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var result = await orderRepository.GetOrdersByUserName(request.UserName);
        if (result is null) return Enumerable.Empty<OrderResponse>().ToList();
        var orderResponse = mapper.Map<IList<OrderResponse>>(result);
        return orderResponse;
    }
}
