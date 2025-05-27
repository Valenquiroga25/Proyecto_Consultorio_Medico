using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTurnos.Models;

public class Historia
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idHistoria { get; set; }

    [Key]
    [Required] 
    public string documentoPaciente { get; set; }

    [ForeignKey("documentoPaciente")]
    public Paciente paciente { get; set; }
    
    [Required]
    public string descripcion { get; set; }
    
    public Historia(){}


    
    public Historia(Paciente paciente, string descripcion)
    {
        this.paciente = paciente;
        documentoPaciente = paciente.documento;
        this.descripcion = descripcion;
    }
}