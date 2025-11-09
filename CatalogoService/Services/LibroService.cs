using CatalogoService.Models;
using CatalogoService.Persistence;
using CatalogoService.Services.Interfaces;

namespace CatalogoService.Services
{
    public class LibroService : CrudService<Libro>, ILibroService
    {
        public LibroService(CatalogoContext ctx) : base(ctx) { }

    }
}