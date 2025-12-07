using GestionCitasMedicas.API.Application.DTOs.Medico;

namespace GestionCitasMedicas.API.Application.Interfaces;

public interface IMedicoService
{
    Task<IEnumerable<MedicoDto>> GetAllAsync();
    Task<MedicoDto?> GetByIdAsync(int id);
    Task<MedicoDto> CreateAsync(CreateMedicoDto dto);
    Task<MedicoDto?> UpdateAsync(int id, UpdateMedicoDto dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<MedicoDto>> SearchByEspecialidadAsync(int especialidadId);
    Task<MedicoDto?> GetByEmailAsync(string email);
}
