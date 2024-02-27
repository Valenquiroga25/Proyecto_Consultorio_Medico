using Microsoft.EntityFrameworkCore;
using ProyectoTurnos.Models;

namespace ProyectoTurnos.Data
{
    public class TurnoContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public TurnoContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"));
        }

        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Consulta> Consulta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aquí puedes configurar relaciones, índices, etc.
            modelBuilder.Entity<Turno>()
                .HasOne(t => t.paciente)
                .WithMany()
                .HasForeignKey(t => t.idPaciente);

            modelBuilder.Entity<Turno>()
                .HasOne(t => t.consulta)
                .WithMany()
                .HasForeignKey(t => t.idConsulta);
        }

    public DbSet<ProyectoTurnos.Models.Turno> Turno { get; set; } = default!;
    }
}