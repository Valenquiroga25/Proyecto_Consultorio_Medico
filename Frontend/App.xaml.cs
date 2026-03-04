using System.Windows;

namespace ProyectoTurnos_FrontEnd;

// Clase que se ejecuta para comenzar el programa.

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}