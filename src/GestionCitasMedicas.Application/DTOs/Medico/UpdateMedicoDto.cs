namespace GestionCitasMedicas.Application.DTOs.Medico;

public class UpdateMedicoDto
{
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public int? EspecialidadId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
}
