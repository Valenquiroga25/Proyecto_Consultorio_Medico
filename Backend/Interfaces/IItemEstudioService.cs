using ProyectoTurnos.Models;

namespace ProyectoTurnos.Interfaces;

public interface IItemEstudioService
{
    public bool GenerarItem(ItemEstudio itemEstudio);
    public bool EliminarItem(ItemEstudio itemEstudio);
    public List<ItemEstudio> ListarItemsByHistoria(String documentoPaciente);

    public ItemEstudio? BuscarItemByHistoriaYFecha(string documentoPaciente, DateTime fecha);
}