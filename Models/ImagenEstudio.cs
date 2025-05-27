using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTurnos.Models;

public class ImagenEstudio
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idImagen { get; set; }
    
    [Required]
    public string nombreEstudio { get; set; }
        
    [Required]
    public string documentoPacienteEstudio { get; set; }
            
    [Required]
    public DateTime fecha { get; set; }
    
    [Required]
    public string titulo { get; set; }
    
    [Required]
    public byte[] imagen { get; set; }

    public ImagenEstudio() {}

    public ImagenEstudio(string nombreEstudio, string documentoPacienteEstudio,DateTime fecha, string titulo, byte[] imagen)
    {
        this.nombreEstudio = nombreEstudio;
        this.documentoPacienteEstudio = documentoPacienteEstudio;
        this.fecha = fecha;
        this.titulo = titulo;
        this.imagen = imagen;
    }
}