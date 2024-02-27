using Microsoft.EntityFrameworkCore;

namespace ProyectoTurnos.Data
{
    public class ConsultaContext : DbContext
    {
        public ConsultaContext (DbContextOptions<ConsultaContext> options)
            : base(options)
        {
        }

        public DbSet<ProyectoTurnos.Models.Consulta> Consulta { get; set; } = default!;
    }
}