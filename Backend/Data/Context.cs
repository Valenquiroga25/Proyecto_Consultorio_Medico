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

            // PK compuesta de ItemEstudio (documentoPaciente, fechaItem)
            modelBuilder.Entity<ItemEstudio>().HasKey(ie => new { ie.documentoPaciente, ie.fecha });
            modelBuilder.Entity<ItemEstudio>()
                .HasOne<Historia>() // Se hace de esta manera cuando la clase contiene solo el campo referente a la PK de la otra clase, y no un objeto.
                .WithMany()
                .HasForeignKey(i => i.documentoPaciente);

            // Hay dos de Estudio porque uno es para indicar la clave compuesta, y otro para indicar el resto de los campos.
            modelBuilder.Entity<Estudio>().HasKey(e => new { e.nombre, e.documentoPaciente, e.fecha });
            modelBuilder.Entity<Estudio>()
                .HasOne<ItemEstudio>()
                .WithMany(i => i.estudios)
                .HasForeignKey(e => new { e.documentoPaciente, e.fecha })
                .HasPrincipalKey(i => new { i.documentoPaciente, i.fecha });
            
            modelBuilder.Entity<ImagenEstudio>()
                .HasOne<Estudio>() 
                .WithMany()
                .HasForeignKey(i => new {i.nombreEstudio, i.documentoPacienteEstudio, i.fecha});
            
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

