using System.IO;
using System.Windows.Media.Imaging;

namespace ProyectoTurnos_FrontEnd.MiApp.Models;

public class ImagenEstudioDTO
{
    public string nombreEstudio { get; set; }
    public string documentoPacienteEstudio { get; set; }

    public string fecha { get; set; }
    public byte[] imagen { get; set; }
    public string titulo { get; set; }

    public ImagenEstudioDTO(string nombreEstudio, string documentoPacienteEstudio, string fecha, BitmapImage imagen, string titulo)
    {
        this.nombreEstudio = nombreEstudio;
        this.documentoPacienteEstudio = documentoPacienteEstudio;
        this.fecha = fecha;
        this.imagen = BitmapImageAByte(imagen);
        this.titulo = titulo;
    }
    
    private byte[] BitmapImageAByte(BitmapImage bitmapImage)
    {
        byte[] data = null;
        string imagen = bitmapImage.ToString();
        string extension = Path.GetExtension(imagen);
        
        if (extension.Equals(".jpeg") || extension.Equals(".jpg"))
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Save(stream);
                data = stream.ToArray();
            }
        }else if (extension.Equals(".png"))
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Save(stream);
                data = stream.ToArray();
            }
        }

        return data;
    }

}