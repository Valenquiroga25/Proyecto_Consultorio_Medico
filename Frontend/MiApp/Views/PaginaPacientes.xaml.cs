using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using ProyectoTurnos_FrontEnd.MiApp.Models;

namespace ProyectoTurnos_FrontEnd.MiApp.Views;

public partial class PaginaPacientes : Page
{
    //Se utiliza 'ObservableCollection' porque notifica a la interfaz gráfica (UI)
    //cuando cambia (se agregan, eliminan o modifican elementos), la lista normal no.
    private readonly ObservableCollection<PacienteDTO> pacientes = new();
    private Frame mainFrame;

    public PaginaPacientes(Frame MainFrame)
    {
        InitializeComponent();
        mainFrame = MainFrame;
        TablaPacientes.ItemsSource =
            pacientes; // Se inicializa por única vez el recurso de la tabla apenas se renderiza la página.
        Loaded += PaginaLoaded;
    }

    public async Task GetPacientes()
    {
        try
        {
            var httpClient = new HttpClient();
            var url = "http://localhost:8080/api/paciente/listar";

            var response = await httpClient.GetAsync(url); // Agarra la respuesta http.

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Pacientes recibidos con éxito!");
                Console.WriteLine("Respuesta: " + result);

                // Se convierte el Json a objeto de C# (en este caso el json de los pacientes a un List<Paciente>)
                // JsonSerializer se encarga de serializar la respuesta (convertirla a objetos C#)
                var pacientesList = JsonSerializer.Deserialize<List<PacienteDTO>>(result);
                pacientes.Clear();

                foreach (var paciente in
                         pacientesList) // Se agrgan los pacientes actuales en la lista por cada vez que se refresca la página.
                    pacientes.Add(
                        paciente); // Se agrgan los pacientes actuales en la lista por cada vez que se refresca la página.
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Hubo un error al recibir a los pacientes: " + e.Message);
            Console.WriteLine("Hubo un error al recibir a los pacientes: " + e.Message);
        }
    }

    public async void PaginaLoaded(object sender, RoutedEventArgs e)
    {
        await GetPacientes();
    }

    public void GetPacientesFiltrados(object sender, TextChangedEventArgs args)
    {
        try
        {
            var contenidoFiltro = Filtro.Text.Trim();

            if (!string.IsNullOrEmpty(contenidoFiltro))
            {
                var pacientesFiltrados = pacientes.Where(pac => pac.nombreCompleto.Contains(contenidoFiltro));

                TablaPacientes.ItemsSource = new ObservableCollection<PacienteDTO>(pacientesFiltrados);
            }
            else
            {
                TablaPacientes.ItemsSource = pacientes;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ha ocurrido un error al filtrar los pacientes: " + ex.Message);
            Console.WriteLine("Ha ocurrido un error al filtrar los pacientes: " + ex.Message);
        }
    }
}