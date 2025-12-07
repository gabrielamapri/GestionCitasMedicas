namespace GestionCitasMedicas.API.Application.DTOs.Cita;

public class CreateCitaDto
{
    public DateTime FechaHora { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public int PacienteId { get; set; }
    public int MedicoId { get; set; }
}
