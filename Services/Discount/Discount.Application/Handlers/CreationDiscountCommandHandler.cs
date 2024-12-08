using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers;
public class CreationDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper) : IRequestHandler<CreationDiscountCommand, CouponModel>
{
    public async Task<CouponModel> Handle(CreationDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = mapper.Map<Coupon>(request);
        var result = await discountRepository.CreateDiscount(coupon);
        if (!result) throw new RpcException(new Status(StatusCode.Internal, $"The Creation of Coupon was failed"));
        var couponModel = mapper.Map<CouponModel>(coupon);
        return couponModel;
    }
}
