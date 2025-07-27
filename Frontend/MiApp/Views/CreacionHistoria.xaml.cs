using System.Globalization;
using System.Net.Http;
using System.Windows.Controls;
using ProyectoTurnos_FrontEnd.MiApp.Models;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;

namespace ProyectoTurnos_FrontEnd.MiApp.Views;

public partial class CreacionHistoria : Page
{
    private Frame mainFrame;
    private static readonly Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
    private static readonly Regex regexDireccion = new Regex("^[a-zA-Z0-9\\sáéíóúÁÉÍÓÚñÑ.&]+$");
    public CreacionHistoria(Frame mainFrame)
    {
        InitializeComponent();
        this.mainFrame = mainFrame;
    }
    
    public async void RegistrarHistoriaClinica(object sender, RoutedEventArgs routedEventArgs)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string url = "http://localhost:8080/api/historia/generar";

            var (datosValidos, mensajeError) = validarDatos(nombresTextbox.Text,
                apellidosTextbox.Text,
                documentoTextbox.Text,
                fechaNacimientoTextbox.Text,
                codAreaTextbox.Text,
                telefonoTextbox.Text,
                direccionTextbox.Text,
                correoTextbox.Text);

            bool validos = (bool)datosValidos;
            
            if (!validos)
            {
                MessageBox.Show(mensajeError, "Error al cargar datos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string nombreCapitalizado = Capitalizar(nombresTextbox.Text);
            string apellidoCapitalizado = Capitalizar(apellidosTextbox.Text);
            HistoriaDTO historiaDto = new HistoriaDTO(nombreCapitalizado,
                apellidoCapitalizado,
                documentoTextbox.Text,
                fechaNacimientoTextbox.Text,
                codAreaTextbox.Text,
                telefonoTextbox.Text,
                direccionTextbox.Text,
                correoTextbox.Text,
                "");
        
            var response = await httpClient.PostAsJsonAsync(url, historiaDto);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Se ha producido un error al encontrar la API: " + response);
                MessageBox.Show("Se ha producido un error al encontrar la API: " + response);
            }
        
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Respuesta: " + result);
            MessageBox.Show("Historia Clínica generada con éxito!");
            mainFrame.NavigationService.GoBack();
        }
        catch (Exception e)
        {
            MessageBox.Show("Ha ocurrido un error al generar la historia clínica: " + e.Message);
            Console.WriteLine("Ha ocurrido un error al generar la historia clínica: " + e.Message);
        }
    }

    private (bool? valido, string? mensajeError) validarDatos(string? nombres, string? apellidos, string? documentoPaciente, string? fechaNacimiento, 
         string? codArea,string? telefono, string? direccion, string? correo)
    {
        if (string.IsNullOrWhiteSpace(nombres) || string.IsNullOrWhiteSpace(apellidos) || string.IsNullOrWhiteSpace(documentoPaciente) || string.IsNullOrWhiteSpace(fechaNacimiento) || string.IsNullOrWhiteSpace(codArea) || string.IsNullOrWhiteSpace(telefono))
        {
            return (false, "Registro inválido, los datos obligatorios (con asteriscos) deben contener un valor!");
        }

        List<char> numerosEnTexto = ['1','2','3','4','5','6','7','8','9','0'];
        foreach (char caracter in numerosEnTexto)
        {
            if (nombres.Contains(caracter))
                return (false, "El nombre del paciente solo debe estar conformado por letras!");
            if (apellidos.Contains(caracter))
                return (false, "El apellido del paciente solo debe estar conformado por letras!");
        }
        
        if(documentoPaciente.Length < 8)
            return (false, "El documento debe tener 8 caracteres!");
        
        // 'IsMatch' dice "Si el texto contiene los caracteres definidos en el atributo 'Regex' de esta clase, entra al if".
        if (regex.IsMatch(documentoPaciente))
            return (false, "El documento solo debe estar conformado por números!");

        fechaNacimiento = FormatearFecha(fechaNacimiento);
        bool fecha = DateTime.TryParseExact(fechaNacimiento, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime dt);
        if (!fecha)
        {
            return (false, "La fecha colocada es inválida. Coloque una fecha correcta! \n(con formato dd/MM/yyyy)");
        }

        if (!string.IsNullOrWhiteSpace(codArea))
        {
            if (regex.IsMatch(codArea))
                return (false, "El código de área solo debe estar conformado por números!");
        }
        
        if (!string.IsNullOrWhiteSpace(telefono))
        {
            if (regex.IsMatch(telefono))
                return (false, "El teléfono solo debe estar conformado por números!");
        }   
        
        if (!string.IsNullOrWhiteSpace(direccion))
        {
            if (!regexDireccion.IsMatch(direccion))
                return (false, "La dirección no debe tener caracteres especiales!");
        }     
        
        if (!string.IsNullOrWhiteSpace(correo))
        {
            if (!correo.Contains('@') || !correo.Contains('.'))
                return (false, "Correo inválido! Coloque un correo correcto \n(Debe contener los siguientes caracteres: '@' y '.')");
        }     

        return (true, "");
    }

    private void Cancelar(object sender, RoutedEventArgs routedEventArgs)
    {
        mainFrame.NavigationService.GoBack();
    }
    
    private string Capitalizar(string texto)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(texto.ToLower());
    }

    private void FormatearTexto(object sender, TextChangedEventArgs e)
    {
        try
        {
            string texto = fechaNacimientoTextbox.Text;

            if (texto.Length == 2)
            {
                texto += "    ";
            } else if (texto.Length == 8)
            {
                texto += "    ";
            }else if (texto.Length >= 8 && texto.Length < 12)
            {
                texto = texto.Substring(0,7);
            }else if (texto.Length >= 2 && texto.Length < 6)
            {
                texto = texto.Substring(0,1);
            }

            fechaNacimientoTextbox.Text = texto;
            fechaNacimientoTextbox.Select(fechaNacimientoTextbox.Text.Length,0);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ha ocurrido un error al formatear la fecha: " + ex.Message);
            throw;
        }
    }

    private string FormatearFecha(string texto)
    {
        if (texto.Length == 16)
        {
            texto = texto.Replace(" ", "");
            texto = texto.Insert(2,"/").Insert(5,"/");
        }
            
        return texto;
    }
}