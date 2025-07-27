using ProyectoTurnos.Models;

namespace ProyectoTurnos.Interfaces;

public interface IHistoriaService
{
   public bool GenerarHistoria(Historia historia);
   public bool EditarHistoria(Historia historia);
   public bool EliminarHistoria(Historia historia);
   public List<Historia>? ListarHistorias();
   public Historia? BuscarHistoriaByDocumento(string documentoPaciente);
}