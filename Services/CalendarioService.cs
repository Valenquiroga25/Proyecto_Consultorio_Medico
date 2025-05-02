using System.Runtime.InteropServices.JavaScript;
using ProyectoTurnos.Data;
using ProyectoTurnos.Models;

namespace ProyectoTurnos.Services;

public class CalendarioService
{
    private Context context;
    
    public CalendarioService(Context context)
    {
        this.context = context;
    }

    public void InicializarAnio(int anio, List<TimeSpan> horarios)
    {
        try
        {
            DateTime inicio = new DateTime(anio, 1, 1);
            DateTime fin = new DateTime(anio, 12, 31);

            for (DateTime dia = inicio; dia <= fin; dia = dia.AddDays(1))
            {
                InicializarDia(dia, horarios);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al inicializar el año del calendario: " + e.Message);
        }
    }

    public void InicializarDia(DateTime fecha, List<TimeSpan> horarios)
    {
        try
        {
            for (int i = 0; i < horarios.Count; i++)
            {
                context.TurnoSlot.Add(new TurnoSlot(fecha, horarios[i],null));
            }

            context.SaveChanges();
        }catch(Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al inicializar el dia " + fecha.Date + " del calendario: " + e.Message);
        }
    }

    public void AsignarTurno(DateTime fecha, TimeSpan hora, Turno turno)
    {
        try
        {
            if (context.TurnoSlot.Any(t => t.fecha == fecha && t.hora == hora && t.turno != null))
            {
                Console.WriteLine("Ya existe un turno en esa fecha y esa hora. No es posible asignar el turno!");
                return;
            }

            TurnoSlot turnoBuscado = context.TurnoSlot.SingleOrDefault(t => t.fecha == fecha && t.hora == hora);

            turnoBuscado.turno = turno;
            turnoBuscado.idTurno = turno.idTurno;

            context.SaveChanges();
            Console.WriteLine("Turno asignado con éxito!");
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al asignar un turno: " + e.Message);
        }
    }

    public List<Turno>? ObtenerTurnosDelDia(DateTime fecha)
    {
        try
        {
            List<Turno> turnosDelDia = context.Turno.Where(t => t.fecha == fecha).ToList();
            return turnosDelDia;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al obtener los turnos del dia " + fecha + ": " + e.Message);
            return null;
        }    
    }
}