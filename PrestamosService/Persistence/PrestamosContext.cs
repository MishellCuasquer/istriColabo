using Microsoft.EntityFrameworkCore;
using PrestamosService.Models;
using System.Collections.Generic;

namespace CatalogoService.Persistence
{
    public class PrestamosContext : DbContext
    {
        public PrestamosContext(DbContextOptions<PrestamosContext> options)
            : base(options)
        {
        }
        public DbSet<Prestamo> Prestamo { get; set; }
        public DbSet<PrestamoHistorial> PrestamoHistorial { get; set; }
    }
}