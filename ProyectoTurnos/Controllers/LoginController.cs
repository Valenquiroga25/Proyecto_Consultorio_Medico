using Microsoft.AspNetCore.Mvc;

namespace ProyectoTurnos.Controllers;

public class LoginController : Controller
{
    public IActionResult Login()
    {
        return View();
    }
}