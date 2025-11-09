using CatalogoService.Models;
using CatalogoService.Persistence;

namespace CatalogoService.Services.Interfaces
{
    public interface IStockMovimientoService : ICrudService<StockMovimiento>
    {
        Task<bool> Reservar(string isbn, int cantidad, Guid idempotencyKey, string? origen = null, string? correlationId = null, CancellationToken ct = default);

        Task<bool> Liberar(string isbn, int cantidad, Guid idempotencyKey, string? origen = null, string? correlationId = null, CancellationToken ct = default);
    }
}