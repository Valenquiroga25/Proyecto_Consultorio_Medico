using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProyectoTurnos.Models.DTOs;

namespace ProyectoTurnos.Models;

public class Acceso
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idAcceso { get; set; }
    
    [Required (ErrorMessage = "El campo nombre de usuario es obligatorio!")] 
    [Display(Name = "nombre de usuario")]
    public string nombreUsuario { get; set; }
    
    [Required (ErrorMessage = "El campo contraseña es obligatorio!")] 
    [Display(Name = "contraseña")]
    public string contrasenia { get; set; }

    public Acceso(string nombreUsuario, string contrasenia)
    {
        this.nombreUsuario = nombreUsuario;
        this.contrasenia = contrasenia;
    }
}