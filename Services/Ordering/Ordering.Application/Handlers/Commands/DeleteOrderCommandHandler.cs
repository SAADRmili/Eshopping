using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers.Commands;
public class DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger) : IRequestHandler<DeleteOrderCommand, Unit>
{
    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToDelete = await orderRepository.GetByIdAsync(request.Id);
        if (orderToDelete == null)
        {
            throw new OrderNotFoundException(nameof(Order), request.Id);
        }
        await orderRepository.DeleteAsync(orderToDelete);
        logger.LogInformation($"OrderId : {request.Id} successfully deleted");
        return Unit.Value;
    }
}
