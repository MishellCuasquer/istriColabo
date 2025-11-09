using CatalogoService.Persistence;
using PrestamosService.Models;
using PrestamosService.Services.Interfaces;

namespace PrestamosService.Services
{
    public class PrestamoHistorialService : CrudService<PrestamoHistorial>, IPrestamoHistorialService
    {
        public PrestamoHistorialService(PrestamosContext ctx) : base(ctx) { }

    }
}