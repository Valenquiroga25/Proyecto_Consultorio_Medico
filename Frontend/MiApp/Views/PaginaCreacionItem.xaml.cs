using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ProyectoTurnos_FrontEnd.MiApp.Models;

namespace ProyectoTurnos_FrontEnd.MiApp.Views;

public partial class PaginaCreacionItem : Page
{
    private readonly string documentoPaciente;
    private readonly Frame mainFrame;

    public PaginaCreacionItem(Frame mainFrame, string documentoPaciente)
    {
        InitializeComponent();
        this.mainFrame = mainFrame;
        this.documentoPaciente = documentoPaciente;
        DataContext = this;
    }

    public ObservableCollection<ImagenEstudioDTO> imagenesEstudio { get; set; } = new();

    private async void CrearRegistro(object sender, RoutedEventArgs routedEventArgs)
    {
        try
        {
            var httpClient = new HttpClient();
            var urlItem = "http://localhost:8080/api/itemestudio/generar";
            
            var (datosValidos, mensajeError) = validarDatos(fechaTextbox.Text, nombreEstudioTextbox.Text);

            if (!(bool)datosValidos)
            {
                MessageBox.Show(mensajeError);
                return;
            }

            var item = new ItemEstudioDTO(documentoPaciente, fechaTextbox.Text);
            EstudioDTO estudioDTO= new EstudioDTO(nombreEstudioTextbox.Text, documentoPaciente, fechaTextbox.Text);
            item.estudios.Add(estudioDTO);
            
            var responseItem = await httpClient.PostAsJsonAsync(urlItem, item);
            if (responseItem.IsSuccessStatusCode)
            {
                var result = await responseItem.Content.ReadAsStringAsync();
                Console.WriteLine("Resultado: " + result);
            }
            else
            {
                Console.WriteLine("Ha ocurrido un error con la llamada al endpoint 'urlItem'" +
                                  responseItem.StatusCode + " " + responseItem.Content.ReadAsStringAsync().Result);
                return;
            }

            var urlEstudio = "http://localhost:8080/api/estudio/generar";

            var responseEstudio = await httpClient.PostAsJsonAsync(urlEstudio, estudioDTO);

            if (responseEstudio.IsSuccessStatusCode)
            {
                var result = await responseEstudio.Content.ReadAsStringAsync();
                Console.WriteLine("Resultado: " + result);
                MessageBox.Show("Item generado con éxito!",  "Esaaaa", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                
                mainFrame.NavigationService.GoBack();
            }
            else
            {
                Console.WriteLine("Ha ocurrido un error con la llamada al endpoint 'urlEstudio'" +
                                  responseEstudio.StatusCode + " " +
                                  responseEstudio.Content.ReadAsStringAsync().Result);
                return;
            }

            if (!imagenesEstudio.Count.Equals(0))
            {
                var urlImagenes = "http://localhost:8080/api/imagenestudio/generar";

                var responseImagenes = await httpClient.PostAsJsonAsync(urlImagenes, imagenesEstudio);

                if (responseImagenes.IsSuccessStatusCode)
                {
                    var result = await responseEstudio.Content.ReadAsStringAsync();
                    Console.WriteLine("Resultado: " + result);
                    MessageBox.Show("Registro generado con éxito!");
                    mainFrame.NavigationService.GoBack();
                }
                else
                {
                    Console.WriteLine("Ha ocurrido un error con la llamada al endpoint 'urlImagenes'" +
                                      responseImagenes.StatusCode + " " +
                                      responseImagenes.Content.ReadAsStringAsync().Result);
                }
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Ha ocurrido un error al generar el registro: " + e.Message);
            Console.WriteLine("Ha ocurrido un error al generar el registro: " + e.Message);
        }
    }

    private void AgregarImagen(object sender, RoutedEventArgs routedEventArgs)
    {
        var openDialog = new OpenFileDialog();
        openDialog.Filter = "Image files | *.jpg;*.jpeg;*.png;*.pdf";
        openDialog.FilterIndex = 1;

        if (openDialog.ShowDialog() == true)
            imagenesEstudio.Add(new ImagenEstudioDTO(nombreEstudioTextbox.Text, documentoPaciente, fechaTextbox.Text,
                new BitmapImage(new Uri(openDialog.FileName)), openDialog.SafeFileName));
    }

    private void FormatearTexto(object sender, TextChangedEventArgs e)
    {
        try
        {
            var texto = fechaTextbox.Text;

            if (texto.Length == 2)
                texto += "    ";
            else if (texto.Length == 8)
                texto += "    ";
            else if (texto.Length >= 8 && texto.Length < 12)
                texto = texto.Substring(0, 7);
            else if (texto.Length >= 2 && texto.Length < 6) texto = texto.Substring(0, 1);

            fechaTextbox.Text = texto;
            fechaTextbox.Select(fechaTextbox.Text.Length, 0);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ha ocurrido un error al formatear la fecha: " + ex.Message);
            throw;
        }
    }

    private (bool?, string?) validarDatos(string? fecha, string? nombreEstudio)
    {
        if (fecha == null || fecha.Equals("") || nombreEstudio == null || nombreEstudio.Equals(""))
            return (false, "Registro inválido, los datos obligatorios (con asteriscos) deben contener un valor!");

        List<char> numerosEnTexto = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0'];
        foreach (var caracter in numerosEnTexto)
            if (nombreEstudio.Contains(caracter))
                return (false, "El nombre del estudio solo debe estar conformado por letras!");

        fecha = FormatearFecha(fecha);
        var fechaValida = DateTime.TryParseExact(fecha, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None,
            out var dt);
        if (!fechaValida)
            return (false, "La fecha colocada es inválida. Coloque una fecha correcta! \n(con formato dd/MM/yyyy)");

        return (true, "");
    }

    private string FormatearFecha(string texto)
    {
        if (texto.Length == 16)
        {
            texto = texto.Replace(" ", "");
            texto = texto.Insert(2, "/").Insert(5, "/");
        }

        return texto;
    }

    private void Volver(object sender, RoutedEventArgs routedEventArgs)
    {
        mainFrame.NavigationService.GoBack();
    }
}