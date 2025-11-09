namespace PrestamosService.Services.Interfaces
{
    public interface ICrudService<T> where T : class
    {
        Task<T?> GetByIdAsync(CancellationToken ct = default, params object[] keyValues);
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);
        Task<T> AddAsync(T entity, CancellationToken ct = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);
        Task<T> UpdateAsync(T entity, CancellationToken ct = default);
        Task DeleteAsync(T entity, CancellationToken ct = default);
        Task DeleteByIdAsync(CancellationToken ct = default, params object[] keyValues);
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
