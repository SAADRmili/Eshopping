using Catalog.Core.Entites;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;
public static class TypesContextSeed
{
    public static void seedData(IMongoCollection<ProductType> typeCollection)
    {
        bool checkTypes = typeCollection.Find(x => true).Any();
        var path = Path.Combine("Data", "SeedData", "types.json");
        if (!checkTypes)
        {
            var typesData = File.ReadAllText(path);
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            if (types is not null)
            {
                foreach (var item in types)
                {
                    typeCollection.InsertOneAsync(item);
                }
            }
        }
    }
}
