
namespace ProyectoTurnos_FrontEnd.MiApp.Models;

public class PacienteDTO
{
    public string nombreCompleto { get; set; }
    public string documento { get; set; }
    public string fechaNacimiento { get; set; }
    public string obraSocial { get; set; }
    public string telefono { get; set; }
    public string direccion { get; set; }
    public string correo { get; set; }

    public PacienteDTO(string nombreCompleto, string documento, string fechaNacimiento, string obraSocial,
        string telefono, string direccion, string correo)
    {
        this.nombreCompleto = nombreCompleto;
        this.documento = documento;
        this.fechaNacimiento = fechaNacimiento;
        this.obraSocial = obraSocial;
        this.telefono = telefono;
        this.direccion = direccion;
        this.correo = correo;
    }
}