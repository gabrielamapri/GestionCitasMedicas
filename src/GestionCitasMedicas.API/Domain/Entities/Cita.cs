using System;

namespace GestionCitasMedicas.Domain.Entities;

public class Cita
{
    public int Id { get; set; }
    public DateTime FechaHora { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public string Estado { get; set; } = "Programada";
    public string? Observaciones { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public int PacienteId { get; set; }
    public Paciente? Paciente { get; set; }

    public int MedicoId { get; set; }
    public Medico? Medico { get; set; }

    public bool IsFutureAppointment() => FechaHora > DateTime.Now;
    public bool CanBeCancelled() => Estado == "Programada" && IsFutureAppointment();
}
