using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTurnos.Models;

public class Paciente
{
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idPaciente { get; set; }
    
    [Required]
    public string nombres { get; set; }
    
    [Required]
    public string apellidos { get; set; }

    [Key] [Required]
    public string documento { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime? fechaNacimiento { get; set; }
    
    [Required]
    public string? telefono { get; set; }

    [Required] 
    public string? codArea { get; set; }
    public string? direccion { get; set; }
    
    public string? correo { get; set; }

    public Paciente()
    {

    }

    public Paciente(string nombres, string apellidos, string documento, DateTime fechaNacimiento, string codArea, string telefono, string direccion, string correo)
    {
        this.nombres = nombres;
        this.apellidos = apellidos;
        this.documento = documento;
        this.fechaNacimiento = fechaNacimiento;
        this.codArea = codArea;
        this.telefono = telefono;
        this.direccion = direccion;
        this.correo = correo;
    }
}