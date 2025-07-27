using ProyectoTurnos.Data;
using ProyectoTurnos.Interfaces;
using ProyectoTurnos.Models;

namespace ProyectoTurnos.Services;

public class EstudioService : IEstudioService
{
    
    private Context context;

    public EstudioService(Context context)
    {
        this.context = context;
    }

    public bool GenerarEstudio(Estudio estudio)
    {
        try
        {
            Estudio? estudioBuscado = BuscarEstudioByHistoriaNombreFecha(estudio.documentoPaciente, estudio.nombre, estudio.fecha);

            if (estudioBuscado != null)
            {
                Console.WriteLine("Ya existe un estudio con la fecha: " + estudio.fecha + "!");
                return false;
            }
            
            context.Estudio.Add(estudio);
            context.SaveChanges();
            Console.WriteLine("El estudio ha sido generado con éxito!");
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al generar el estudio: " + e.Message);
            return false;
        }
    }

    public bool EliminarEstudio(Estudio estudio)
    {
        try
        {
            context.Estudio.Remove(estudio);
            context.SaveChanges();
            Console.WriteLine("El estudio ha sido eliminado con éxito!");
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al eliminar el estudio: " + e.Message);
            return false;
        }
    }

    public Estudio? BuscarEstudioByHistoriaNombreFecha(string documentoPaciente, string nombre, DateTime fecha)
    {
        try
        {
            return context.Estudio.SingleOrDefault(e => e.documentoPaciente == documentoPaciente 
                                                        && e.nombre == nombre 
                                                        && e.fecha == fecha);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al devolver el estudio: " + e.Message);
            throw;
        }    
    }

    public List<Estudio> ListarEstudiosByItem(ItemEstudio itemEstudio)
    {
        try
        {
            return context.Estudio.Where(e => e.documentoPaciente == itemEstudio.documentoPaciente 
                                                        && e.fecha == itemEstudio.fecha).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al listar los estudios: " + e.Message);
            throw;
        }
    }
}