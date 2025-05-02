namespace ProyectoTurnos.Models.DTOs;

public class PacienteDTO
{
    public string nombreCompleto { get; set; }
    public string documento { get; set; }
    public DateTime fechaNacimiento { get; set; }
    public string obraSocial { get; set; }
    public string telefono { get; set; }
    public string direccion { get; set; }
    public string correo { get; set; }
}