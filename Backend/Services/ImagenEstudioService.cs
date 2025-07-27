using ProyectoTurnos.Data;
using ProyectoTurnos.Interfaces;
using ProyectoTurnos.Models;

namespace ProyectoTurnos.Services;

public class ImagenEstudioService : IImagenEstudioService
{

    private Context context;

    public ImagenEstudioService(Context context)
    {
        this.context= context;
    }
    
    public bool CargarImagenEstudio(ImagenEstudio imagenEstudio)
    {
        try
        {
            context.ImagenEstudio.Add(imagenEstudio);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al cargar una imagen del estudio: " + e.Message);
            return false;
        }
    }
    
    public bool EliminarImagenEstudio(ImagenEstudio imagenEstudio)
    {
        try
        {
            context.ImagenEstudio.Remove(imagenEstudio);
            context.SaveChanges();
            Console.WriteLine("Imagen eliminada con éxito!");
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al eliminar la imagen del estudio: " + e.Message);
            return false;
        }
    }

    public ImagenEstudio? BuscarImagenByItemNombreFecha(string documentoPaciente, string nombre, DateTime fecha)
    {
        try
        {
            return context.ImagenEstudio.SingleOrDefault(i => i.documentoPacienteEstudio == documentoPaciente
                                                              && i.nombreEstudio == nombre
                                                              && i.fecha == fecha);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al buscar la imagen del estudio: " + e.Message);
            throw;
        }
    }

    public List<ImagenEstudio> ListarImagenesByEstudio(Estudio estudio)
    {
        try
        {
            return context.ImagenEstudio.Where(i => i.documentoPacienteEstudio == estudio.documentoPaciente 
                                                    && i.fecha == estudio.fecha 
                                                    && i.nombreEstudio == estudio.nombre).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al listar las imágenes del estudio: " + e.Message);
            throw;
        }    
    }
}