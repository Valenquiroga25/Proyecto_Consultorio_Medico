using ProyectoTurnos.Interfaces;
using ProyectoTurnos.Data;
using ProyectoTurnos.Models;

namespace ProyectoTurnos.Services;

public class AccesoService : IAccesoService
{
    private Context context;

    public AccesoService(Context context)
    {
        this.context = context;
    }
    
    public void Registrarse(Acceso acceso)
    {
        try
        {
            Acceso? usuario = BuscarUsuario(acceso.nombreUsuario);
            if (usuario != null)
            {
                Console.WriteLine("El usuario ya existe en el sistema!");
                return;
            }

            context.Add(acceso);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error con el acceso: " + e.Message);
        }
    }

    public void CambiarContrasenia(Acceso acceso)
    {
        try
        {
            Acceso? usuarioBuscado = BuscarUsuario(acceso.nombreUsuario);
            if (usuarioBuscado == null)
            {
                Console.WriteLine("El usuario no se encuentra registrado en el sistema!");
                return;
            }

            usuarioBuscado.contrasenia = acceso.contrasenia;

            context.SaveChanges();
            Console.WriteLine("La contraseña ha sido actualizada con éxito!");
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error en el cambio de contraseña: " + e.Message);
        }
    }

    public Acceso? BuscarUsuario(string usr)
    {
        try
        {
            Acceso? usuario = context.Acceso.SingleOrDefault(u => u.nombreUsuario == usr);
            return usuario;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error con el acceso: " + e.Message);
            return null;
        }
    }

    public bool ValidarUsuario(Acceso acceso)
    {
        try
        {
            Acceso? usuarioValido = context.Acceso.SingleOrDefault(item =>
                item.nombreUsuario == acceso.nombreUsuario && item.contrasenia == acceso.contrasenia);
            
            return (usuarioValido != null);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrio un error al validar el usuario: " + e.Message);
            return false;
        }
    }
}