namespace ProyectoTurnos.Interfaces;

using ProyectoTurnos.Models;

public interface IPacienteService
{
    public void RegistrarPaciente(Paciente paciente);
    public List<Paciente>? ListarPacientes();
    public Paciente? BuscarPacienteByDocumento(string documento);
    public Paciente? BuscarPacienteByNombreCompleto(string nombre);
    public void EditarPaciente(Paciente paciente);
}