using System.Windows.Controls;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using ProyectoTurnos_FrontEnd.MiApp.Models;

namespace ProyectoTurnos_FrontEnd.MiApp.Views;

public partial class PaginaDatosPaciente : Page
{
    private Frame mainFrame;
    private string documentoPaciente;
    public HistoriaDTO historia { get; set; }
    
    public PaginaDatosPaciente(Frame mainFrame, string documentoPaciente)
    {
        InitializeComponent();
        this.mainFrame = mainFrame;
        this.documentoPaciente = documentoPaciente;
        Loaded += PaginaLoaded;
    }
    
    public async Task GetHistoriaClinica()
    {
        try
        {
            LoaderPanel.Visibility = Visibility.Visible; // Se muestra la página de carga mientras se recuperan los datos.
            PaginaDatos.Visibility = Visibility.Hidden;
            
            HttpClient httpClient = new HttpClient();
            string url = "http://localhost:8080/api/historia/buscar/" + documentoPaciente;

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Historia recibida con éxito!");
                Console.WriteLine("Respuesta: " + result);

                HistoriaDTO? historiaRecibida = JsonSerializer.Deserialize<HistoriaDTO>(result);

                if (historiaRecibida != null)
                {
                    historia = historiaRecibida;
                    historia.fechaNacimiento = historiaRecibida.fechaNacimiento.Substring(0,10);
                    DataContext = this; // Esto le indica a la UI que puede acceder a los datos de esta clase en el Binding.
                    LoaderPanel.Visibility = Visibility.Hidden;
                    PaginaDatos.Visibility = Visibility.Visible;
                }
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Ha ocurrido un error al listar las historias clínicas: " + e.Message);
        }
    }
    
    private void Volver(object sender, RoutedEventArgs routedEventArgs)
    {
        mainFrame.NavigationService.GoBack();
    }
    
    private async void PaginaLoaded(object sender, RoutedEventArgs args)
    {
        await GetHistoriaClinica();
    }
}