using System.Collections.ObjectModel;

namespace ProyectoTurnos_FrontEnd.MiApp.Models;

public class ItemEstudioDTO
{
    public string documentoPaciente { get; set; }
    public string fecha { get; set; }
    
    public ObservableCollection<EstudioDTO> estudios { get; set; } = new();
    
    public ItemEstudioDTO() {}

    public ItemEstudioDTO(string documentoPaciente, string fecha)
    {
        this.documentoPaciente = documentoPaciente;
        this.fecha = fecha;
    }
    
    public ItemEstudioDTO(string documentoPaciente, string fecha, ObservableCollection<EstudioDTO> estudios)
    {
        this.documentoPaciente = documentoPaciente;
        this.fecha = fecha;
        this.estudios = estudios;
    }
}