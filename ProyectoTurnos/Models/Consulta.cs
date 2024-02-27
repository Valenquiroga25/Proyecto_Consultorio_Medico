using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTurnos.Models;

public class Consulta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdConsulta { get; set; }

    [Required (ErrorMessage = "El campo Descripción es obligatorio!")] 
    [Display(Name = "Descripción")]
    public string descripcion { get; set; }
    
    [Required (ErrorMessage = "El campo Precio es obligatorio!")] 
    [Display(Name = "Precio")]
    public double precio { get; set; }
    
    public Consulta()
    {

    }
    
    public Consulta(String descripcion, double precio)
    {
        this.descripcion = descripcion;
        this.precio = precio;
    }
}