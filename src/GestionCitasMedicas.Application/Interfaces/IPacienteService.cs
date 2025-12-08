using GestionCitasMedicas.Application.DTOs.Paciente;

namespace GestionCitasMedicas.Application.Interfaces;

public interface IPacienteService
{
    Task<IEnumerable<PacienteDto>> GetAllAsync();
    Task<PacienteDto?> GetByIdAsync(int id);
    Task<PacienteDto> CreateAsync(CreatePacienteDto dto);
    Task<PacienteDto?> UpdateAsync(int id, UpdatePacienteDto dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<PacienteDto>> SearchByNameAsync(string name);
    Task<PacienteDto?> GetByEmailAsync(string email);
}
