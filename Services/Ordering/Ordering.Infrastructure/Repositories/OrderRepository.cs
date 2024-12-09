using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository(OrderContext orderContext) : RespositoryBase<Order>(orderContext), IOrderRepository
{
    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        var orders = await orderContext
            .Orders
            .Where(x => x.UserName == userName)
            .ToListAsync();
        return orders;
    }
}
