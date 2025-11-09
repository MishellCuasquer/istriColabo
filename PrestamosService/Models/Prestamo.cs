namespace PrestamosService.Models
{
    public enum PrestamoEstado { Abierto = 1, Cerrado = 2, Cancelado = 3 }

    public class Prestamo
    {
        public long id { get; set; }                 
        public string usuario_id { get; set; } = default!;    
        public string isbn { get; set; } = default!;         
        public DateTime fecha_prestamo { get; set; } = DateTime.UtcNow;
        public DateTime? fecha_devolucion { get; set; }
        public PrestamoEstado estado { get; set; } = PrestamoEstado.Abierto;

        public Guid? ReservaStockKey { get; set; }          
        public Guid? LiberacionStockKey { get; set; }       
    }
}
