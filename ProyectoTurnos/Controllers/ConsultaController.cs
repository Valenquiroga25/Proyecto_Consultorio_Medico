using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTurnos.Data;
using ProyectoTurnos.Models;

namespace ProyectoTurnos
{
    public class ConsultaController : Controller
    {
        private readonly ConsultaContext _context;

        public ConsultaController(ConsultaContext context)
        {
            _context = context;
        }

        // GET: Consulta
        public async Task<IActionResult> Index()
        {
            return View(await _context.Consulta.ToListAsync());
        }
        
        
        // GET: Consulta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdConsulta,descripcion,precio")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                if (ConsultaExists(consulta.descripcion)){throw new Exception("Ya existe esta consulta en la base de datos.");}

                _context.Add(consulta);
                await _context.SaveChangesAsync(); // EL 'await' indica que se espere a que se termine el proceso para continuar la ejecución.
                return RedirectToAction(nameof(Index)); // Si el guardado del obj en la BD es exitoso redirecciona a la lista. 
            }
            return View(consulta);
        }

        // GET: Consulta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consulta.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }
            return View(consulta);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdConsulta,descripcion,precio")] Consulta consulta)
        {
            if (id != consulta.IdConsulta){return NotFound();}

            if (ModelState.IsValid)
            {
                if (ConsultaExists(consulta.descripcion)){throw new Exception("Ya existe esta consulta en la base de datos.");}
                
                _context.Update(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consulta);
        }

        // GET: Consulta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Consulta
                .FirstOrDefaultAsync(m => m.IdConsulta == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consulta = await _context.Consulta.FindAsync(id);
            if (consulta != null)
            {
                _context.Consulta.Remove(consulta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool ConsultaExists(String descripcion)
        {
            return _context.Consulta.Any(e => e.descripcion == descripcion);
        }
    }
}
