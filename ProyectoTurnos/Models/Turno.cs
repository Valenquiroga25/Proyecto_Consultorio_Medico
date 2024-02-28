using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTurnos.Models;

public class Turno
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdTurno { get; set; }
    
    [Required (ErrorMessage = "El campo Fecha es obligatorio!")]  
    [Display(Name = "Fecha")]
    [DataType(DataType.Date)]
    public DateTime? fecha { get; set; }
    
    [Required (ErrorMessage = "El campo Hora es obligatorio!")]  
    [Display(Name = "Hora")]
    [DataType(DataType.Time)]
    public TimeSpan? hora { get; set; }
    
    [Required (ErrorMessage = "El campo Descripción es obligatorio!")] 
    [Display(Name = "Descripción")]
    public String descripcion { get; set; }
    
    [ForeignKey("idPaciente")]
    [Required (ErrorMessage = "El campo paciente es obligatorio!")] 
    [Display(Name = "Id del paciente")]
    public int? idPaciente { get; set; } // Identificador del usuario asociado

    [ForeignKey("idConsulta")]
    [Required (ErrorMessage = "El campo consulta es obligatorio!")] 
    [Display(Name = "Id de la consulta")]
    public int? idConsulta { get; set; } // Identificador del usuario asociado
    
    [Display(Name = "Paciente")]
    public Paciente paciente { get; set; }
    
    [Display(Name = "Consulta")]
    public Consulta consulta { get; set; }

    public Turno()
    {
        
    }
    
    public Turno(DateTime fecha, String descripcion, int idPaciente, int idConsulta)
    {
        this.fecha = fecha;
        this.descripcion = descripcion;
        this.idPaciente = idPaciente;
        this.idConsulta = idConsulta;
    }
}