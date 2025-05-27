using ProyectoTurnos.Models;

namespace ProyectoTurnos.Interfaces;

public interface IImagenEstudioService
{
    public bool CargarImagenEstudio(ImagenEstudio imagenEstudio);
    public bool EliminarImagenEstudio(ImagenEstudio imagenEstudio);
    public ImagenEstudio? BuscarImagenByItemNombreFecha(string documentoPaciente, string nombre, DateTime fecha);
    public List<ImagenEstudio> ListarImagenesByEstudio(Estudio estudio);
}