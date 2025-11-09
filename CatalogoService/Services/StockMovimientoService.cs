using CatalogoService.Models;
using CatalogoService.Persistence;
using CatalogoService.Services.Interfaces;

namespace CatalogoService.Services
{
    public class StockMovimientoService : CrudService<StockMovimiento>, IStockMovimientoService
    {
        public StockMovimientoService(CatalogoContext ctx) : base(ctx) { }

        private readonly CatalogoContext _ctx;


        public async Task<bool> Reservar(string isbn, int cantidad, Guid idempotencyKey, string? origen = null, string? correlationId = null, CancellationToken ct = default)
        {
            var libro = await _libros.FirstOrDefaultAsync(l => l.ISBN == isbn, ct);
            if (libro == null)
                throw new Exception("El libro no existe en el catálogo.");

            if (libro.CantidadDisponible < cantidad)
                throw new Exception("No hay suficientes ejemplares disponibles.");

            // Registrar movimiento
            var movimiento = new MovimientoLibro
            {
                MovimientoId = Guid.NewGuid(),
                ISBN = isbn,
                Cantidad = cantidad,
                Tipo = TipoMovimiento.RESERVA,
                IdempotencyKey = idempotencyKey,
                Origen = origen,
                CorrelationId = correlationId
            };

            _movimientos.Add(movimiento);

            // Actualizar disponibilidad
            libro.CantidadDisponible -= cantidad;
            _libros.Update(libro);

            await _ctx.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> Liberar(string isbn, int cantidad, Guid idempotencyKey, string? origen = null, string? correlationId = null, CancellationToken ct = default)
        {
            var libro = await _libros.FirstOrDefaultAsync(l => l.ISBN == isbn, ct);
            if (libro == null)
                throw new Exception("El libro no existe en el catálogo.");

            // Registrar movimiento
            var movimiento = new MovimientoLibro
            {
                MovimientoId = Guid.NewGuid(),
                ISBN = isbn,
                Cantidad = cantidad,
                Tipo = TipoMovimiento.LIBERACION,
                IdempotencyKey = idempotencyKey,
                Origen = origen,
                CorrelationId = correlationId
            };

            _movimientos.Add(movimiento);

            // Actualizar disponibilidad
            libro.CantidadDisponible += cantidad;
            _libros.Update(libro);

            await _ctx.SaveChangesAsync(ct);
            return true;
        }
    }

}
}
