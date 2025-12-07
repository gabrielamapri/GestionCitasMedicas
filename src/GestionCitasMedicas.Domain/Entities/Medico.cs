using System.Collections.Generic;

namespace GestionCitasMedicas.Domain.Entities;

public class Medico
{
    public int Id { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public int? EspecialidadId { get; set; }
    public Especialidad? Especialidad { get; set; }

    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;

    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}
