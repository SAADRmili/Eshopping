using Catalog.Core.Entites;

namespace Catalog.Core.Repositories;
public interface ITypesRepository
{
    Task<IEnumerable<ProductType>> GetProductTypes();
}
