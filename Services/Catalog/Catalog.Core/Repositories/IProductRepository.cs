using Catalog.Core.Entites;
using Catalog.Core.Specs;

namespace Catalog.Core.Repositories;
public interface IProductRepository
{
    Task<Product> CreateProduct(Product product);
    Task<bool> DeleteProduct(string id);
    Task<bool> UpdateProduct(Product product);
    Task<Product> GetProduct(string id);
    Task<Pagination<Product>> GetAllProducts(CatalogSpecParams catalogSpecParams);
    Task<IEnumerable<Product>> GetAllProductByName(string name);
    Task<IEnumerable<Product>> GetAllProductByBrand(string name);
}
