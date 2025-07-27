using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProyectoTurnos_FrontEnd.MiApp.Models;

namespace ProyectoTurnos_FrontEnd.MiApp.Views;

public partial class PaginaItemsEstudios : Page
{
    private Frame mainFrame;
    private string documentoPaciente;
    private ObservableCollection<ItemEstudioDTO> items = new ObservableCollection<ItemEstudioDTO>();

    public PaginaItemsEstudios(Frame mainFrame, string documentoPaciente)
    {
        InitializeComponent();
        this.mainFrame = mainFrame;
        this.documentoPaciente = documentoPaciente;
        DataContext = this; // Esto le indica a la UI que puede acceder a los datos de esta clase en el Binding.
        Loaded += PaginaLoaded;
    }
    
    public async Task GetItems()
    {
        try
        {
            LoaderPanel.Visibility = Visibility.Visible;
            PaginaDatos.Visibility = Visibility.Hidden;
            HttpClient httpClient = new HttpClient();
            string urlItems = "http://localhost:8080/api/itemestudio/listar/" + documentoPaciente;

            var response = await httpClient.GetAsync(urlItems);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Items recibidos con éxito!");
                Console.WriteLine("Respuesta: " + result);

                List<ItemEstudioDTO>? itemsRecibidos = JsonSerializer.Deserialize<List<ItemEstudioDTO>>(result);

                if (itemsRecibidos != null)
                {
                    foreach(ItemEstudioDTO itemAgregar in itemsRecibidos)
                    {
                        itemAgregar.fecha = itemAgregar.fecha.Substring(0, 10);
                        items.Add(itemAgregar);
                    }

                    ListaItemsEstudio.ItemsSource = items;
                    
                    LoaderPanel.Visibility = Visibility.Hidden;
                    PaginaDatos.Visibility = Visibility.Visible;

                    if (itemsRecibidos.Count > 0)
                        ListaItemsEstudio.Visibility = Visibility.Visible;
                }
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Ha ocurrido un error al listar los items de la historia clínica: " + e.Message);
        }
    }
    
    private async void GridConEstudios_Loaded(object sender, RoutedEventArgs e)
    {
        var grid = sender as Grid;
        if (grid == null) return;

        var itemEstudio = grid.DataContext as ItemEstudioDTO;
        if (itemEstudio == null) return;

        var itemsControl = FindChild<ItemsControl>(grid, "ListaEstudios");
        if (itemsControl == null)
        {
            Console.WriteLine("No se encontró ListaEstudios");
            return;
        }

        using var httpClient = new HttpClient();
        string urlEstudios = "http://localhost:8080/api/estudio/listar/";

        try
        {
            var response = await httpClient.PostAsJsonAsync(urlEstudios, itemEstudio);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var estudiosRecibidos = JsonSerializer.Deserialize<List<EstudioDTO>>(result);
                
                foreach(EstudioDTO est in estudiosRecibidos)
                {
                   // estudios.Add(est);
                }

                //itemsControl.ItemsSource = estudios;
            }
            else
            {
                Console.WriteLine($"Error al cargar estudios: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    
    public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
    {
        if (parent == null) return null;

        int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
        for (int i = 0; i < childrenCount; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);

            if (child is T t && (child as FrameworkElement)?.Name == childName)
                return t;

            var foundChild = FindChild<T>(child, childName);
            if (foundChild != null)
                return foundChild;
        }

        return null;
    }

    
    private async void PaginaLoaded(object sender, RoutedEventArgs args)
    {
        await GetItems();
    }

    private void RedirigirACreacionItem(object sender, RoutedEventArgs routedEventArgs)
    {
        mainFrame.Navigate(new PaginaCreacionItem(mainFrame, documentoPaciente));
    }
    
    private void Volver(object sender, RoutedEventArgs routedEventArgs)
    {
        mainFrame.NavigationService.GoBack();
    }
}