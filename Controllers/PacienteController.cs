using Microsoft.AspNetCore.Mvc;
using ProyectoTurnos.Models;
using ProyectoTurnos.Models.DTOs;
using ProyectoTurnos.Services;

namespace ProyectoTurnos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacienteController : Controller
{
    private readonly PacienteService pacienteService;

    public PacienteController(PacienteService pacienteService)
    {
        this.pacienteService = pacienteService;
    }
    
    [HttpGet("mensaje")]
    public IActionResult GetMensaje()
    {
        return Ok("Api funcionando correctamente!");
    }

    [HttpPost("registrar")]
    public IActionResult RegistrarPaciente(PacienteDTO pacienteDto)
    {
        try
        {
            Paciente? pacienteBuscado = pacienteService.BuscarPacienteByDocumento(pacienteDto.documento);

            if (pacienteBuscado != null)
            {
                Console.WriteLine("El paciente ya se encuentra registrado en el sistema!");
                return Problem("El paciente ya se encuentra registrado en el sistema!");
            }

            Paciente pacienteRegistrar = new Paciente(pacienteDto.nombreCompleto,
                pacienteDto.documento,
                pacienteDto.fechaNacimiento,
                pacienteDto.obraSocial,
                pacienteDto.telefono,
                pacienteDto.direccion,
                pacienteDto.correo);

            pacienteService.RegistrarPaciente(pacienteRegistrar);

            Console.WriteLine("Paciente registrado con éxito!");
            return Ok(pacienteRegistrar);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
    
    [HttpGet("listar")]
    public IActionResult GetPacientes()
    {
        try
        {
            List<Paciente>? pacientesBuscados = pacienteService.ListarPacientes();

            if (pacientesBuscados != null)
            {
                if (pacientesBuscados.Count > 0)
                {
                    Console.WriteLine("Listado enviado con éxito!");
                    return Ok(pacientesBuscados);
                }

                return NoContent();
            }

            return Problem("Ocurrió un error al querer listar los pacientes en el Controller del Backend!");
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpGet("/{documento}")]
    public IActionResult GetPacienteByDocumento(string documento)
    {
        try
        {
            Paciente? pacienteBuscado = pacienteService.BuscarPacienteByDocumento(documento);

            if (pacienteBuscado == null)
            {
                Console.WriteLine("El paciente no se encuentra registrado en el sistema!");
                return NoContent();
            }

            Console.WriteLine("Paciente enviado con éxito!");
            return Ok(pacienteBuscado);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPost("editar/{documento}")]
    public IActionResult EditarPaciente(string documento)
    {
        try
        {
            Paciente? pacienteBuscado = pacienteService.BuscarPacienteByDocumento(documento);

            if (pacienteBuscado == null)
            {
                Console.WriteLine("El paciente que quiere eliminar no se encuentra en el sistema!");
                return Problem("El paciente que quiere eliminar no se encuentra en el sistema!");
            }

            pacienteService.EliminarPaciente(pacienteBuscado);
            Console.WriteLine("El paciente ha sido eliminado con éxito!");
            return Ok("El paciente ha sido eliminado con éxito!");
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpDelete("eliminar/{documento}")]
    public IActionResult DeletePaciente(string documento)
    {
        {
            try
            {
                Paciente? pacienteBuscado = pacienteService.BuscarPacienteByDocumento(documento);

                if (pacienteBuscado == null)
                {
                    Console.WriteLine("El paciente que quiere eliminar no se encuentra en el sistema!");
                    return Problem("El paciente que quiere eliminar no se encuentra en el sistema!");
                }

                pacienteService.EliminarPaciente(pacienteBuscado);
                Console.WriteLine("El paciente ha sido eliminado con éxito!");
                return Ok("El paciente ha sido eliminado con éxito!");
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }
    }
}