namespace ProyectoTurnos_FrontEnd.MiApp.Models;

public class ItemConEstudios
{
    public ItemConEstudios()
    {
    }

    public ItemConEstudios(string fecha, List<EstudioDTO> estudios)
    {
        this.fecha = fecha;
        this.estudios = estudios;
    }

    public string fecha { get; set; }
    public List<EstudioDTO> estudios { get; set; }
}