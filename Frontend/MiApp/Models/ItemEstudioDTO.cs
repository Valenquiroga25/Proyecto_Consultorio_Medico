namespace ProyectoTurnos_FrontEnd.MiApp.Models;

public class ItemEstudioDTO
{
    public string documentoPaciente { get; set; }
    public string fecha { get; set; }
    
    public List<EstudioDTO> estudios { get; set; }

    public ItemEstudioDTO(){}

    public ItemEstudioDTO(string documentoPaciente, string fecha, List<EstudioDTO> estudios)
    {
        this.documentoPaciente = documentoPaciente;
        this.fecha = fecha;
        this.estudios = estudios;
    }
}