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
            var turnos = await _context.Turno
                .Include(t => t.paciente)
                .Include(t => t.consulta) 
                .ToListAsync();
            
            return View(turnos);        }
        
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
        
        // El index al que nos dirigimos cuando editamos o eliminamos un paciente buscado por "FindByDNI".
        public async Task<IActionResult> Index2(int id)
        {
            var turnos = await _context.Turno
                .Include(t => t.paciente)
                .Include(t => t.consulta) 
                .Where(u => u.idPaciente == id)
                .ToListAsync();
            
            return View(turnos);
        }
        
        // El index al que nos dirigimos cuando editamos o eliminamos un paciente buscado por "FindByFecha".
        public async Task<IActionResult> Index3(DateTime fecha)
        {
            var turnos = await _context.Turno
                .Include(t => t.paciente)
                .Include(t => t.consulta) 
                .Where(u => u.fecha == fecha)
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
        public async Task<IActionResult> Create([Bind("IdTurno,fecha,hora,descripcion,idPaciente,idConsulta")] Turno turno)
        {
            _context.Add(turno);

            var turnos = await _context.Turno.Where(t => t.fecha == turno.fecha).ToListAsync();

            if (!turnoValido(turnos, turno)){throw new Exception("Ya hay un turno en esa fecha ese dia.");}
            if (!PacienteExists(turno.idPaciente)){throw new Exception("El paciente no se encuentra registrado en la base de datos.");};
            if (!ConsultaExists(turno.idConsulta)){throw new Exception("La consulta no se encuentra registrado en la base de datos.");};

            // Buscar el usuario en UsuarioContext
            var paciente = await _contextPaciente.Paciente.FindAsync(turno.idPaciente);
            var consulta = await _contextConsulta.Consulta.FindAsync(turno.idConsulta);

            // Asignar el usuario al turno
            turno.paciente = paciente;
            turno.consulta = consulta;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(subHome)); 
        }

        // GET: Turno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!TurnoExists(id)){return NotFound();}

            var turno = await _context.Turno.FindAsync(id);

            return View(turno);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTurno,fecha,hora,idPaciente,idConsulta,paciente,consulta,descripcion")] Turno turno)
        {
            
            if (!TurnoExists(turno.IdTurno)){throw new Exception("El turno no se encuentra registrado en la base de datos.");}
            if (!PacienteExists(turno.idPaciente)){throw new Exception("El paciente no se encuentra registrado en la base de datos.");};
            if (!ConsultaExists(turno.idConsulta)){throw new Exception("La consulta no se encuentra registrado en la base de datos.");};

            // Cargar las entidades Paciente y Consulta desde la base de datos
            var paciente = await _context.Paciente.FindAsync(turno.idPaciente);
            var consulta = await _context.Consulta.FindAsync(turno.idConsulta);
            
            var idPaciente = turno.idPaciente; // Esta variable se crea para indicar el id al 'RedirectToAction'.

            // Asignar las entidades cargadas al turno
            turno.paciente = paciente;
            turno.consulta = consulta;

            _context.Update(turno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index2), new {id=idPaciente});
        }

        // Edit que usamos para los turnos buscados por fecha ("FindByFecha")
        public async Task<IActionResult> Edit2(int? id)
        {
            if (!TurnoExists(id)){return NotFound();}

            var turno = await _context.Turno.FindAsync(id);

            return View(turno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit2(int id, [Bind("IdTurno,fecha,hora,idPaciente,idConsulta,paciente,consulta,descripcion")] Turno turno)
        {
            
            if (!TurnoExists(turno.IdTurno)){throw new Exception("El turno no se encuentra registrado en la base de datos.");}
            if (!PacienteExists(turno.idPaciente)){throw new Exception("El paciente no se encuentra registrado en la base de datos.");};
            if (!ConsultaExists(turno.idConsulta)){throw new Exception("La consulta no se encuentra registrado en la base de datos.");};

            // Cargar las entidades Paciente y Consulta desde la base de datos
            var paciente = await _context.Paciente.FindAsync(turno.idPaciente);
            var consulta = await _context.Consulta.FindAsync(turno.idConsulta);
            
            // Asignar las entidades cargadas al turno
            turno.paciente = paciente;
            turno.consulta = consulta;

            var fecha = turno.fecha;
            
            _context.Update(turno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index3), new {fecha});
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
        
        // Delete que usamos para los turnos buscados por fecha ("FindByFecha")
        public async Task<IActionResult> Delete2(int? id)
        {
            if (id == null){return NotFound();}
            
            var turno = await _context.Turno
                .Include(t => t.paciente)
                .Include(t => t.consulta)
                .FirstOrDefaultAsync(m => m.IdTurno == id);
            
            if (!TurnoExists(id)) {return NotFound();}

            return View(turno);
        }
        
        // Este delete es para el metodo FindByFecha, para que al eliminar un turno nos redirija al listado de esa fecha.
        [HttpPost, ActionName("Delete2")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed2(int? id)
        {
            var turno = await _context.Turno.Include(t => t.paciente)
                .FirstOrDefaultAsync(m => m.IdTurno == id);
            
            if (!TurnoExists(id)) {return NotFound();}

            var fecha = turno.fecha;
            
            _context.Turno.Remove(turno);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index3), new {fecha});
        }
        
        // El index al que nos dirigimos cuando buscamos turno por DNI de paciente.
        public async Task<IActionResult> FindByDNI(int? BusquedaTurno)
        {
            ViewData["BusquedaTurno"] = BusquedaTurno;
            if (BusquedaTurno.HasValue && BusquedaTurno.ToString().Length == 8)
            {
                var dni = Convert.ToInt32(BusquedaTurno);
                var turnos = await _context.Turno
                    .Include(t => t.paciente)
                    .Include(t => t.consulta)
                    .Where(u => u.paciente.documento == dni)
                    .ToListAsync();

                return View(turnos);
            }

            return RedirectToAction(nameof(subHome));
        }
        
        // El index al que nos dirigimos cuando buscamos turno por fecha de turno.
        public async Task<IActionResult> FindByFecha(DateTime? BusquedaTurnoFecha)
        {
            ViewData["BusquedaTurno"] = BusquedaTurnoFecha;
            if (BusquedaTurnoFecha.HasValue)
            {
                var fecha = Convert.ToDateTime(BusquedaTurnoFecha);
                var turnos = await _context.Turno
                    .Include(t => t.paciente)
                    .Include(t => t.consulta)
                    .Where(u => u.fecha == fecha)
                    .ToListAsync();

                return View(turnos);
            }

            return RedirectToAction(nameof(subHome));
        }


        private bool TurnoExists(int? id)
        {
            return _context.Turno.Any(e => e.IdTurno == id);
        }
        
        private bool PacienteExists(int? id)
        {
            return _context.Paciente.Any(e => e.idPaciente == id);
        }
        
        private bool ConsultaExists(int? id)
        {
            return _context.Consulta.Any(e => e.IdConsulta == id);
        }

        private bool turnoValido(List<Turno> turnos, Turno turnoAdar)
        {
            foreach (Turno t in turnos)
            {
                if (t.fecha == turnoAdar.fecha && t.hora == turnoAdar.hora)
                    return false;
            }

            return true;
        }
    }
}
