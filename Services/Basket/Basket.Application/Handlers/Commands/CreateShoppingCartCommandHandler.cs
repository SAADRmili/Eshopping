using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers.Commands;
public class CreateShoppingCartCommandHandler(IBasketRepository basketRepository) : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        //TODO : WILL BE INTEGRATION DISCOUNT SERVICE 
        var result = await basketRepository.UpdateBasket(new ShoppingCart
        {
            UserName = request.UserName,
            Items = request.Items,
        });

        var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(result);
        return shoppingCartResponse;
    }
}
