using System.Windows;
using System.Windows.Controls;
using System.Net.Http;              // Para poder generar el objeto de HttpClient
using System.Net.Http.Json;        // Para poder usar .PostAsJsonAsync y .GetFromJsonAsync
using ProyectoTurnos_FrontEnd.MiApp.Models;

namespace ProyectoTurnos_FrontEnd.MiApp.Views;

public partial class Login : Page
{
    public Frame mainFrame;
    
    public Login(Frame MainFrame)
    {
        mainFrame = MainFrame;
        InitializeComponent();
    }

    private async void OnClick(Object sender, RoutedEventArgs e) // Parámetro Object es quien activó el evento (un botón por lo menos).
                                                                //  Parámetro RoutedEventArgs tiene la información del evento.
    {
        string user = usuarioForm.Text; // Se puede llamar asi a cualquier info que se coloque en el xaml, gracias al x:Name
        string password = contraseniaForm.Text;

        LoginDTO login = new LoginDTO{nombreUsuario = user, contrasenia = password};

        await ValidarUsuario(login);
    }

    public async Task ValidarUsuario(LoginDTO acceso) // Devuelve un Task, que representa una operación en curso.
    {
        HttpClient httpClient = new HttpClient();
        string url = "http://localhost:8080/api/login/iniciar";

        var response = await httpClient.PostAsJsonAsync(url, acceso); // Se le pasa la url de la API y el objeto de login.

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Respuesta: " + result);
            
            // Creación de instancia de la clase a la que queremos acceder a la hora de navegar.
            mainFrame.Navigate(new PaginaPrincipal(mainFrame, acceso.nombreUsuario));
        }
        else
        {
            MessageBox.Show("El usuario ingresado no es válido!", "Login fallido", MessageBoxButton.OK, MessageBoxImage.Error);
            Console.WriteLine("Error: " + response.StatusCode);
        }
    }
}