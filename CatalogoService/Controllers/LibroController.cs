using CatalogoService.Models;
using CatalogoService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoService.Controllers
{
    [ApiController]
    [Route("api/libro")]
    public class LibroController : ControllerBase
    {
        private readonly ILibroService _libroService;
        public LibroController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetAll(CancellationToken ct)
       => Ok(await _libroService.GetAllAsync(ct));

        [HttpGet("{isbn}", Name = "GetLibroByIsbn")]
        public async Task<ActionResult<Libro>> GetById(string isbn, CancellationToken ct)
        {
            var libros = await _libroService.GetByIdAsync(ct, isbn);
            return libros is null ? NotFound() : Ok(libros);
        }

        [HttpPost]
        public async Task<ActionResult<Libro>> Create(Libro libro, CancellationToken ct)
        {
            await _libroService.AddAsync(libro, ct);
            await _libroService.SaveChangesAsync(ct);
            return CreatedAtRoute("GetLibroByIsbn", new { isbn = libro.isbn }, libro);
        }
    }
}