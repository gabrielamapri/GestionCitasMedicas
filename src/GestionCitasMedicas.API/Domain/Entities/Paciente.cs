using System;
using System.Collections.Generic;

namespace GestionCitasMedicas.Domain.Entities;

public class Paciente
{
    public int Id { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string DocumentoIdentidad { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}
