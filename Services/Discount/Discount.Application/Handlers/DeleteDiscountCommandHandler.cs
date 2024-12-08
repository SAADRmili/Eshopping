using Discount.Application.Commands;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers;
public class DeleteDiscountCommandHandler(IDiscountRepository discountRepository) : IRequestHandler<DeleteDiscountCommand, DeleteDiscountResponse>
{
    public async Task<DeleteDiscountResponse> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        var result = await discountRepository.DeleteDiscount(request.ProductName);
        return new DeleteDiscountResponse
        {
            Success = result,
        };
    }
}
