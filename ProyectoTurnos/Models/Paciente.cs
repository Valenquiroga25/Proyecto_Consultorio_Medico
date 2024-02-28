using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTurnos.Models;

public class Paciente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idPaciente { get; set; }
    
    [Required (ErrorMessage = "El campo Nombre es obligatorio!")] 
    [Display(Name = "Nombre completo")]
    public String nombreCompleto { get; set; }
    
    [Required(ErrorMessage = "El campo Documento es obligatorio!")]
    [Display(Name = "Documento")]
    public int? documento { get; set; }

    [Required (ErrorMessage = "El campo Obra social es obligatorio!")]  
    [Display(Name = "Obra social")]
    public String obraSocial { get; set; }
    
    [Required (ErrorMessage = "El campo Fecha es obligatorio!")]  
    [Display(Name = "Fecha de nacimiento")]
    [DataType(DataType.Date)]
    public DateTime? fechaNacimiento { get; set; }
    
    [Required (ErrorMessage = "El campo Teléfono es obligatorio!")]  
    [Display(Name = "Teléfono")]
    public String telefono { get; set; }

    public Paciente()
    {

    }

    public Paciente(String nombreCompleto, String obraSocial, String telefono, DateTime fechaNacimiento, int documento)
    {
        this.nombreCompleto = nombreCompleto;
        this.obraSocial = obraSocial;
        this.telefono = telefono;
        this.fechaNacimiento = fechaNacimiento;
        this.documento = documento;
    }
}