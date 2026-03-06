namespace ProyectoTurnos.Models.DTOs;

public class ItemEstudioDTO
{
    public string documentoPaciente { get; set; }
    
    public string fecha { get; set; }
    
    public List<EstudioDTO> estudios { get; set; }
    
    public ItemEstudioDTO(){}

    public ItemEstudioDTO(string fecha, string documentoPaciente)
    {
        this.documentoPaciente = documentoPaciente;
        this.fecha = fecha;
        estudios = new List<EstudioDTO>();
    }
    
    public ItemEstudioDTO(string fecha, string documentoPaciente, List<EstudioDTO> estudios)
    {
        this.documentoPaciente = documentoPaciente;
        this.fecha = fecha;
        this.estudios = estudios;
    }
}