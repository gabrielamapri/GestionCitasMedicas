using Microsoft.EntityFrameworkCore;
using GestionCitasMedicas.Domain.Entities;
using GestionCitasMedicas.Infrastructure.Persistence.Context;

namespace GestionCitasMedicas.Infrastructure.Repositories;

public class MedicoRepository : Repository<Medico>, IMedicoRepository
{
    public MedicoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Medico>> GetByEspecialidadAsync(int especialidadId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(m => m.EspecialidadId == especialidadId).ToListAsync(cancellationToken);
    }

    public async Task<Medico?> GetWithEspecialidadAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Include(m => m.Especialidad).FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<bool> IsAvailableAsync(int medicoId, DateTime start, DateTime end, CancellationToken cancellationToken = default)
    {
        // available if there are no citas overlapping [start, end)
        var any = await _context.Set<Cita>()
            .Where(c => c.MedicoId == medicoId && c.Estado == "Programada")
            .AnyAsync(c => c.FechaHora < end && c.FechaHora.AddMinutes(30) > start, cancellationToken); // assume 30 min slot
        return !any;
    }
}
