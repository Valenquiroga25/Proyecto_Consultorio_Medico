
namespace ProyectoTurnos.Services;

using Interfaces;
using Models;
using Data;


public class PacienteService : IPacienteService
{
    private Context context;

    public PacienteService(Context context)
    {
        this.context = context;
    }

    public Paciente RegistrarPaciente(Paciente paciente)
    {
        try
        {
            Paciente? pacienteBuscado = BuscarPacienteByDocumento(paciente.documento);
            
            if (pacienteBuscado != null)
            {
                Console.WriteLine("El paciente ya se encuentra registrado en el sistema!");
                return null;
            }
            
            context.Paciente.Add(paciente);
            context.SaveChanges();
            Console.WriteLine("El paciente fue registrado con éxito!");
            return paciente;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error en el paciente: " + e.Message);
            throw;
        }
    }

    public List<Paciente> ListarPacientes()
    {
        try
        {
            return context.Paciente.ToList(); // Nunca devuelve Null, en todo caso una lista vacia. 
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error en el paciente: " + e.Message);
            throw;
        }
    }
    
    public Paciente? BuscarPacienteByDocumento(string docu)
    {
        try
        {
            Paciente? pacienteBuscado = context.Paciente.SingleOrDefault(p => p.documento == docu);
            return pacienteBuscado;
        }
        catch (Exception e)
        { 
            Console.WriteLine("Ha ocurrido un error en el paciente: " + e.Message);
            throw;
        }
    }

    public Paciente? BuscarPacienteByApellido(string apellidos)
    {
        try
        {
            Paciente? pacienteBuscado = context.Paciente.SingleOrDefault(p => p.apellidos == apellidos);
            
            if(pacienteBuscado == null)
                Console.WriteLine("El paciente no se encuentra registrado en el sistema!");
            
            return pacienteBuscado;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error en el paciente: " + e.Message);
            throw;
        }    
    }
    
    public void EditarPaciente(Paciente paciente)
    {
        try
        {
            Paciente? pacienteBuscado = BuscarPacienteByDocumento(paciente.documento);
            
            if (pacienteBuscado == null)
            {
                Console.WriteLine("El paciente no se encuentra registrado en el sistema!");
                return;
            }
            
            pacienteBuscado.nombres = paciente.nombres;
            pacienteBuscado.apellidos = paciente.apellidos;
            pacienteBuscado.documento = paciente.documento;
            pacienteBuscado.fechaNacimiento = paciente.fechaNacimiento;
            pacienteBuscado.codArea = paciente.codArea;
            pacienteBuscado.telefono = paciente.telefono;
            pacienteBuscado.direccion = paciente.direccion;
            pacienteBuscado.correo = paciente.correo;

            context.SaveChanges();
            Console.WriteLine("El paciente ha sido modificado con éxito!");
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error en el paciente: " + e.Message);
            throw;
        }
    }

    public void EliminarPaciente(Paciente paciente)
    {
        try
        {
            context.Paciente.Remove(paciente);
            context.SaveChanges();
            Console.WriteLine("El paciente ha sido eliminado del sistema de manera exitosa!");
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al eliminar un paciente del sistema: " + e.Message);
            throw;
        }
    }
}