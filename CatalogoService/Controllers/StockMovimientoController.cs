using Microsoft.AspNetCore.Mvc;

namespace CatalogoService.Controllers
{
    [ApiController]
    [Route("api/stock-movimiento")]

    public class StockMovimientoController
    {
        private readonly IStockMovimientoService _stockService;
        public StockMovimientoController(IStockMovimientoService stockService)
        {
            _stockService = stockService;
        }


        [HttpPost("reservar")]
        public async Task<ActionResult> Reservar([FromBody] stockMovimiento req, CancellationToken ct)
        {
           var resultado = await _stockService.Reservar(req.isbn, req.cantidad, req.idempotency_Key, req.origen, req.correlation_id, ct);
              if (resultado)
              {
                return Ok();
              }
              else
              {
                return BadRequest("No se pudo reservar el stock.");
            }
        }


        [HttpPost("liberar")]
        public async Task<ActionResult> Liberar([FromBody] stockMovimiento req, CancellationToken ct)
        {

              var resultado = await _stockService.Liberar(req.isbn, req.cantidad, req.idempotency_Key, req.origen, req.correlation_id, ct);
                  if (resultado)
                  {
                 return Ok();
                  }
                  else
                  {
                 return BadRequest("No se pudo liberar el stock.");
            }

        }
    }
}
