namespace GestionCitasMedicas.API.Application.DTOs.Cita;

public class CitaDto
{
    public int Id { get; set; }
    public DateTime FechaHora { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string? Observaciones { get; set; }
    public DateTime FechaRegistro { get; set; }
    public int PacienteId { get; set; }
    public int MedicoId { get; set; }
}
