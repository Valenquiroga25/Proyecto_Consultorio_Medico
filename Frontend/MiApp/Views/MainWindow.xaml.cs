using System.Windows;
using ProyectoTurnos_FrontEnd.MiApp.Views;

namespace ProyectoTurnos_FrontEnd;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Se navega a la página de Login, que es la primera del programa. (Siempre se pasa el frame en la navegación) 
        MainFrame.Navigate(new Login(MainFrame));
    }
}