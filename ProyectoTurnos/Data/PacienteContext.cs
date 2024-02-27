
using Microsoft.EntityFrameworkCore;

namespace ProyectoTurnos.Data
{
    public class PacienteContext : DbContext
    {
        public PacienteContext (DbContextOptions<PacienteContext> options)
            : base(options)
        {
        }

        public DbSet<ProyectoTurnos.Models.Paciente> Paciente { get; set; } = default!;
    }
}
