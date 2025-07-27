namespace ProyectoTurnos.Models.DTOs;

public class PacienteDTO
{
    public string nombres { get; set; }
    public string apellidos { get; set; }
    public string documento { get; set; }
    public DateTime fechaNacimiento { get; set; }
    public string codArea { get; set; }
    public string telefono { get; set; }
    public string direccion { get; set; }
    public string correo { get; set; }

    public PacienteDTO()
    {
    }

    public PacienteDTO(string nombres, string apellidos, string documento, DateTime fechaNacimiento, string codArea, string telefono, string direccion, string correo)
    {
        this.nombres = nombres;
        this.apellidos = apellidos;
        this.documento = documento;
        this.fechaNacimiento = fechaNacimiento;
        this.codArea = codArea;
        this.telefono = telefono;
        this.direccion = direccion;
        this.correo = correo;
    }
}