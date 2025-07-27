using ProyectoTurnos.Interfaces;
using ProyectoTurnos.Models;
using ProyectoTurnos.Data;

namespace ProyectoTurnos.Services;

public class TurnoService : ITurnoService
{
    private Context context;
    private PacienteService pacienteService;

    public TurnoService(Context context, PacienteService pacienteService)
    {
        this.context = context;
        this.pacienteService = pacienteService;
    }
    
    public void GenerarTurno(Turno turno)
    {
        try
        {
            Turno? turnoBuscado = ObtenerTurnoByFecha(turno.fecha);

            if (turnoBuscado != null)
            {
                Console.WriteLine("Ya existe un turno dado en esa fecha!");
                return;
            }

            Paciente? pac = pacienteService.BuscarPacienteByDocumento(turno.paciente.documento);

            if (pac == null)
            {
                Console.WriteLine("El paciente no se encuentra registrado en el sistema!");
                return;
            }            

            context.Add(turno);
            context.SaveChanges();
            Console.WriteLine("Turno generado con éxito!");
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error en el turno: " + e.Message);
            throw;
        }
    }

    public List<Turno>? BuscarTurnos()
    {
        try
        {
            return context.Turno.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error en el turno: " + e.Message);
            throw;
        }
    }

    public Turno? BuscarTurnoById(int id)
    {
        try
        {
            Turno? turnoBuscado = context.Turno.SingleOrDefault(t => t.idTurno == id);
            if (turnoBuscado == null)
                Console.WriteLine("El turno no se encuentra registrado en el sistema!");

            return turnoBuscado;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error en el turno: " + e.Message);
            throw;
        }
    }

    public void EditarTurno(Turno turno)
    {
        try
        {
            Turno? turnoBuscado = BuscarTurnoById(turno.idTurno);
            if (turnoBuscado == null)
            {
                Console.WriteLine("El turno no se encuentra registrado en el sistema!");
                return;
            }

            turnoBuscado.paciente = turno.paciente;
            turnoBuscado.documento = turno.documento;
            turnoBuscado.fecha = turno.fecha;
            turnoBuscado.descripcion = turno.descripcion;
            turnoBuscado.estado = turno.estado;

            context.SaveChanges();
            Console.WriteLine("El turno fue modificado con éxito!");
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error en el turno: " + e.Message);
            throw;
        }
    }

    public Turno? ObtenerTurnoByFecha(DateTime fecha)
    {
        Turno? turnoBuscado = context.Turno.SingleOrDefault(t => t.fecha == fecha);
        if (turnoBuscado == null)
            Console.WriteLine("El turno no se encuentra registrado en el sistema!");

        return turnoBuscado;
    }
}