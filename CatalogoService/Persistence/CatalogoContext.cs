using CatalogoService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CatalogoService.Persistence
{
    public class CatalogoContext : DbContext
    {
        public CatalogoContext(DbContextOptions<CatalogoContext> options)
            : base(options)
        {
        }
        public DbSet<Libro> Libro { get; set; }
        public DbSet<StockMovimiento> StockMovimiento { get; set; }
    }
}