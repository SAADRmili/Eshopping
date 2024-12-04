using Catalog.Core.Entites;

namespace Catalog.Core.Repositories;
public interface IProductRepository
{
    Task<Product> CreateProduct(Product product);
    Task<bool> DeleteProduct(string id);
    Task<bool> UpdateProduct(Product product);
    Task<Product> GetProduct(string id);
    Task<IEnumerable<Product>> GetAllProducts();
    Task<IEnumerable<Product>> GetAllProductByName(string name);
    Task<IEnumerable<Product>> GetAllProductByBrand(string name);
}
