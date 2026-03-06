using Microsoft.EntityFrameworkCore;
using ProyectoTurnos.Data;
using ProyectoTurnos.Services;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args); // Se crea la aplicacion
        
        // Se agrega la BD (el contexto). Se especifica que la BD debe ser MySQL y se debe utilizar el connection string "Default" de appsettings
        builder.Services.AddDbContext<Context>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'Default' not found."),ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default"))));
        
        // Esto basicamente agrega las entidades al contenedor de Entity Framework para la inyeccion de dependencias.
        
        builder.Services.AddScoped<AccesoService>(); // También le dice al contenedor: “cuando alguien necesite AccesoService, créalo automáticamente”. Sirve para instanciar los Controllers.
        builder.Services.AddScoped<CalendarioService>(); 
        builder.Services.AddScoped<PacienteService>();
        builder.Services.AddScoped<HistoriaService>();
        builder.Services.AddScoped<ItemEstudioService>();
        builder.Services.AddScoped<EstudioService>();
        builder.Services.AddScoped<ImagenEstudioService>();
        builder.Services.AddScoped<TurnoService>();

        builder.Services.AddControllers(); // Registra los controladores con atributos [ApiController].
        
        var app = builder.Build(); // Se construye la aplicación web con la configuración que hiciste en el builder.

        app.UseRouting(); // Activa el sistema de ruteo (es decir, cómo se manejan las URLs entrantes). Esto permite que ASP.NET Core encuentre el controlador correcto para cada solicitud.

        app.UseAuthorization();

        app.MapControllers(); // Registra las rutas para todos los controladores que usen [ApiController] y [Route(...)]. Sin esto, tus rutas como /api/login/iniciar no van a funcionar.

        app.Run();
    }
}