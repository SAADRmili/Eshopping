using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands;
using Ordering.Application.Queries;
using Ordering.Application.Response;

namespace Ordering.API.Controllers;

public class OrderController(IMediator mediator, ILogger<OrderController> logger) : ApiController
{
    [HttpGet("{userName}", Name = "GetOrdersByUserName")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>), 200)]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByUserName(string userName)
    {
        var query = new GetOrderListQuery(userName);
        var result = await mediator.Send(query);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost(Name = "CheckoutOrder")]
    [ProducesResponseType(200)]
    public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
    {
        var result = await mediator.Send(command);
        return result != 0 ? Ok(result) : NotFound();
    }

    [HttpPut(Name = "UpdateOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> UpdateOrder([FromBody] UpdateOrderCommand command)
    {
        var result = await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        var cmd = new DeleteOrderCommand() { Id = id };
        await mediator.Send(cmd);
        return NoContent();
    }
}
