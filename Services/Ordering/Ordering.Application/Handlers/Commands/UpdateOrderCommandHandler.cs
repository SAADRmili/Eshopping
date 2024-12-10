using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers.Commands;
public class UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger) : IRequestHandler<UpdateOrderCommand, Unit>
{
    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderUpdated = await orderRepository.GetByIdAsync(request.Id);
        if (orderUpdated is null)
            throw new OrderNotFoundException(nameof(Order), request.Id);

        mapper.Map(request, orderUpdated, typeof(UpdateOrderCommand), typeof(Order));
        await orderRepository.UpdateAsync(orderUpdated);
        logger.LogInformation($"Order Id:{orderUpdated.Id} is successfuly updated");
        return Unit.Value;
    }
}
