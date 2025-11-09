using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoService.Models
{
    public enum TipoMovimiento { RESERVA = 1, LIBERACION = 2, AJUSTE = 3 }
    [Table("stock_movimiento", Schema = "catalogo")]
    public class StockMovimiento
    {
        [Key]
        public Guid stock_movimiento_id { get; set; }          
        public string isbn { get; set; } = default!;         
        public int cantidad { get; set; }                    
        public TipoMovimiento tipo { get; set; }             
        public Guid idempotency_Key { get; set; }             
        public DateTime creado_en { get; set; } = DateTime.UtcNow;

        public string? origen { get; set; }                  
        public string? correlation_id { get; set; }           
    }
}
