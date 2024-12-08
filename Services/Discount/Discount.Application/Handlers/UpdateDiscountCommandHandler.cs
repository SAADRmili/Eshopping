using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers;
public class UpdateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper) : IRequestHandler<UpdateDiscountCommand, CouponModel>
{
    public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = mapper.Map<Coupon>(request);
        var result = await discountRepository.UpdateDiscount(coupon);
        if (!result) throw new RpcException(new Status(StatusCode.Internal, $"The Update of Coupon was failed"));
        var couponModel = mapper.Map<CouponModel>(coupon);
        return couponModel;
    }
}
