using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTurnos.Models;

public class Acceso
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idAcceso { get; set; }
    
    [Required (ErrorMessage = "El campo nombre de usuario es obligatorio!")] 
    [Display(Name = "nombre de usuario")]
    public String nombreUsuario { get; set; }
    
    [Required (ErrorMessage = "El campo contraseña es obligatorio!")] 
    [Display(Name = "contraseña")]
    public String contraseña { get; set; }

    public Acceso()
    {
        
    }

    public Acceso(String nombreUsuario, String contraseña)
    {
        this.nombreUsuario = nombreUsuario;
        this.contraseña = contraseña;
    }
}