namespace ProyectoTurnos.Models.DTOs;

public class ImagenEstudioDTO
{
    public string documentoPacienteHistoria { get; set; }
    public byte[] imagen { get; set; }

    public ImagenEstudioDTO() {}

    public ImagenEstudioDTO(string documentoPacienteHistoria, byte[] imagen)
    {
        this.documentoPacienteHistoria = documentoPacienteHistoria;
        this.imagen = imagen;
    }
}