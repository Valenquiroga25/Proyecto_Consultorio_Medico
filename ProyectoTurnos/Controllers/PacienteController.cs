using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration;
using ProyectoTurnos.Data;
using ProyectoTurnos.Models;

namespace ProyectoTurnos.Controllers
{
    public class PacienteController : Controller
    {
        private readonly PacienteContext _context;

        public PacienteController(PacienteContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Paciente.ToListAsync());
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Paciente
                .FirstOrDefaultAsync(m => m.idPaciente == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idPaciente,nombreCompleto,obraSocial,documento,telefono,fechaNacimiento")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                if (DocumentoExist(paciente.documento)){throw new Exception("Ya existe un paciente con este documento en la base de datos.");}
                
                _context.Add(paciente);
                await _context.SaveChangesAsync(); // EL 'await' indica que se espere a que se termine el proceso para continuar la ejecución.
                return RedirectToAction(nameof(Index)); // Si el guardado del obj en la BD es exitoso redirecciona a la lista. 
            }
            return View(paciente);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Paciente.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idPaciente,nombreCompleto,obraSocial,documento,telefono,fechaNacimiento")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                
                var pacienteEncontrado =  await _context.Paciente.FirstOrDefaultAsync(p => p.documento == paciente.documento);

                if (pacienteEncontrado!=null)
                {
                    if (pacienteEncontrado.idPaciente != paciente.idPaciente)
                    {
                        throw new Exception("El paciente ya se encuentra registrado en la base de datos.");
                    }

                    pacienteEncontrado.nombreCompleto = paciente.nombreCompleto;
                    pacienteEncontrado.documento = paciente.documento;
                    pacienteEncontrado.obraSocial = paciente.obraSocial;
                    pacienteEncontrado.fechaNacimiento= paciente.fechaNacimiento;
                    pacienteEncontrado.telefono= paciente.telefono;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                _context.Update(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(paciente);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Paciente
                .FirstOrDefaultAsync(m => m.idPaciente == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Paciente.FindAsync(id);
            if (usuario != null)
            {
                _context.Paciente.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> FindByDNI(int Busqueda)
        {
            ViewData["BusquedaTurno"] = Busqueda;
            if (Busqueda.ToString().Length == 8)
            {
                var dni = Convert.ToInt32(Busqueda);
                var pacientes = await _context.Paciente
                    .Where(u => u.documento == dni)
                    .ToListAsync();

                return View(pacientes);
            }
            return RedirectToAction(nameof(Index));
        }
        
        private bool PacienteExists(int id)
        {
            return _context.Paciente.Any(e => e.idPaciente == id);
        }
        
        private bool DocumentoExist(int? documento)
        {
            return _context.Paciente.Any(e => e.documento == documento);
        }
    }
}
