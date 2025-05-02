using Microsoft.AspNetCore.Mvc;
using ProyectoTurnos.Models.DTOs;
using ProyectoTurnos.Models;

using ProyectoTurnos.Services;

namespace ProyectoTurnos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly AccesoService loginService;

    public LoginController(AccesoService loginService)
    {
        this.loginService = loginService;
    }
    
    [HttpPost("iniciar")]
    public IActionResult IniciarSesion([FromBody]AccesoDTO loginDTO)
    {
        Acceso login = new Acceso(loginDTO.nombreUsuario, loginDTO.contrasenia);
        bool esValido = loginService.ValidarUsuario(login);

        if (esValido)
        {
            Console.WriteLine("Acceso exitoso para el usuario " + login.nombreUsuario + "!");
            return Ok(new { succes = true });
        }
        
        return Unauthorized(new { succes = false });
    }
}