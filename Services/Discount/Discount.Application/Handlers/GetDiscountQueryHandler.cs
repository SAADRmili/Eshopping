using AutoMapper;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers;
public class GetDiscountQueryHandler(IDiscountRepository discountRepository, IMapper mapper, ILogger<GetDiscountQueryHandler> logger) : IRequestHandler<GetDiscountQuery, CouponModel>
{
    public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var result = await discountRepository.GetDiscount(request.ProductName);
        if (result == null) throw new RpcException(new Status(StatusCode.NotFound, $"Discount for the Product Name = {request.ProductName} not Found"));
        var couponModel = new CouponModel
        {
            Id = result.Id,
            Amount = result.Amount,
            Description = result.Description,
            ProductName = result.ProductName
        };
        logger.LogInformation($"Coupon for the {request.ProductName} is fetched");
        return couponModel;
    }
}
