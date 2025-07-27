using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Net.Http;
using System.Text.Json;
using ProyectoTurnos_FrontEnd.MiApp.Models;

namespace ProyectoTurnos_FrontEnd.MiApp.Views;

public partial class PaginaPrincipal : Page
{
    private Frame mainFrame;
    public ObservableCollection<HistoriaDTO> historias { get; set; } = new ObservableCollection<HistoriaDTO>();

    public PaginaPrincipal(Frame MainFrame, string nombreUsuario)
    {
        InitializeComponent();
        mainFrame = MainFrame;
        Titulo.Text = "Bienvenida " + nombreUsuario + "!";
        TablaHistorias.ItemsSource = historias;
        Loaded += PaginaLoaded;
    }

    private void OnClick(Object sender, RoutedEventArgs e)
    {
        string? nombreBoton = sender.ToString();

        // Siempre al chequear el contenido de los botones tienen que ir de nombres más cortos a más largos!
        if (nombreBoton.Substring(32, 16).Equals("Agenda de Turnos"))
        {
            MessageBox.Show("Esta funcionalidad estará disponible proximamente!", "Proximamente", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        
        if (nombreBoton.Substring(32, 24).Equals("Generar Historia Clínica"))
            mainFrame.Navigate(new CreacionHistoria(mainFrame));
    }
    
    public async Task GetHistoriasClinicas()
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string url = "http://localhost:8080/api/historia/listar";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                
                List<HistoriaDTO>? historiasList = JsonSerializer.Deserialize<List<HistoriaDTO>>(result); // Convierte cada objeto de json en objeto de C#
                
                historias.Clear();
                
                foreach (HistoriaDTO historia in historiasList)
                {
                    historias.Add(historia);
                }
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Ha ocurrido un error al listar las historias clínicas: " + e.Message);
            Console.WriteLine("Ha ocurrido un error al listar las historias clínicas: " + e.Message);
            throw;
        }
    }
    
    public void GetHistoriasFiltradas(object sender, TextChangedEventArgs args)
    {
        try
        {
            string contenidoFiltro = Filtro.Text.Trim();

            ListaHistorias.Visibility = Visibility.Visible;
            if (!string.IsNullOrEmpty(contenidoFiltro))
            {
                List<char> numerosEnTexto = ['1','2','3','4','5','6','7','8','9','0'];
                bool contieneNumero = false;
            
                foreach (char caracter in numerosEnTexto)
                {
                    if (contenidoFiltro.Contains(caracter)) 
                        contieneNumero = true;
                }

                if (contieneNumero)
                {
                    var historiasFiltradas = historias.Where(his => his.documentoPaciente.Contains(contenidoFiltro));
                    TablaHistorias.ItemsSource = new ObservableCollection<HistoriaDTO>(historiasFiltradas);  
                }
                else
                {
                    string capitalizado = Capitalizar(contenidoFiltro);
                    var historiasFiltradas = historias.Where(his => his.apellidos.Contains(capitalizado) || his.nombres.Contains(capitalizado));
                    TablaHistorias.ItemsSource = new ObservableCollection<HistoriaDTO>(historiasFiltradas);  
                }
            }
            else
            {
                ListaHistorias.Visibility = Visibility.Hidden;
                TablaHistorias.ItemsSource = historias;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ha ocurrido un error al filtrar los pacientes: " + ex.Message);
            Console.WriteLine("Ha ocurrido un error al filtrar los pacientes: " + ex.Message);
            throw;
        }
    }

    // Al estar dentro de un DataTemplate, necesitamos obtener los datos del DataContext (que sería la instancia de la historia), y no utilizando x:name como siempre 
    // ya que por cada item de la lista habria un x:name (por cada instancia), por lo que usando x:name el backend no sabria a que instancia nos refereimos. 
    public void RedirigirAHistoria(object sender, RoutedEventArgs routedEventArgs)
    {
        var boton = sender as Button;

        if (boton != null)
        {
            // El DataContext del botón es el objeto que estás bindeando en el DataTemplate. Se lo iguala a la clase que queremos usar.
            var historia = boton.DataContext as HistoriaDTO;

            if (historia != null)
            {
                Filtro.Text = "";
                mainFrame.Navigate(new PaginaHistoria(mainFrame, historia.documentoPaciente));
            }
        }
    }
    
    private async void PaginaLoaded(object sender, RoutedEventArgs e)
    {
        await GetHistoriasClinicas();
    }
    
    private string Capitalizar(string texto)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(texto.ToLower());
    }
}