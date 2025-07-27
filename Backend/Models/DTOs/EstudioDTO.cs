namespace ProyectoTurnos.Models.DTOs;

public class EstudioDTO
{
    public string nombre { get; set; }
    public string documentoPaciente { get; set; }
    public DateTime fecha { get; set; }
    
    public EstudioDTO(){}

    public EstudioDTO(string nombre, string documentoPaciente, DateTime fecha)
    {
        this.nombre = nombre;
        this.documentoPaciente = documentoPaciente;
        this.fecha = fecha;
    }
}