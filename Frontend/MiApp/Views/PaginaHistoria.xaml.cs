using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using ProyectoTurnos_FrontEnd.MiApp.Models;

namespace ProyectoTurnos_FrontEnd.MiApp.Views;


public partial class PaginaHistoria : Page
{
    private Frame mainFrame;
    private string documentoPaciente;
    public HistoriaDTO historia { get; set; }

    private int banderaWeight = 0;
    private int banderaStyle = 0;
    private double banderaSize = 14;
    private string banderaColor = "Black";

    public ObservableCollection<ImagenEstudioDTO> imagenesHistoria { get; set; }= new ObservableCollection<ImagenEstudioDTO>();
    public int edadPaciente { get; set; }
    
    public PaginaHistoria(Frame MainFrame, string documentoPaciente)
    {
        InitializeComponent();
        mainFrame = MainFrame;
        this.documentoPaciente = documentoPaciente;
        Loaded += PaginaLoaded;
    }

    public async Task GetHistoriaClinica()
    {
        try
        {
            LoaderPanel.Visibility = Visibility.Visible;
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
                    edadPaciente = CalcularEdadPaciente(historia.fechaNacimiento);
                    if(!historiaRecibida.descripcion.Equals(""))
                        CargarContenidoConFormato(historiaRecibida.descripcion); // Se coloca la descripción en el editor.
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

    public void RedirigirDatosPaciente(Object sender, RoutedEventArgs e)
    {
        mainFrame.Navigate(new PaginaDatosPaciente(mainFrame, documentoPaciente));
    }
    
    private async void PaginaLoaded(object sender, RoutedEventArgs args)
    {
        await GetHistoriaClinica();
    }

    public void Bold_Click(Object sender, RoutedEventArgs e)
    {
        // TextElement es una clase base que define los atributos y métodos que tienen todos los textos en WPF,
        // por eso se utiliza para enviar la propiedad de weight del texto que queremos cambiar.
        ToggleFormatting(TextElement.FontWeightProperty, FontWeights.Bold);
    }
    
    public void Italic_Click(Object sender, RoutedEventArgs e)
    {
        ToggleFormatting(TextElement.FontStyleProperty, FontStyles.Italic);
    }
    
    public void Underline_Click(Object sender, RoutedEventArgs e)
    {
        ToggleFormatting(Inline.TextDecorationsProperty, TextDecorations.Underline);
    }

    public void FontSizeComboBox_SelectionChanged(object sender, RoutedEventArgs e)
    {
        TextSelection selection = Editor.Selection;

        if (!selection.IsEmpty)
        {
            if (FontSizeComboBox.SelectedItem is ComboBoxItem item)
            {
                double size = double.Parse(item.Content.ToString());
                selection.ApplyPropertyValue(TextElement.FontSizeProperty, size);
                banderaSize = size;
            }
        }
        else
        {
            // Si no hay texto seleccionado, quiere decir que se quiere empezar a escribir con cierta propiedad activa.
            TextPointer puntero = Editor.CaretPosition;
            Run run = new Run();

            if (FontSizeComboBox.SelectedItem is ComboBoxItem item)
            {
                double size = double.Parse(item.Content.ToString());
                run.FontSize = size;
                banderaSize = size;
                CambiarColor(run, banderaColor);
                if(banderaWeight == 1)
                    run.FontWeight = FontWeights.Bold;
                if(banderaStyle == 1)
                    run.FontStyle = FontStyles.Italic;
                puntero.Paragraph.Inlines.Add(run);
                Editor.CaretPosition = run.ContentEnd;
                Editor.Focus();
            }        
        }
    }

    public void Color_Click(Object sender, RoutedEventArgs e)
    {
        TextSelection selection = Editor.Selection;
        Button? boton = sender as Button;

        if (!selection.IsEmpty && boton != null)
        {
            selection.ApplyPropertyValue(TextElement.ForegroundProperty, boton.Name);
        }
        else
        {
            if (boton != null)
            {
                TextPointer puntero = Editor.CaretPosition;
                Run run = new Run();
                CambiarColor(run, boton.Name);
                if(banderaWeight == 1)
                    run.FontWeight = FontWeights.Bold;
                if(banderaStyle == 1)
                    run.FontStyle = FontStyles.Italic;
                run.FontSize = banderaSize;
                puntero.Paragraph.Inlines.Add(run);
                Editor.CaretPosition = run.ContentEnd;
                Editor.Focus();
            }
        }
    }

    public void ToggleFormatting(DependencyProperty property, object value) // value hace referencia a 'FontStyles.Italic' o 'FontStyles.Bold'
    {
        var selection = Editor.Selection; // Representa el texto que uno marca con el mouse.
        
        if (!selection.IsEmpty)
        {
            var valorActual = selection.GetPropertyValue(property); // De eso que seleccionamos, sacamos el valor actual de la propiedad que estemos cambiando.

            if (value.Equals(FontWeights.Bold))
            {
                if (valorActual.Equals(value))
                {
                    selection.ApplyPropertyValue(property, FontWeights.Normal);
                    banderaWeight = 0;
                }
                else
                {
                    selection.ApplyPropertyValue(property, value);
                    banderaWeight = 1;
                }
                
            }else if (value.Equals(FontStyles.Italic))
            {
                if (valorActual.Equals(value))
                {
                    selection.ApplyPropertyValue(property, FontStyles.Normal);
                    banderaStyle = 0;
                }
                else
                {
                    selection.ApplyPropertyValue(property, value); 
                    banderaStyle = 1;
                }
            }
            else
            {
                TextDecorationCollection decs = (TextDecorationCollection)valorActual;

                if (valorActual.Equals(value))
                {
                    // TextDecorations.Underline[0] accede al primer (y único) objeto TextDecoration que representa el subrayado.
                    if (decs.Contains(TextDecorations.Underline[0]))
                    {
                        TextDecorationCollection noUnder = new TextDecorationCollection(decs);
                        noUnder.Remove(TextDecorations.Underline[0]);
                        selection.ApplyPropertyValue(Inline.TextDecorationsProperty, noUnder);
                    }
                }
                else
                    selection.ApplyPropertyValue(property, value); 
            }
        }
        else
        {
            // Si no hay texto seleccionado, quiere decir que se quiere empezar a escribir con cierta propiedad activa.
            TextPointer puntero = Editor.CaretPosition;

            if (value.Equals(FontWeights.Bold))
            {
                if (banderaWeight == 0)
                {
                    Run run = new Run(); // Crea una nueva instancia de Run, que representa una secuencia de texto
                    run.FontWeight = FontWeights.Bold;
                    if (banderaStyle == 1)
                        run.FontStyle = FontStyles.Italic;
                    
                    CambiarColor(run, banderaColor); // Se aplica el color activo.

                    run.FontSize = banderaSize; // Se aplica el tamaño activo.
                    puntero.Paragraph.Inlines.Add(run); // Agrega el Run recién creado al final del párrafo.
                    Editor.CaretPosition = run.ContentEnd; // Coloca el cursor (caret) al final del nuevo Run.
                    Editor.Focus(); // Asegura que el control Editor (el RichTextBox) tenga el foco del teclado para que el usuario pueda seguir escribiendo inmediatamente.
                    banderaWeight = 1;
                }
                else
                {
                    Run run = new Run();
                    run.FontWeight = FontWeights.Normal;
                    if (banderaStyle == 1)
                        run.FontStyle = FontStyles.Italic;
                    
                    CambiarColor(run, banderaColor); // Se aplica el color activo.

                    run.FontSize = banderaSize; // Se aplica el tamaño activo.
                    puntero.Paragraph.Inlines.Add(run);
                    Editor.CaretPosition = run.ContentEnd;
                    Editor.Focus();
                    banderaWeight = 0;
                }
            }else if (value.Equals(FontStyles.Italic))
            {
                if (banderaStyle == 0)
                {
                    Run run = new Run();
                    run.FontStyle = FontStyles.Italic;
                    
                    if (banderaWeight == 1)
                        run.FontWeight = FontWeights.Bold; // Si el bold está activado, se le agrega al nuevo run.
                    
                    CambiarColor(run, banderaColor); // Se aplica el color activo.

                    run.FontSize = banderaSize; // Se aplica el tamaño activo.
                    puntero.Paragraph.Inlines.Add(run);
                    Editor.CaretPosition = run.ContentEnd;
                    Editor.Focus();
                    banderaStyle = 1;
                }
                else
                {
                    Run run = new Run();
                    run.FontStyle = FontStyles.Normal;
                    if (banderaWeight == 1)
                        run.FontWeight = FontWeights.Bold;
                    
                    CambiarColor(run, banderaColor); // Se aplica el color activo.

                    run.FontSize = banderaSize;
                    puntero.Paragraph.Inlines.Add(run);
                    Editor.CaretPosition = run.ContentEnd;
                    Editor.Focus();
                    banderaStyle = 0; 
                }
            }
        }
    }

    public async void GuardarCambiosHistoria(object sender, RoutedEventArgs e)
    {
        try
        {
            
            string contenidoFormateado = ObtenerContenidoConFormato();
            historia.descripcion = contenidoFormateado;
            
            HistoriaDTO historiaDto = new HistoriaDTO(historia.nombres,
                historia.apellidos,
                historia.documentoPaciente,
                historia.fechaNacimiento,
                historia.codArea,
                historia.telefono,
                historia.direccion,
                historia.correo,
                historia.descripcion);
            await GuardarHistoria(historiaDto);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ha ocurrido un error al guardar los cambios de la historia: " + ex.Message);
        }
    }
    
    public async Task GuardarHistoria(HistoriaDTO historiaDto)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string url = "http://localhost:8080/api/historia/editar/" + documentoPaciente;
            
            var response = await httpClient.PostAsJsonAsync(url,historiaDto); // enviando el objeto historiaDto como JSON.

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync(); // Se lee el contenido de la respuesta como texto.
                Console.WriteLine("Respuesta: " + result);
                MessageBox.Show("Cambios guardados con éxito!");
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Ha ocurrido un error al guardar los cambios de la historia: " + e.Message);
        }
    }
    
