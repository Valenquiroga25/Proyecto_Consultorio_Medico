using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTurnos.Models;

public class Estudio
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idEstudio { get; set; }
    
    [Key]
    [Required]
    public string nombre { get; set; }
    
    [Key]
    [Required]
    public string documentoPaciente { get; set; }
    
    [Key]
    [Required]
    public DateTime fecha { get; set; }
    
    public Estudio(){}

    public Estudio(string nombre, string documentoPaciente, DateTime fecha)
    {
        this.nombre = nombre;
        this.documentoPaciente = documentoPaciente;
        this.fecha = fecha;
    }
}