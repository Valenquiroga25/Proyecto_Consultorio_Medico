namespace ProyectoTurnos.Data;

using Microsoft.EntityFrameworkCore;
using ProyectoTurnos.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) {}
    public DbSet<Acceso> Acceso { get; set; } = default!;
    public DbSet<Paciente> Paciente { get; set; } = default!;
    public DbSet<Historia> Historia { get; set; } = default!;
    public DbSet<ItemEstudio> ItemEstudio { get; set; } = default!;
    public DbSet<Estudio> Estudio { get; set; } = default!;

    public DbSet<ImagenEstudio> ImagenEstudio { get; set; } = default!;
    public DbSet<Turno> Turno { get; set; } = default!;
    public DbSet<TurnoSlot> TurnoSlot { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        try
        {
            // Configuramos las relaciones si hacen falta
            modelBuilder.Entity<Historia>()
                .HasOne(h => h.paciente)
                .WithOne(); 
            
            modelBuilder.Entity<ItemEstudio>()
                .HasOne<Historia>() // Se hace de esta manera cuando la clase contiene solo el campo referente a la PK de la otra clase, y no un objeto.
                .WithMany()
                .HasForeignKey(i => i.documentoPaciente); 
            
            modelBuilder.Entity<Estudio>()
                .HasOne<ItemEstudio>() 
                .WithMany()
                .HasForeignKey(e => e.documentoPaciente);
            
            modelBuilder.Entity<ImagenEstudio>()
                .HasOne<Estudio>() 
                .WithMany()
                .HasForeignKey(i => i.documentoPacienteEstudio);
            
            modelBuilder.Entity<Turno>()
                .HasOne(t => t.paciente) // Indica que esta entidad (turno) está asociada a una sola entidad (paciente).
                .WithMany() // Indica que la entidad a la que se hace referencia (paciente) puede estar asociada a muchos de esta entidad (turno).
                .HasForeignKey(t => t.documento); //Indica que la clave foranea a la tabla pacientes es documento.

            modelBuilder.Entity<TurnoSlot>()
                .HasOne(t => t.turno)
                .WithMany()
                .HasForeignKey(t => t.idTurno);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al querer cargar datos en la BD: " + e.Message);
            throw;
        }
    }
}

