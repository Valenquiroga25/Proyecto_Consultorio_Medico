using Microsoft.AspNetCore.Mvc;
using ProyectoTurnos.Models;
using ProyectoTurnos.Models.DTOs;
using ProyectoTurnos.Services;

namespace ProyectoTurnos.Controllers;

[ApiController]
[Route("api/[controller")]
public class ItemEstudioController : Controller
{
    private readonly ItemEstudioService itemEstudioService;

    public ItemEstudioController(ItemEstudioService itemEstudioService)
    {
        this.itemEstudioService = itemEstudioService;
    }

    [HttpPost("generar")]
    public IActionResult GenerarItem([FromBody] ItemEstudioDTO itemEstudioDto)
    {
        try
        {
            ItemEstudio itemCargar = ConvertToEntity(itemEstudioDto);
            if(itemEstudioService.GenerarItem(itemCargar))
                return Ok("Item generado con éxito!");
            
            return Conflict("Ha ocurrido un error al generar el item del dia: "  + itemEstudioDto.fecha);
        }
        catch (Exception e)
        {
            return Conflict("Ha ocurrido un error al generar el item del dia "  + itemEstudioDto.fecha + ": " + e.Message);
        }
    }
    
    [HttpGet("listar")]
    public IActionResult ListarItemsByHistoria([FromBody] ItemEstudioDTO itemEstudioDto)
    {
        try
        {
            ItemEstudio itemEstudio = ConvertToEntity(itemEstudioDto);
            List<ItemEstudio>? itemsListados = itemEstudioService.ListarItemsByHistoria(itemEstudio);
            List<ItemEstudioDTO>? itemsEnviar = new List<ItemEstudioDTO>();
            
            foreach(ItemEstudio item in itemsListados)
            {
                ItemEstudioDTO itemDto = ConvertToDto(item);
                itemsEnviar.Add(itemDto);
            }
                
            return Ok(itemsEnviar);
        }
        catch (Exception e)
        {
            return Conflict("Ha ocurrido un error al listar los items de la historia clínica " + itemEstudioDto.documentoPaciente + ": " + e.Message);
        }
    }

    [HttpGet("listar/{documentoPaciente}/{fecha}")]
    public IActionResult BuscarItemByHistoriaFecha(string documentoPaciente, string fecha)
    {
        try
        {
            ItemEstudio? itemBuscado = itemEstudioService.BuscarItemByHistoriaYFecha(documentoPaciente, DateTime.Parse(fecha));

            if (itemBuscado != null)
            {
                ItemEstudioDTO itemFinal = ConvertToDto(itemBuscado);
                return Ok(itemFinal);
            }

            return NoContent();
        }
        catch (Exception e)
        {
            return Conflict("Ha ocurrido un error al buscar un item por historia y fecha: " + e.Message);
        }
    }
    
    [HttpDelete("eliminar/{documentoPaciente}/{fecha}")]
    public IActionResult EliminarItemEstudio(string documentoPaciente, string fecha)
    {
        {
            try
            {
                ItemEstudio? itemBuscado = itemEstudioService.BuscarItemByHistoriaYFecha(documentoPaciente, DateTime.Parse(fecha));

                if (itemBuscado == null)
                {
                    Console.WriteLine("El item que quiere eliminar no se encuentra en el sistema!");
                    return Problem("El item que quiere eliminar no se encuentra en el sistema!");
                }

                itemEstudioService.EliminarItem(itemBuscado);
                Console.WriteLine("El item ha sido eliminado con éxito!");
                return Ok("El item ha sido eliminado con éxito!");
            }
            catch (Exception e)
            {
                return Conflict("Ha ocurrido un error al intentar eliminar el item: " + e.Message);
            }
        }
    }

    private ItemEstudio ConvertToEntity(ItemEstudioDTO itemEstudioDto)
    {
        try
        {
            return new ItemEstudio(itemEstudioDto.documentoPaciente,itemEstudioDto.fecha);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al querer convertir un item DTO a Entity: " + e.Message);
            throw;
        }
    }
    
    private ItemEstudioDTO ConvertToDto(ItemEstudio itemEstudio)
    {
        try
        {
            return new ItemEstudioDTO(itemEstudio.fecha, itemEstudio.documentoPaciente);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al querer convertir un item Entity a DTO: " + e.Message);
            throw;
        }
    }
}