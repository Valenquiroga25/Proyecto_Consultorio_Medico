using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTurnos.Models;

public class ItemHistoriaClinica
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idItem { get; set; }
    
    [Required]
    public int idHistoria { get; set; }
    
    [ForeignKey("idHistoria")]                 // Le estamos diciendo "el atributo de esta clae 'idHistoria' es la clave foranea en la tabla"
    public HistoriaClinica? historia { get; set; }  // EF la mapea a la columna idHistoria de la tabla ITEM_HISTORIA, y de ahí enlaza con HISTORIA_CLINICA.idHistoria.
    
    public byte[]? imagen { get; set; }
    
    public ItemHistoriaClinica(){}

    public ItemHistoriaClinica(HistoriaClinica historia,byte[]? imagen)
    {
        this.historia = historia;
        idHistoria = historia.idHistoria;
        if (imagen != null)
            this.imagen = imagen;
    }
}