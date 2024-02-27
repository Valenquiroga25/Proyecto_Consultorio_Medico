using Microsoft.EntityFrameworkCore;

namespace ProyectoTurnos.Data
{
    public class AccesoContext : DbContext
    {
        public AccesoContext (DbContextOptions<AccesoContext> options)
            : base(options)
        {
        }

        public DbSet<ProyectoTurnos.Models.Acceso> Acceso { get; set; } = default!;
    }
}