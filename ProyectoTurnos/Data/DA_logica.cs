
using ProyectoTurnos.Models;

namespace ProyectoTurnos.Model;

public class DA_logica
{
    public List<Acceso> listarUsuarios()
    {
        return new List<Acceso>
        {
            new Acceso { nombreUsuario = "vale", contraseña = "jaja" }
        };
    }

    public bool validarUsuario(String nombreUsuario, String contraseña)
    { 
        Acceso usuarioValido = (listarUsuarios().Where(item => item.nombreUsuario == nombreUsuario && item.contraseña == contraseña).FirstOrDefault());

        return (usuarioValido != null);
    }
}