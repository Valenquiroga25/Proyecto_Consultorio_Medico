namespace ProyectoTurnos.Models.DTOs;

public class ImagenHistoriaDTO
{
    public string documentoPacienteHistoria { get; set; }
    public byte[] imagen { get; set; }

    public ImagenHistoriaDTO() {}

    public ImagenHistoriaDTO(string documentoPacienteHistoria, byte[] imagen)
    {
        this.documentoPacienteHistoria = documentoPacienteHistoria;
        this.imagen = imagen;
    }
}