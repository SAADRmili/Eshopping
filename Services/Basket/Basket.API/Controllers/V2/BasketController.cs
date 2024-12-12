using Asp.Versioning;
using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Core.Entities;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers.V2;

[ApiVersion("2")]
[Route("api/v{verion:apiVersion}/[controller]")]
[ApiController]
public class BasketController(IMediator mediator, IPublishEndpoint publishEndpoint, ILogger<BasketController> logger) : ControllerBase
{
    [Route("[action]")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckoutV2 basketCheckout)
    {
        //Get the exsiting basket with username 
        var query = new GetBasketByUserNameQuery(basketCheckout.UserName);
        var basket = await mediator.Send(query);
        if (basket == null) return BadRequest();

        var eventMassage = BasketMapper.Mapper.Map<BasketCheckoutEventV2>(basketCheckout);
        eventMassage.TotalPrice = basket.TotalPrice;
        await publishEndpoint.Publish(eventMassage);
        logger.LogInformation($"Basket Published for {basket.UserName} With V2 endpoint");
        //remove the baseket
        var deletecmd = new DeleteBasketByUserNameCommand(basket.UserName);
        await mediator.Send(deletecmd);
        return Accepted();
    }
}
