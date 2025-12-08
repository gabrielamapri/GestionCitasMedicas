using GestionCitasMedicas.Domain.Entities;

namespace GestionCitasMedicas.Ports.Out;

public interface IMedicoRepository : IRepository<Medico>
{
    Task<IEnumerable<Medico>> GetByEspecialidadAsync(int especialidadId, CancellationToken cancellationToken = default);
    Task<Medico?> GetWithEspecialidadAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> IsAvailableAsync(int medicoId, DateTime start, DateTime end, CancellationToken cancellationToken = default);
}
