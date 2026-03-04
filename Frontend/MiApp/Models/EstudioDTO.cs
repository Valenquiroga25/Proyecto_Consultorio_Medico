namespace ProyectoTurnos_FrontEnd.MiApp.Models;

public class EstudioDTO
{
    
    public string nombre { get; set; }
    public string documentoPaciente { get; set; }
    public string fecha { get; set; }
    
    public EstudioDTO()
    {
    }

    public EstudioDTO(string nombre, string documentoPaciente, string fecha)
    {
        this.nombre = nombre;
        this.documentoPaciente = documentoPaciente;
        this.fecha = fecha;
    }
}