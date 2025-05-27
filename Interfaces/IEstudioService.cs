using ProyectoTurnos.Models;

namespace ProyectoTurnos.Interfaces;

public interface IEstudioService
{
    public bool GenerarEstudio(Estudio estudio);
    public bool EliminarEstudio(Estudio estudio);
    public Estudio? BuscarEstudioByHistoriaNombreFecha(string documentoPaciente, string nombre, DateTime fecha);
    public List<Estudio> ListarEstudiosByItem(ItemEstudio itemEstudio);
}