using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTurnos.Models;

public class TurnoSlot
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    [Required]
    public DateTime fecha { get; set; }
    [Required]
    public TimeSpan hora { get; set; }
    
    public Turno? turno { get; set; }
    
    [Key]
    [Required]
    public int? idTurno { get; set; }

    public TurnoSlot()
    {
        
    }
    public TurnoSlot(DateTime fecha, TimeSpan hora, Turno? turno)
    {
        this.fecha = fecha;
        this.hora = hora;
        this.turno = turno;
        if(turno != null)
            idTurno = turno.idTurno;
    }
}