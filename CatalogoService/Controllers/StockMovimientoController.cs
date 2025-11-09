using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using CatalogoService.Services.Interfaces;  
using CatalogoService.Models;               

namespace CatalogoService.Controllers
{
    [ApiController]
    [Route("api/stock-movimiento")]
    public class StockMovimientoController : ControllerBase
    {
        private readonly IStockMovimientoService _stockService;

        public StockMovimientoController(IStockMovimientoService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost("reservar")]
        public async Task<ActionResult> Reservar([FromBody] StockMovimiento req, CancellationToken ct)
        {
            var resultado = await _stockService.Reservar(
                req.isbn,
                req.cantidad,
                req.idempotency_Key,
                req.origen,
                req.correlation_id,
                ct
            );

            if (resultado)
                return Ok(new { message = "Stock reservado correctamente." });

            return BadRequest("No se pudo reservar el stock.");
        }

        [HttpPost("liberar")]
        public async Task<ActionResult> Liberar([FromBody] StockMovimiento req, CancellationToken ct)
        {
            var resultado = await _stockService.Liberar(
                req.isbn,
                req.cantidad,
                req.idempotency_Key,
                req.origen,
                req.correlation_id,
                ct
            );

            if (resultado)
                return Ok(new { message = "Stock liberado correctamente." });

            return BadRequest("No se pudo liberar el stock.");
        }
    }
}
