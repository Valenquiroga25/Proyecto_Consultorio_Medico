using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ProyectoTurnos_FrontEnd.MiApp.Models;
// Para poder generar el objeto de HttpClient
// Para poder usar .PostAsJsonAsync y .GetFromJsonAsync

namespace ProyectoTurnos_FrontEnd.MiApp.Views;

public partial class Login : Page
{
    public Frame mainFrame;

    public Login(Frame MainFrame)
    {
        mainFrame = MainFrame;
        InitializeComponent();
    }

    private async void
        OnClick(object sender, RoutedEventArgs e) // Parámetro Object es quien activó el evento (un botón por lo menos).
        //  Parámetro RoutedEventArgs tiene la información del evento.
    {
        var user = usuarioForm.Text; // Se puede llamar asi a cualquier info que se coloque en el xaml, gracias al x:Name
        var password = contraseniaForm.Password;

        var login = new LoginDTO { nombreUsuario = user, contrasenia = password };

        await ValidarUsuario(login);
    }

    public async Task ValidarUsuario(LoginDTO acceso) // Devuelve un Task, que representa una operación en curso.
    {
        var httpClient = new HttpClient();
        var url = "http://localhost:8080/api/login/iniciar";

        var response =
            await httpClient.PostAsJsonAsync(url, acceso); // Se le pasa la url de la API y el objeto de login.

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Respuesta: " + result);

            // Creación de instancia de la clase a la que queremos acceder a la hora de navegar.
            mainFrame.Navigate(new PaginaPrincipal(mainFrame, acceso.nombreUsuario));
        }
        else
        {
            MessageBox.Show("El usuario ingresado no es válido!", "Login fallido", MessageBoxButton.OK,
                MessageBoxImage.Error);
            contraseniaForm.Password = string.Empty;
            Console.WriteLine("Error: " + response.StatusCode);
        }
    }
    private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e) => ShowPasswordFunction();
    private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e) => HidePasswordFunction();
    private void ShowPassword_MouseLeave(object sender, MouseEventArgs e) => HidePasswordFunction();

    private void ShowPasswordFunction()
    {
        ShowPassword.Text = "Mostrar";
        PasswordUnmask.Visibility = Visibility.Visible;
        contraseniaForm.Visibility = Visibility.Hidden;
        PasswordUnmask.Text = contraseniaForm.Password;
    }

    private void HidePasswordFunction()
    {
        ShowPassword.Text = "Mostrar";
        PasswordUnmask.Visibility = Visibility.Hidden;
        contraseniaForm.Visibility = Visibility.Visible;
    }
}