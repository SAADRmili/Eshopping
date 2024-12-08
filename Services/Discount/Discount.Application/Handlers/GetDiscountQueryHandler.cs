﻿using AutoMapper;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers;
public class GetDiscountQueryHandler(IDiscountRepository discountRepository, IMapper mapper) : IRequestHandler<GetDiscountQuery, CouponModel>
{
    public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var result = await discountRepository.GetDiscount(request.ProductName);
        if (result == null) throw new RpcException(new Status(StatusCode.NotFound, $"Discount for the Product Name = {request.ProductName} not Found"));
        var coupon = mapper.Map<CouponModel>(result);
        return coupon;
    }
}