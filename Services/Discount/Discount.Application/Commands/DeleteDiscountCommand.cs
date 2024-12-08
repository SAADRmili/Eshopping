using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands;
public class DeleteDiscountCommand : IRequest<DeleteDiscountResponse>
{
    public DeleteDiscountCommand(string productName)
    {
        ProductName = productName;
    }

    public string ProductName { get; set; }

}
