using CatalogoService.Models;
using CatalogoService.Persistence;
using CatalogoService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogoService.Services
{
    public class StockMovimientoService : CrudService<StockMovimiento>, IStockMovimientoService
    {
        public StockMovimientoService(CatalogoContext ctx) : base(ctx) { }

        private readonly CatalogoContext _ctx;


        public async Task<bool> Reservar(string isbn, int cantidad, Guid idempotencyKey, string? origen = null, string? correlationId = null, CancellationToken ct = default)
        {
            var libro = await _ctx.Libro.FirstOrDefaultAsync(l => l.isbn == isbn, ct);
            if (libro == null)
                throw new Exception("El libro no existe en el catálogo.");

            if (libro.stock_disponible < cantidad)
                throw new Exception("No hay suficientes ejemplares disponibles.");

            var movimiento = new StockMovimiento
            {
                stock_movimiento_id = Guid.NewGuid(),
                isbn = isbn,
                cantidad = cantidad,
                tipo = TipoMovimiento.RESERVA,
                idempotency_Key = idempotencyKey,
                origen = origen,
                correlation_id = correlationId
            };

            _ctx.StockMovimiento.Add(movimiento);

            libro.stock_disponible -= cantidad;
            _ctx.Libro.Update(libro);

            await _ctx.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> Liberar(string isbn, int cantidad, Guid idempotencyKey, string? origen = null, string? correlationId = null, CancellationToken ct = default)
        {
            var libro = await _ctx.Libro.FirstOrDefaultAsync(l => l.isbn == isbn, ct);
            if (libro == null)
                throw new Exception("El libro no existe en el catálogo.");

            var movimiento = new StockMovimiento
            {
                stock_movimiento_id = Guid.NewGuid(),
                isbn = isbn,
                cantidad = cantidad,
                tipo = TipoMovimiento.LIBERACION,
                idempotency_Key = idempotencyKey,
                origen = origen,
                correlation_id = correlationId
            };

            _ctx.StockMovimiento.Add(movimiento);

            libro.stock_disponible += cantidad;
            _ctx.Libro.Update(libro);

            await _ctx.SaveChangesAsync(ct);
            return true;
        }
    }

}
