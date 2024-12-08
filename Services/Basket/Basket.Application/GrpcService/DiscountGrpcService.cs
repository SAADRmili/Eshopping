using Discount.Grpc.Protos;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Basket.Application.GrpcService;
public class DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient, ILogger<DiscountGrpcService> logger)
{
    public async Task<CouponModel> GetDiscount(string productName)
    {
        try
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await discountProtoServiceClient.GetDiscountAsync(discountRequest);
        }
        catch (RpcException ex)
        {
            logger.LogError($"gRPC error: {ex.StatusCode} - {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError($"Unexpected error: {ex.Message}");
            throw;
        }

    }
}