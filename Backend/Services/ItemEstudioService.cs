using ProyectoTurnos.Data;
using ProyectoTurnos.Interfaces;
using ProyectoTurnos.Models;
using ProyectoTurnos.Models.DTOs;

namespace ProyectoTurnos.Services;

public class ItemEstudioService : IItemEstudioService
{

    private Context context;

    public ItemEstudioService(Context context)
    {
        this.context = context;
    }
    
    public bool GenerarItem(ItemEstudio itemEstudio)
    {
        try
        {
            ItemEstudio? itemBuscado = BuscarItemByHistoriaYFecha(itemEstudio.documentoPaciente,itemEstudio.fecha);

            if (itemBuscado != null)
            {
                Console.WriteLine("Ya existe un item con la fecha: " + itemEstudio.fecha + "!");
                return false;
            }
            
            context.ItemEstudio.Add(itemEstudio);
            context.SaveChanges();
            Console.WriteLine("El item ha sido generado con éxito!");
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al generar el item del estudio: " + e.Message);
            return false;
        }
    }

    public bool EliminarItem(ItemEstudio itemEstudio)
    {
        try
        {
            ItemEstudio? itemBuscado = BuscarItemByHistoriaYFecha(itemEstudio.documentoPaciente,itemEstudio.fecha);

            if (itemBuscado == null)
            {
                Console.WriteLine("No existe un item con la fecha: " + itemEstudio.fecha);
                return false;
            }
            
            context.ItemEstudio.Remove(itemEstudio);
            context.SaveChanges();
            Console.WriteLine("El item ha sido eliminado con éxito!");
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al eliminar el item del estudio: " + e.Message);
            return false;
        }
    }

    public List<ItemEstudio> ListarItemsByHistoria(ItemEstudio itemEstudio)
    {
        try
        {
            return context.ItemEstudio.Where(i => i.documentoPaciente == itemEstudio.documentoPaciente).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al buscar el item del estudio por fecha: " + e.Message);
            throw;
        }
    }

    public ItemEstudio? BuscarItemByHistoriaYFecha(string documentoPaciente, DateTime fecha)
    {
        try
        {
            return context.ItemEstudio.SingleOrDefault(i => i.documentoPaciente == documentoPaciente && i.fecha == fecha);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al buscar el item del estudio por fecha: " + e.Message);
            throw;
        }    
    }
}