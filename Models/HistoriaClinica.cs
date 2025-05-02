using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTurnos.Models;

public class HistoriaClinica
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idHistoria { get; set; }

    [Required] 
    public Paciente? paciente { get; set; }
    
    public HistoriaClinica(){}

    public HistoriaClinica(Paciente paciente)
    {
        this.paciente = paciente;
    }
}