﻿using Catalog.Core.Entites;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;
public class CatalogContext : ICatalogContext
{
    public IMongoCollection<Product> Products { get; }

    public IMongoCollection<ProductBrand> Brands { get; }

    public IMongoCollection<ProductType> Types { get; }

    public CatalogContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DataBaseName"));
        Brands = database.GetCollection<ProductBrand>(configuration.GetValue<string>("DatabaseSettings:BrandsCollection"));
        Types = database.GetCollection<ProductType>(configuration.GetValue<string>("DatabaseSettings:TypesCollection"));
        Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

        BrandContextSeed.seedData(Brands);
        TypesContextSeed.seedData(Types);
        ProductContextSeed.seedData(Products);

    }
}
