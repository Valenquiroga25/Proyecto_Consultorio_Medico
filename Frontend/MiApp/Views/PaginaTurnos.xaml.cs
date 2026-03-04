using System.Windows.Controls;

namespace ProyectoTurnos_FrontEnd.MiApp.Views;

public partial class PaginaTurnos : Page
{
    private Frame mainFrame;

    public PaginaTurnos(Frame MainFrame)
    {
        InitializeComponent();
        mainFrame = MainFrame;
    }
}