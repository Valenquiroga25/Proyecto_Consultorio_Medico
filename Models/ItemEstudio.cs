using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTurnos.Models;

public class ItemEstudio
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idItem { get; set; }
    
    [Key]
    [Required]
    public string documentoPaciente { get; set; }
    
    [Key]
    [Required]
    public DateTime fecha { get; set; }

    public ItemEstudio(){}

    public ItemEstudio(string documentoPaciente, DateTime fecha)
    {
        this.documentoPaciente = documentoPaciente;
        this.fecha = fecha;
    }
}