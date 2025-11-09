using CatalogoService.Persistence;
using CatalogoService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CatalogoService.Services
{
    public class CrudService<T> : ICrudService<T> where T : class
    {
        protected readonly CatalogoContext _ctx;
        protected readonly DbSet<T> _set;

        public CrudService(CatalogoContext ctx)
        {
            _ctx = ctx;
            _set = ctx.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(CancellationToken ct = default, params object[] keyValues)
            => await _set.FindAsync(keyValues, ct);

        public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
            => await _set.AsNoTracking().ToListAsync(ct);

        public virtual async Task<T> AddAsync(T entity, CancellationToken ct = default)
        {
            await _set.AddAsync(entity, ct);
            return entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
            => await _set.AddRangeAsync(entities, ct);

        public virtual Task<T> UpdateAsync(T entity, CancellationToken ct = default)
        {
            _set.Update(entity);
            return Task.FromResult(entity);
        }

        public virtual Task DeleteAsync(T entity, CancellationToken ct = default)
        {
            _set.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual async Task DeleteByIdAsync(CancellationToken ct = default, params object[] keyValues)
        {
            var entity = await GetByIdAsync(ct, keyValues);
            if (entity is not null)
            {
                _set.Remove(entity);
            }
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken ct = default)
            => _ctx.SaveChangesAsync(ct);
    }
}
