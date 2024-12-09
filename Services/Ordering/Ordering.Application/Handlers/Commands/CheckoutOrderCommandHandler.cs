using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers.Commands;
public class CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger logger) : IRequestHandler<CheckoutOrderCommand, int>
{
    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var order = mapper.Map<Order>(request);
        var result = await orderRepository.AddAsync(order);
        logger.LogInformation($"OrderID {result.Id} Succesfully created");
        return result.Id;
    }
}
