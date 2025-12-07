using GestionCitasMedicas.Domain.Entities;

namespace GestionCitasMedicas.Infrastructure.Repositories;

public interface ICitaRepository : IRepository<Cita>
{
    Task<IEnumerable<Cita>> GetByPacienteAsync(int pacienteId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Cita>> GetByMedicoAsync(int medicoId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Cita>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
    Task<Cita?> GetWithIncludesAsync(int id, CancellationToken cancellationToken = default);
}
