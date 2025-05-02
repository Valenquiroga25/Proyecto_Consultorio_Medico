using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Humanizer.DateTimeHumanizeStrategy;
using ProyectoTurnos.Models.DTOs;

namespace ProyectoTurnos.Models;

public class Paciente
{
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idPaciente { get; set; }
    
    [Required (ErrorMessage = "El campo Nombre es obligatorio!")] 
    [Display(Name = "Nombre completo")]
    public string nombreCompleto { get; set; }

    [Key] [Required(ErrorMessage = "El campo Documento es obligatorio!")] [Display(Name = "Documento")]
    public string documento { get; set; }
    
    [Display(Name = "Fecha de nacimiento")]
    [DataType(DataType.Date)]
    public DateTime? fechaNacimiento { get; set; }
    
    [Display(Name = "Obra social")]
    public string? obraSocial { get; set; }
    
    [Display(Name = "Teléfono")]
    public string? telefono { get; set; }
    
    [Display(Name = "Dirección")]
    public string? direccion { get; set; }
    
    [Display(Name = "Correo")]
    public string? correo { get; set; }

    public Paciente()
    {

    }

    public Paciente(string nombreCompleto, string documento, DateTime fechaNacimiento, string obraSocial, string telefono, string direccion, string correo)
    {
        this.nombreCompleto = nombreCompleto;
        this.documento = documento;
        this.fechaNacimiento = fechaNacimiento;
        this.obraSocial = obraSocial;
        this.telefono = telefono;
        this.direccion = direccion;
        this.correo = correo;
    }
}