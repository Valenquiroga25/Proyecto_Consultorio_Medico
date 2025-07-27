using ProyectoTurnos.Models;

namespace ProyectoTurnos.Interfaces;

public interface IItemEstudioService
{
    public bool GenerarItem(ItemEstudio itemEstudio);
    public bool EliminarItem(ItemEstudio itemEstudio);
    public List<ItemEstudio> ListarItemsByHistoria(ItemEstudio itemEstudio);

    public ItemEstudio? BuscarItemByHistoriaYFecha(string documentoPaciente, DateTime fecha);
}