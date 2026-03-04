namespace ProyectoTurnos_FrontEnd.MiApp.Models;

public class HistoriaDTO
{
    public HistoriaDTO()
    {
    }

    public HistoriaDTO(string nombres, string apellidos, string documentoPaciente, string fechaNacimiento,
        string codArea, string telefono, string direccion, string correo, string descripcion)
    {
        this.nombres = nombres;
        this.apellidos = apellidos;
        this.documentoPaciente = documentoPaciente;
        this.fechaNacimiento = fechaNacimiento;
        this.codArea = codArea;
        this.telefono = telefono;
        this.direccion = direccion;
        this.correo = correo;
        this.descripcion = descripcion;
    }

    public string nombres { get; set; }
    public string apellidos { get; set; }
    public string documentoPaciente { get; set; }
    public string fechaNacimiento { get; set; }
    public string codArea { get; set; }
    public string telefono { get; set; }
    public string direccion { get; set; }
    public string correo { get; set; }

    public string descripcion { get; set; }
}