namespace ProyectoTurnos.Interfaces;
using ProyectoTurnos.Models;

public interface IAccesoService
{
    public void Registrarse(Acceso acceso);
    public void CambiarContrasenia(Acceso acceso);
    public Acceso? BuscarUsuario(string usuario);
    public bool ValidarUsuario(Acceso acceso);
}