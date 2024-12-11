using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Entities;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers;

public class BasketController(IMediator mediator, IPublishEndpoint publishEndpoint, ILogger<BasketController> logger) : ApiController
{
    [HttpGet]
    [Route("[action]/{userName}", Name = "GetBasketByUserName")]
    [ProducesResponseType(typeof(ShoppingCartResponse), 200)]
    public async Task<ActionResult<ShoppingCartResponse>> GetBasketByUserName(string userName)
    {
        var query = new GetBasketByUserNameQuery(userName);
        var result = await mediator.Send(query);
        logger.LogInformation(result is not null ? $"Basket for {result.UserName}" : "Basket Not found");
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
        logger.LogInformation($"Basket Deleted for {userName}");
        return Ok();
    }

    [Route("[action]")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
    {
        //Get the exsiting basket with username 
        var query = new GetBasketByUserNameQuery(basketCheckout.UserName);
        var basket = await mediator.Send(query);
        if (basket == null) return BadRequest();

        var eventMassage = BasketMapper.Mapper.Map<BasketCheckoutEvent>(basketCheckout);
        eventMassage.TotalPrice = basket.TotalPrice;
        await publishEndpoint.Publish(eventMassage);
        logger.LogInformation($"Basket Published for {basket.UserName}");
        //remove the baseket
        var deletecmd = new DeleteBasketByUserNameCommand(basket.UserName);
        await mediator.Send(deletecmd);
        return Accepted();
    }
}
