using Microsoft.AspNetCore.Mvc;
using ProyectoTurnos.Models;
using ProyectoTurnos.Models.DTOs;
using ProyectoTurnos.Services;

namespace ProyectoTurnos.Controllers;

[ApiController]
[Route("api/[controller")]
public class EstudioController : Controller
{
    private readonly EstudioService estudioService;
    private readonly ItemEstudioService itemEstudioService;

    public EstudioController(EstudioService estudioService, ItemEstudioService itemEstudioService)
    {
        this.estudioService = estudioService;
        this.itemEstudioService = itemEstudioService;
    }
    
    [HttpPost("generar")]
    public IActionResult GenerarEstudio([FromBody] EstudioDTO estudioDto)
    {
        try
        {
            Estudio estudioCargar = ConvertToEntity(estudioDto);
            if(estudioService.GenerarEstudio(estudioCargar))
                return Ok("Estudio generado con éxito!");
            
            return Conflict("Ha ocurrido un error al generar el estudio!");
        }
        catch (Exception e)
        {
            return Conflict("Ha ocurrido un error al generar el estudio: " + e.Message);
        }
    }
    
    [HttpGet("listar")]
    public IActionResult ListarEstudiosByItem([FromBody] EstudioDTO estudioDto)
    {
        try
        {
            ItemEstudio? itemEstudio = itemEstudioService.BuscarItemByHistoriaYFecha(estudioDto.documentoPaciente, estudioDto.fecha);
            List<Estudio>? estudiosListados = estudioService.ListarEstudiosByItem(itemEstudio);
            List<EstudioDTO>? estudiosEnviar = new List<EstudioDTO>();
            
            foreach(Estudio estudio in estudiosListados)
            {
                EstudioDTO estudiosDto = ConvertToDto(estudio);
                estudiosEnviar.Add(estudiosDto);
            }
                
            return Ok(estudiosEnviar);
        }
        catch (Exception e)
        {
            return Conflict("Ha ocurrido un error al listar los estudios del item perteneciente a la historia " + estudioDto.documentoPaciente + ": " + e.Message);
        }
    }

    [HttpGet("listar/{documentoPaciente}/{nombre}/{fecha}")]
    public IActionResult BuscarEstudioByHistoriaNombreFecha(string documentoPaciente, string nombre, string fecha)
    {
        try
        {
            Estudio? estudioBuscado = estudioService.BuscarEstudioByHistoriaNombreFecha(documentoPaciente, nombre, DateTime.Parse(fecha));

            if (estudioBuscado != null)
            {
                EstudioDTO estudioFinal = ConvertToDto(estudioBuscado);
                return Ok(estudioFinal);
            }

            return NoContent();
        }
        catch (Exception e)
        {
            return Conflict("Ha ocurrido un error al buscar un estudio por historia, nombre y fecha: " + e.Message);
        }
    }
    
    [HttpDelete("eliminar/{documentoPaciente}/{nombre}/{fecha}")]
    public IActionResult EliminarEstudio(string documentoPaciente, string nombre, string fecha)
    {
        {
            try
            {
                Estudio? estudioBuscado = estudioService.BuscarEstudioByHistoriaNombreFecha(documentoPaciente, nombre, DateTime.Parse(fecha));

                if (estudioBuscado == null)
                {
                    Console.WriteLine("El estudio que quiere eliminar no se encuentra en el sistema!");
                    return Problem("El estudio que quiere eliminar no se encuentra en el sistema!");
                }

                estudioService.EliminarEstudio(estudioBuscado);
                Console.WriteLine("El estudio ha sido eliminado con éxito!");
                return Ok("El estudio ha sido eliminado con éxito!");
            }
            catch (Exception e)
            {
                return Conflict("Ha ocurrido un error al intentar eliminar el estudio: " + e.Message);
            }
        }
    }
    
    private Estudio ConvertToEntity(EstudioDTO estudioDto)
    {
        try
        {
            return new Estudio(estudioDto.nombre,estudioDto.documentoPaciente,estudioDto.fecha);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al querer convertir un estudio DTO a Entity: " + e.Message);
            throw;
        }
    }
    
    private EstudioDTO ConvertToDto(Estudio estudio)
    {
        try
        {
            return new EstudioDTO(estudio.nombre,estudio.documentoPaciente,estudio.fecha);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al querer convertir un estudio Entity a DTO: " + e.Message);
            throw;
        }
    }
}