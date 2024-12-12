using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers.Commands;
public class CheckoutOrderCommandHandlerV2(IOrderRepository orderRepository, IMapper mapper, ILogger<CheckoutOrderCommandHandler> logger) : IRequestHandler<CheckoutOrderCommandV2, int>
{
    public async Task<int> Handle(CheckoutOrderCommandV2 request, CancellationToken cancellationToken)
    {
        var order = mapper.Map<Order>(request);
        var result = await orderRepository.AddAsync(order);
        logger.LogInformation($"OrderID {result.Id} Succesfully created");
        return result.Id;
    }
}
