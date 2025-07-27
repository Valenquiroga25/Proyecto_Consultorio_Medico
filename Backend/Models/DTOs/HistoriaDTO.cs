namespace ProyectoTurnos.Models.DTOs;

public class HistoriaDTO
{
    public string nombres { get; set; }
    public string apellidos { get; set; }
    public string documentoPaciente { get; set; }
    public string? fechaNacimiento { get; set; }
    public string? codArea { get; set; }
    public string? telefono { get; set; }
    public string? direccion { get; set; }
    public string? correo { get; set; }
    public string descripcion { get; set; }
    
    // Constructor requerido para la deserialización
    public HistoriaDTO() { }
    public HistoriaDTO(Historia historia)
    {
        nombres = historia.paciente.nombres;
        apellidos = historia.paciente.apellidos;
        documentoPaciente = historia.documentoPaciente;
        fechaNacimiento = historia.paciente.fechaNacimiento.ToString();
        codArea = historia.paciente.codArea;
        telefono = historia.paciente.telefono;
        direccion = historia.paciente.direccion;
        correo = historia.paciente.correo;
        descripcion = historia.descripcion;
    }
}