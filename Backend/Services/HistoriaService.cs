using Microsoft.Build.Experimental;
using Microsoft.EntityFrameworkCore;
using ProyectoTurnos.Data;
using ProyectoTurnos.Interfaces;
using ProyectoTurnos.Models;

namespace ProyectoTurnos.Services;

public class HistoriaService : IHistoriaService
{
    private Context context;

    public HistoriaService(Context context)
    {
        this.context = context;
    }
    
    public bool GenerarHistoria(Historia historia)
    {
        try
        {
            Historia? historiaBuscada = BuscarHistoriaByDocumento(historia.documentoPaciente);

            if (historiaBuscada != null)
            {
                Console.WriteLine("La historia clínica ya se encuentra registrada en el sistema!");
                return false;
            }

            // Si no se encuentra registrada la historia se la carga directo. Se verifica si el paciente existe en 'convertToEntity' (controller), que llama a pacienteService.
            context.Historia.Add(historia);
            context.SaveChanges();
            Console.WriteLine("Historia clínica generada con éxito!");
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al generar una historia clínica: " + e.Message);
            return false;
        }
    }

    public bool EditarHistoria(Historia historia)
    {
        try
        {
            Historia? historiaActualizar = BuscarHistoriaByDocumento(historia.documentoPaciente);

            if (historiaActualizar != null)
            {
                historiaActualizar.descripcion = historia.descripcion;
                context.SaveChanges();
                Console.WriteLine("Historia clínica editada con éxito!");
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al editar la historia clínica: " + e.Message);
            throw;
        }
    }

    public bool EliminarHistoria(Historia historia)
    {
        try
        {
            Historia? historiaBuscada = BuscarHistoriaByDocumento(historia.documentoPaciente);

            if (historiaBuscada == null)
            {
                Console.WriteLine("La historia clínica no se encuentra registrada en el sistema!");
                return false;
            }

            context.Remove(historiaBuscada);
            context.SaveChanges();
            Console.WriteLine("Historia clínica eliminada con éxito!");
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al editar una historia clínica: " + e.Message);
            throw;
        }
    }

    public List<Historia>? ListarHistorias()
    {
        try
        {
            // Se retorna la lista de esta manera para que al hacer un listarHistorias desde el front se incluya el objeto de 'paciente'.
            return context.Historia.Include(h => h.paciente).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al listar las historias clínicas: " + e.Message);
            throw;
        }
    }

    public Historia? BuscarHistoriaByDocumento(string documentoPaciente)
    {
        try
        {
            return context.Historia.Include(obj => obj.paciente).FirstOrDefault(h => h.paciente.documento == documentoPaciente);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al buscar una historia clínica: " + e.Message);
            throw;
        }
    }
}