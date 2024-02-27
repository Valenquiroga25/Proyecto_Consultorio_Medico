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

            var usuarioValido = validarUsuario(usuario.nombreUsuario, usuario.contraseña);

            if (usuarioValido)
                return RedirectToAction("Home", "Home");
            
            return RedirectToAction("Login", "Login");
        }

        public bool validarUsuario(String? nombreUsuario, String? contraseña)
        {
            var acceso = _context.Acceso
                .Where(item => item.nombreUsuario == nombreUsuario && item.contraseña == contraseña).FirstOrDefault();

            return (acceso != null);
        }
    }
}