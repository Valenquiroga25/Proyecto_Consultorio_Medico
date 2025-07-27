using System.Windows;
using System.Windows.Controls;

namespace ProyectoTurnos_FrontEnd.MiApp.Views;

public partial class PaginaEstudios : Page
{
    private Frame mainFrame;
    
    public PaginaEstudios(Frame mainFrame)
    {
        InitializeComponent();
        this.mainFrame = mainFrame;
    }
    
    private void Volver(object sender, RoutedEventArgs routedEventArgs)
    {
        mainFrame.NavigationService.GoBack();
    }
    
    /*
    public void AgregarImagen(object sender, RoutedEventArgs e)
    {
        try
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Imagenes|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openDialog.FilterIndex = 1;

            if (openDialog.ShowDialog() == true)
            {
                
                if (!File.Exists(openDialog.FileName))
                {
                    MessageBox.Show("El archivo no existe.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                imagenesHistoria.Add(new ImagenEstudioDTO(documentoPaciente,Path.GetFileName(openDialog.FileName), new BitmapImage(new Uri(openDialog.FileName))));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ha ocurrido un error al cargar la imagen del FileSystem: " + ex.Message);
            throw;
        }
    }
    */
}