using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data;
public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
    {
        if (!context.Orders.Any())
        {
            context.Orders.AddRange(GetOrders());
            await context.SaveChangesAsync();
            logger.LogInformation($"Ordering Database : {typeof(OrderContextSeed).Name} seeded");
        }
    }

    private static IEnumerable<Order> GetOrders()
    => new List<Order>()
    {
        new()
        {
            UserName="SAADRmili",
            FirstName="Saad",
            LastName="Rmili",
            EmailAddress="saad@eshopping.net",
            AddressLine="Guliz",
            Country="Morocco",
            TotalPrice=6000,
            State="Marrakesh Asfi",
            ZipCode="40000",
            CardName="SAAD",
            CardNumber="12334555575589",
            Expiration="12/26",
            Cvv="432",
            PaymentMethod =1,
            CreatedBy ="System",
        }
    };
}
