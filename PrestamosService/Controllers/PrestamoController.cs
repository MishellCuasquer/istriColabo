using Microsoft.AspNetCore.Mvc;
using PrestamosService.Services.Interfaces;

namespace PrestamosService.Controllers
{
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;
        public PrestamoController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }
    }
}