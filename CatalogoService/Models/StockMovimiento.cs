using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoService.Models
{
    public enum TipoMovimiento { RESERVA = 1, LIBERACION = 2, AJUSTE = 3 }
    [Table("stock_movimiento", Schema = "catalogo")]
    public class StockMovimiento
    {
        public Guid StockMovimientoId { get; set; }          
        public string Isbn { get; set; } = default!;         
        public int Cantidad { get; set; }                    
        public TipoMovimiento Tipo { get; set; }             
        public Guid IdempotencyKey { get; set; }             
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

        public string? Origen { get; set; }                  
        public string? CorrelationId { get; set; }           
    }
}
