using Microsoft.AspNetCore.Mvc;
using PrestamosService.Services.Interfaces;

namespace PrestamosService.Controllers
{
    [ApiController]
    [Route("api/prestamo-historial")]
    public class PrestamoHistorialController : ControllerBase
    {
        private readonly IPrestamoHistorialService _prestamoHistorialService;
        public PrestamoHistorialController(IPrestamoHistorialService prestamoHistorialService)
        {
            _prestamoHistorialService = prestamoHistorialService;
        }
    }
}