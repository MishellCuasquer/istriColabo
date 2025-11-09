using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoService.Models
{
    [Table("libro", Schema = "catalogo")]
    public class Libro
    {
        [Key]
        public string isbn { get; set; } = default!;
        public string titulo { get; set; } = default!;
        public string autor { get; set; } = default!;
        public string categoria { get; set; } = default!;
        public int stock_disponible { get; set; }
    }
}
