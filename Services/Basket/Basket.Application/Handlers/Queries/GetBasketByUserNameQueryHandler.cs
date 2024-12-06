using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers.Queries;
public class GetBasketByUserNameQueryHandler(IBasketRepository basketRepository) : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
{
    public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        var result = await basketRepository.GetBasket(request.UserName);
        if (result == null) return null;
        var basket = BasketMapper.Mapper.Map<ShoppingCartResponse>(result);
        return basket;
    }
}
