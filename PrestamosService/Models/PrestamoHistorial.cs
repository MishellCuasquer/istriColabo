namespace PrestamosService.Models
{
    public class PrestamoHistorial
    {
        public long PrestamoHistorialId { get; set; }
        public long PrestamoId { get; set; }
        public PrestamoEstado Estado { get; set; }
        public DateTime CreadoEn { get; set; } = DateTime.UtcNow;
        public string? Nota { get; set; }
    }
}
