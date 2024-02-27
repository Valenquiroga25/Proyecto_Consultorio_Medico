using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTurnos.Data;
using ProyectoTurnos.Models;

namespace ProyectoTurnos
{
    public class TurnoController : Controller
    {
        private readonly TurnoContext _context;
        private readonly PacienteContext _contextPaciente;
        private readonly ConsultaContext _contextConsulta;

        public TurnoController(TurnoContext context, PacienteContext contextPaciente, ConsultaContext contextConsulta)
        {
            _context = context;
            _contextPaciente = contextPaciente;
            _contextConsulta = contextConsulta;
        }
        
        public async Task<IActionResult> subHome()
        {
            return View();
        }
        
        // GET: Index busca turnos de paciente por id.
        public async Task<IActionResult> Index(int? BusquedaTurno)
        {
            var turnos = await _context.Turno
                .Include(t => t.paciente)
                .Include(t => t.consulta) 
                .Where(u => u.idPaciente == BusquedaTurno)
                .ToListAsync();
            
            return View(turnos);
        }
        
        public async Task<IActionResult> Index2(int id)
        {
            var turnos = await _context.Turno
                .Include(t => t.paciente)
                .Include(t => t.consulta) 
                .Where(u => u.idPaciente == id)
                .ToListAsync();
            
            return View(turnos);
        }
        
        // GET: Turno/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Turno/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTurno,fecha,descripcion,idPaciente,idConsulta")] Turno turno)
        {
            _context.Add(turno);

            // Buscar el usuario en UsuarioContext
            var paciente = await _contextPaciente.Paciente.FindAsync(turno.idPaciente);
            var consulta = await _contextConsulta.Consulta.FindAsync(turno.idConsulta);

            if (paciente == null || consulta == null) return NotFound();

            // Asignar el usuario al turno
            turno.paciente = paciente;
            turno.consulta = consulta;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(subHome)); 
        }

        // GET: Turno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!TurnoExists(id))
            {
                return NotFound();
            }

            var turno = await _context.Turno.FindAsync(id);

            return View(turno);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTurno,fecha,idPaciente,idConsulta,paciente,consulta,descripcion")] Turno turno)
        {
            if (id != turno.IdTurno){return NotFound();}
            
            if (!TurnoExists(turno.IdTurno)){return NotFound();}

            try
            {
                // Cargar las entidades Paciente y Consulta desde la base de datos
                var paciente = await _context.Paciente.FindAsync(turno.idPaciente);
                var consulta = await _context.Consulta.FindAsync(turno.idConsulta);
                var idPaciente = turno.idPaciente;

                // Asignar las entidades cargadas al turno
                turno.paciente = paciente;
                turno.consulta = consulta;

                _context.Update(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index2), new {id=idPaciente});
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurnoExists(turno.IdTurno))
                {
                    return NotFound();
                }
                
                throw;
            }
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null){return NotFound();}
            
            var turno = await _context.Turno
                .Include(t => t.paciente)
                .Include(t => t.consulta)
                .FirstOrDefaultAsync(m => m.IdTurno == id);
            
            if (!TurnoExists(id)) {return NotFound();}

            return View(turno);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turno = await _context.Turno.Include(t => t.paciente)
                .FirstOrDefaultAsync(m => m.IdTurno == id);
            
            if (!TurnoExists(id)) {return NotFound();}

            var idPaciente = turno.idPaciente;
            
            _context.Turno.Remove(turno);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index2), new { id = idPaciente });
        }
        
        // FindByDNI busca turnos de paciente por DNI.
        public async Task<IActionResult> FindByDNI(int? BusquedaTurno)
        {
            ViewData["BusquedaTurno"] = BusquedaTurno;
            if (BusquedaTurno.HasValue && BusquedaTurno.ToString().Length == 8)
            {
                var turnos = await _context.Turno
                    .Include(t => t.paciente)
                    .Include(t => t.consulta).
                    Where(u => Convert.ToInt32(u.paciente.documento) == BusquedaTurno).ToListAsync();

                return View(turnos);
            }
    
            return RedirectToAction(nameof(subHome));
        }

        private bool TurnoExists(int? id)
        {
            return _context.Turno.Any(e => e.IdTurno == id);
        }
    }
}
