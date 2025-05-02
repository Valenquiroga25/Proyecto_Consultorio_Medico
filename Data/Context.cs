namespace ProyectoTurnos.Data;

using Microsoft.EntityFrameworkCore;
using ProyectoTurnos.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) {}
    public DbSet<Acceso> Acceso { get; set; } = default!;
    public DbSet<Paciente> Paciente { get; set; } = default!;
    public DbSet<Turno> Turno { get; set; } = default!;

    public DbSet<TurnoSlot> TurnoSlot { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuramos las relaciones si hacen falta
        modelBuilder.Entity<Turno>()
            .HasOne(t => t.paciente) // Indica que esta entidad (turno) está asociada a una sola entidad (paciente).
            .WithMany() // Indica que la entidad a la que se hace referencia (paciente) puede estar asociada a muchos de esta entidad (turno).
            .HasForeignKey(t => t.documento); //Indica que la clave foranea a la tabla pacientes es documento.

        modelBuilder.Entity<TurnoSlot>()
            .HasOne(t => t.turno)
            .WithMany()
            .HasForeignKey(t => t.idTurno);
    }
}

