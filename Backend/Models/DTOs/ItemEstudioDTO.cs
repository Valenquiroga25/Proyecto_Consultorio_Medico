namespace ProyectoTurnos.Models.DTOs;

public class ItemEstudioDTO
{
    public DateTime fecha { get; set; }
    public string documentoPaciente { get; set; }
    
    public ItemEstudioDTO(){}

    public ItemEstudioDTO(DateTime fecha, string documentoPaciente)
    {
        this.fecha = fecha;
        this.documentoPaciente = documentoPaciente;
    }
}