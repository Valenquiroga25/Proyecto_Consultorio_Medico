using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTurnos.Data;
using ProyectoTurnos.Models;

namespace ProyectoTurnos.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AccesoContext _context;

        public AccesoController(AccesoContext context)
        {
            _context = context;
        }

        public IActionResult Ingresar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ingresar(Acceso usuario)
        {

            if (usuarioValido(usuario.nombreUsuario,usuario.contraseña))
                return RedirectToAction("Home", "Home");
            
            return RedirectToAction("Ingresar", "Acceso");
        }

        public bool usuarioValido(String? nombreUsuario, String? contraseña)
        {
            return _context.Acceso.Any(e => e.nombreUsuario == nombreUsuario && e.contraseña == contraseña);
        }
    }
}