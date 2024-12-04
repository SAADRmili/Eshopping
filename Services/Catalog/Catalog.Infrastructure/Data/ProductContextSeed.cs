using Catalog.Core.Entites;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;
public static class ProductContextSeed
{
    public static void seedData(IMongoCollection<Product> productCollection)
    {
        bool checkProducts = productCollection.Find(x => true).Any();
        var path = Path.Combine("Data", "SeedData", "products.json");
        if (!checkProducts)
        {
            var productsData = File.ReadAllText(path);
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products is not null)
            {
                foreach (var item in products)
                {
                    productCollection.InsertOneAsync(item);
                }
            }
        }
    }
}
