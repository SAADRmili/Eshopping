using Microsoft.EntityFrameworkCore;
using Ordering.Core.Common;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using System.Linq.Expressions;

namespace Ordering.Infrastructure.Repositories;
public class RespositoryBase<T>(OrderContext orderContext) : IAsyncRepository<T> where T : EntityBase
{

    public async Task<IReadOnlyList<T>> GetAllAsync()
    => await orderContext
        .Set<T>()
        .ToListAsync();

    public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    => await orderContext
        .Set<T>()
        .Where(predicate)
        .ToListAsync();

    public async Task<T> GetByIdAsync(int id)
    => await orderContext
        .Set<T>()
        .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<T> AddAsync(T entity)
    {
        orderContext
            .Set<T>()
            .Add(entity);
        await orderContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        var entitydeleted = await GetByIdAsync(entity.Id);
        if (entitydeleted is null) return;
        orderContext
            .Set<T>()
            .Remove(entitydeleted);
        await orderContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        var entityUpdated = await GetByIdAsync(entity.Id);
        if (entityUpdated is null) return;
        orderContext.Entry(entity).State = EntityState.Modified;
        await orderContext.SaveChangesAsync();
    }
}
