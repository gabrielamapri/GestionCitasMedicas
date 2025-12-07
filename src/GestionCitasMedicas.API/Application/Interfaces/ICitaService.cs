using GestionCitasMedicas.API.Application.DTOs.Cita;

namespace GestionCitasMedicas.API.Application.Interfaces;

public interface ICitaService
{
    Task<IEnumerable<CitaDto>> GetAllAsync();
    Task<CitaDto?> GetByIdAsync(int id);
    Task<CitaDto> CreateAsync(CreateCitaDto dto);
    Task<CitaDto?> UpdateAsync(int id, CreateCitaDto dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<CitaDto>> GetByPacienteAsync(int pacienteId);
    Task<IEnumerable<CitaDto>> GetByMedicoAsync(int medicoId);
    Task<IEnumerable<CitaDto>> GetByDateAsync(DateTime date);
    Task<bool> CancelAsync(int id);
}
