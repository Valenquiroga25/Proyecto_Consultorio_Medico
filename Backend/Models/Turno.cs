using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Humanizer.DateTimeHumanizeStrategy;

namespace ProyectoTurnos.Models;

public class Turno
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idTurno { get; set; }

    [Display(Name = "Paciente")] 
    [ForeignKey("documento")]
    public Paciente paciente { get; set; }
    
    [Required]
    public string documento { get;set;}
    
    [Required (ErrorMessage = "El campo Fecha es obligatorio!")]  
    [Display(Name = "Fecha")]
    [DataType(DataType.Date)]
    public DateTime fecha { get; set; }
    
    [Display(Name = "Descripción")]
    public string descripcion { get; set; }
    
    [Display(Name = "Estado")]
    public string estado { get; set; }
    
    public Turno()
    {
        
    }
    
    public Turno(Paciente paciente, DateTime fecha, string descripcion)
    {
        this.paciente = paciente;
        this.documento = paciente.documento;
        this.fecha = fecha;
        this.descripcion = descripcion;
        this.estado = "Ocupado";
    }
}