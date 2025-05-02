
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

    public void RegistrarPaciente(Paciente paciente)
    {
        try
        {
            Paciente? pacienteBuscado = BuscarPacienteByDocumento(paciente.documento);
            
            if (pacienteBuscado != null)
            {
                Console.WriteLine("El paciente ya se encuentra registrado en el sistema!");
                return;
            }
            
            context.Paciente.Add(paciente);
            context.SaveChanges();
            Console.WriteLine("El paciente fue registrado con éxito!");
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error en el paciente: " + e.Message);
        }
    }

    public List<Paciente>? ListarPacientes()
    {
        try
        {
            return context.Paciente.ToList(); // Nunca devuelve Null, en todo caso una lista vacia. 
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error en el paciente: " + e.Message);
            return null;
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
            return null;
        }
    }

    public Paciente? BuscarPacienteByNombreCompleto(string nombre)
    {
        try
        {
            Paciente? pacienteBuscado = context.Paciente.SingleOrDefault(p => p.nombreCompleto == nombre);
            
            if(pacienteBuscado == null)
                Console.WriteLine("El paciente no se encuentra registrado en el sistema!");
            
            return pacienteBuscado;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error en el paciente: " + e.Message);
            return null;
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
            
            pacienteBuscado.nombreCompleto = paciente.nombreCompleto;
            pacienteBuscado.documento = paciente.documento;
            pacienteBuscado.fechaNacimiento = paciente.fechaNacimiento;
            pacienteBuscado.obraSocial = paciente.obraSocial;
            pacienteBuscado.telefono = paciente.telefono;
            pacienteBuscado.direccion = paciente.direccion;
            pacienteBuscado.correo = paciente.correo;

            context.SaveChanges();
            Console.WriteLine("El paciente ha sido modificado con éxito!");
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error en el paciente: " + e.Message);
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
        }
    }
}