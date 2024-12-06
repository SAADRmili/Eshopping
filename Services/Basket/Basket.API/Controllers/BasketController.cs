using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

public class BasketController(IMediator mediator) : ApiController
{
    [HttpGet]
    [Route("[action]/{userName}", Name = "GetBasketByUserName")]
    [ProducesResponseType(typeof(ShoppingCartResponse), 200)]
    public async Task<ActionResult<ShoppingCartResponse>> GetBasketByUserName(string userName)
    {
        var query = new GetBasketByUserNameQuery(userName);
        var result = await mediator.Send(query);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    [Route("[action]", Name = "CreateBasket")]
    [ProducesResponseType(typeof(ShoppingCartResponse), 200)]
    public async Task<ActionResult<ShoppingCartResponse>> CreateBasket([FromBody] CreateShoppingCartCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await mediator.Send(command);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpDelete]
    [Route("[action]/{userName}", Name = "DeleteBasket")]
    [ProducesResponseType(200)]
    public async Task<ActionResult> DeleteBasket(string userName)
    {
        var command = new DeleteBasketByUserNameCommand(userName);
        var result = await mediator.Send(command);
        return Ok();
    }
}
