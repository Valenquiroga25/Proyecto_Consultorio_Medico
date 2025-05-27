namespace ProyectoTurnos.Interfaces;

using ProyectoTurnos.Models;

public interface IPacienteService
{
    public Paciente RegistrarPaciente(Paciente paciente);
    public List<Paciente>? ListarPacientes();
    public Paciente? BuscarPacienteByDocumento(string documento);
    public Paciente? BuscarPacienteByApellido(string nombre);
    public void EditarPaciente(Paciente paciente);
}