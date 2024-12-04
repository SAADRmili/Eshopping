using Catalog.Core.Entites;
using Catalog.Core.Repositories;
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
            .Find(x => x.Name.ToLower().Equals(name.ToLower()))
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await catalogContext
            .Products
            .Find(x => true)
            .ToListAsync();
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
