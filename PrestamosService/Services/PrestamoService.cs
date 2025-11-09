using CatalogoService.Persistence;
using PrestamosService.Models;
using PrestamosService.Services.Interfaces;

namespace PrestamosService.Services
{
    public class PrestamoService : CrudService<Prestamo>, IPrestamoService
    {
        public PrestamoService(PrestamosContext ctx) : base(ctx) { }

    }
}