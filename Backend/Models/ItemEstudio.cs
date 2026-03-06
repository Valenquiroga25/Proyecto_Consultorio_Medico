using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTurnos.Models;

public class ItemEstudio
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idItem { get; set; }
    

    [Required]
    public string documentoPaciente { get; set; }
    

    [Required]
    public DateTime fecha { get; set; }
    
    public List<Estudio> estudios { get; set; } = new();

    public ItemEstudio(){}

    public ItemEstudio(string documentoPaciente, DateTime fecha)
    {
        this.documentoPaciente = documentoPaciente;
        this.fecha = fecha;
    }
    
    public ItemEstudio(string documentoPaciente, DateTime fecha, List<Estudio> estudios)
    {
        this.documentoPaciente = documentoPaciente;
        this.fecha = fecha;
        this.estudios = estudios;
    }
}