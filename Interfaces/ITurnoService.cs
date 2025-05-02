using Microsoft.AspNetCore.Mvc;

namespace ProyectoTurnos.Interfaces;
using ProyectoTurnos.Models;

public interface ITurnoService
{
    public void GenerarTurno(Turno turno);
    public List<Turno>? BuscarTurnos();
    public Turno? BuscarTurnoById(int idTurno);
    public void EditarTurno(Turno turno);
    public Turno? ObtenerTurnoByFecha(DateTime fecha);
}