    private int CalcularEdadPaciente(string fechaNacimiento)
    {
        try
        {
            DateTime fechaPaciente = DateTime.Parse(fechaNacimiento);
            int edad = DateTime.Now.Year - fechaPaciente.Year;
            Console.WriteLine(edad);

            if (DateTime.Now.Month < fechaPaciente.Month)
                edad -= 1;
            else if(DateTime.Now.Day < fechaPaciente.Day)
                edad -= 1;
            Console.WriteLine(edad);

            return edad;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ha ocurrido un error al calcular la edad del paciente: " + e.Message);
            throw;
        }
    }
    
    public string ObtenerContenidoConFormato() // Guardar el contenido del editor con formato.
    {
        TextRange range = new TextRange(Editor.Document.ContentStart, Editor.Document.ContentEnd);
       
        MemoryStream ms = new MemoryStream();
        range.Save(ms, DataFormats.Xaml); // Usa Xaml en vez de Text
        return Encoding.UTF8.GetString(ms.ToArray());
    }

    public void CargarContenidoConFormato(string contenidoXaml) // Cargar el contenido en el editor desde la cadena.
    {
        TextRange range = new TextRange(Editor.Document.ContentStart, Editor.Document.ContentEnd);
        
        MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(contenidoXaml));
        range.Load(ms, DataFormats.Xaml); // Usa el mismo formato con el que se guardó
    }

    private void CambiarColor(Run run, string color)
    {
        switch(color)
        {
            case "Black":
                run.Foreground = Brushes.Black;
                banderaColor = "Black";
                break;
            case "Red":
                run.Foreground = Brushes.Red;
                banderaColor = "Red";
                break;
            case "Blue":
                run.Foreground = Brushes.Blue;
                banderaColor = "Blue";
                break;
            case "Green":
                run.Foreground = Brushes.Green;
                banderaColor = "Green";
                break;
        }
    }

    private void RedirigirPaginaEstudios(object sender, RoutedEventArgs routedEventArgs)
    {
        mainFrame.Navigate(new PaginaItemsEstudios(mainFrame, documentoPaciente));
    }
    
    private void Volver(object sender, RoutedEventArgs routedEventArgs)
    {
        mainFrame.NavigationService.GoBack();
    }
}
