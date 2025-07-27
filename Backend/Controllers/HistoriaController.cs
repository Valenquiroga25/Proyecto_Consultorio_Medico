using Microsoft.AspNetCore.Mvc;
using ProyectoTurnos.Models;
using ProyectoTurnos.Services;
using ProyectoTurnos.Models.DTOs;

namespace ProyectoTurnos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoriaController : Controller
{
    private readonly HistoriaService historiaService;
    private readonly PacienteService pacienteService;

    public HistoriaController(HistoriaService historiaService, PacienteService pacienteService)
    {
        this.historiaService = historiaService;
        this.pacienteService = pacienteService;
    }
    
    [HttpGet]
    public IActionResult Mensaje()
    {
        return Ok("Api funcionando!");
    }

    [HttpPost("generar")]
    public IActionResult GenerarHistoriaClinica([FromBody] HistoriaDTO historiaDto)
    {
        try
        {
            Historia? historiaCargar = convertToEntity(historiaDto);
            if(historiaService.GenerarHistoria(historiaCargar))
                return Ok("Historia Clínica generada con éxito!");

            return Conflict("Ha ocurrido un error al generar una historia clínica!");
        }
        catch (Exception e)
        {
            return Conflict("Ha ocurrido un error al generar una historia clínica: " + e.Message);
        }
    }
    
    [HttpGet("listar")]
    public IActionResult GetHistoriasClinicas()
    {
        try
        {
            List<Historia>? historiasListadas = historiaService.ListarHistorias();
            List<HistoriaDTO>? historiasEnviar = new List<HistoriaDTO>();
            
            foreach(Historia his in historiasListadas)
            {
                HistoriaDTO hisDto = convertToDto(his);
                historiasEnviar.Add(hisDto);
            }
                
            return Ok(historiasEnviar);
        }
        catch (Exception e)
        {
            return Conflict("Ha ocurrido un error al listar las historias clínicas: " + e.Message);
        }
    }

    [HttpGet("listar/{documento}")]
    public IActionResult GetHistoriaByDocumento(string documento)
    {
        try
        {
            Historia? historiaBuscada = historiaService.BuscarHistoriaByDocumento(documento);

            if (historiaBuscada != null)
            {
                HistoriaDTO historiaFinal = convertToDto(historiaBuscada);
                return Ok(historiaFinal);
            }

            return NoContent();
        }
        catch (Exception e)
        {
            return Conflict("Ha ocurrido un error al listar la historia clínica por documento de paciente: " + e.Message);
        }
    }

    [HttpPost("editar/{documento}")]
    public IActionResult EditarHistoria(HistoriaDTO historiaDto)
    {
        try
        {
            Historia? historia = convertToEntity(historiaDto);
            
            if(historia != null)
                historiaService.EditarHistoria(historia);                    
            
            return Ok("La historia se ha guardado con éxito!");
        }
        catch (Exception e)
        {
            return Conflict("Ha ocurrido un error al editar la historia clínica: " + e.Message);
        }
    }
    
    private Historia? convertToEntity(HistoriaDTO historiaDto)
    {
        try
        {
            Paciente? pacienteBuscado = pacienteService.BuscarPacienteByDocumento(historiaDto.documentoPaciente);

            if (pacienteBuscado == null)
            {
                Paciente pacienteCargar = new Paciente(historiaDto.nombres,
                    historiaDto.apellidos,
                    historiaDto.documentoPaciente,
                    DateTime.Parse(historiaDto.fechaNacimiento),
                    historiaDto.codArea,
                    historiaDto.telefono,
                    historiaDto.direccion,
                    historiaDto.correo
                    );
                
                Paciente pacienteCargado = pacienteService.RegistrarPaciente(pacienteCargar); // Por si el dia de mañana se crean pacientes pero no con historias clínicas asociadas.
                return new Historia(pacienteCargado, historiaDto.descripcion);
            }
            
            return new Historia(pacienteBuscado,historiaDto.descripcion);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al querer convertir un DTO a Entity: " + e.Message);
            throw;
        }
    }

    private HistoriaDTO convertToDto(Historia historia)
    {
        try
        {
            return new HistoriaDTO(historia);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al convertir un Entity de Historia a DTO: " + e.Message);
            throw;
        }
    }
}