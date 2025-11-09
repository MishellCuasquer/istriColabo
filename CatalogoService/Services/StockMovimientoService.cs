using CatalogoService.Models;
using CatalogoService.Persistence;
using CatalogoService.Services.Interfaces;

namespace CatalogoService.Services
{
    public class StockMovimientoService : CrudService<StockMovimiento>, IStockMovimientoService
    {
        public StockMovimientoService(CatalogoContext ctx) : base(ctx) { }
    }
}
