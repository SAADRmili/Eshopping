using Catalog.Core.Entites;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;
public class ProductRepository(ICatalogContext catalogContext) : IProductRepository, IBrandRepository, ITypesRepository
{
    public async Task<Product> CreateProduct(Product product)
    {
        await catalogContext
            .Products
            .InsertOneAsync(product);
        return product;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var deletedProduct = await catalogContext
            .Products
            .DeleteOneAsync(x => x.Id == id);
        return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateProduct = await catalogContext
            .Products
            .ReplaceOneAsync(p => p.Id == product.Id, product);
        return updateProduct.IsAcknowledged && updateProduct.ModifiedCount > 0;
    }

    public async Task<Product> GetProduct(string id)
    {
        return await catalogContext
            .Products
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<Product>> GetAllProductByBrand(string name)
    {
        return await catalogContext
            .Products
            .Find(x => x.Brands.Name.ToLower() == name.ToLower())
            .ToListAsync();

    }

    public async Task<IEnumerable<Product>> GetAllProductByName(string name)
    {
        return await catalogContext
            .Products
            .Find(x => x.Name.ToLower() == name.ToLower())
            .ToListAsync();
    }

    public async Task<Pagination<Product>> GetAllProducts(CatalogSpecParams catalogSpecParams)
    {
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;
        if (!string.IsNullOrEmpty(catalogSpecParams.Search))
        {
            filter &= builder.Where(p => p.Name.ToLower().Contains(catalogSpecParams.Search.ToLower()));
        }
        if (!string.IsNullOrEmpty(catalogSpecParams.BrandId))
        {
            var brandFilter = builder.Eq(p => p.Brands.Id, catalogSpecParams.BrandId);
            filter &= brandFilter;
        }
        if (!string.IsNullOrEmpty(catalogSpecParams.TypeId))
        {
            var typesFilter = builder.Eq(p => p.Types.Id, catalogSpecParams.TypeId);
            filter &= typesFilter;
        }

        var totalItem = await catalogContext.Products.CountDocumentsAsync(filter);
        var data = await DataFilter(catalogSpecParams, filter);

        return new Pagination<Product>(
            catalogSpecParams.PageIndex,
            catalogSpecParams.PageSize,
            (int)totalItem,
            data);
    }

    private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
    {
        var sortDefn = Builders<Product>.Sort.Ascending("Name");
        if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
        {
            sortDefn = catalogSpecParams.Sort switch
            {
                "priceAsc" => Builders<Product>.Sort.Ascending(p => p.Price),
                "priceDesc" => Builders<Product>.Sort.Descending(p => p.Price),
                _ => Builders<Product>.Sort.Ascending(p => p.Name),
            };
        }

        return await catalogContext.Products
                                  .Find(filter)
                                  .Sort(sortDefn)
                                  .Skip((catalogSpecParams.PageIndex - 1) * catalogSpecParams.PageSize)
                                  .Limit(catalogSpecParams.PageSize).ToListAsync();
    }

    public async Task<IEnumerable<ProductBrand>> GetBrands()
    {
        return await catalogContext
            .Brands
            .Find(x => true)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductType>> GetProductTypes()
    {
        return await catalogContext
            .Types
            .Find(x => true)
            .ToListAsync();
    }

}
