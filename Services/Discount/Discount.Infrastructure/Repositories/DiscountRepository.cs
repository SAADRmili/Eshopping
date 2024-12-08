using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Repositories;
public class DiscountRepository(IConfiguration configuration) : IDiscountRepository
{

    public async Task<Coupon> GetDiscount(string productName)
    {
        await using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var coupon = await connection.QueryFirstAsync<Coupon>
            ($"Select c.* from Coupon c WHERE c.ProductName = @ProductName",
            new { ProductName = productName });
        if (coupon is null)
        {
            return new Coupon
            {
                ProductName = "No Discount",
                Amount = 0,
                Description = "No Discount Availables"
            };
        }
        return coupon;
    }
    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var affected = await connection.ExecuteAsync
            (@$"INSERT INTO Coupon (ProductName,Description,Amount)
             VALUES (@ProductName,@Description,@Amount)",
             new
             {
                 ProductName = coupon.ProductName,
                 Description = coupon.Description,
                 Amount = coupon.Amount,
             });

        return affected != 0 ? true : false;

    }
    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var affected = await connection.ExecuteAsync
            (@$"UPDATE Coupon SET 
             ProductName =@ProductName,
             Description=@Description,
             Amount=@Amount
            WHERE Id=@Id",
             new
             {
                 Id = coupon.Id,
                 ProductName = coupon.ProductName,
                 Description = coupon.Description,
                 Amount = coupon.Amount,
             });

        return affected != 0 ? true : false;
    }
    public async Task<bool> DeleteDiscount(string productName)
    {
        await using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var affected = await connection.ExecuteAsync
           (@$"DELETE FROM Coupon WHERE ProductName=@ProductName",
            new
            {
                ProductName = productName,
            });

        return affected != 0 ? true : false;
    }
}
