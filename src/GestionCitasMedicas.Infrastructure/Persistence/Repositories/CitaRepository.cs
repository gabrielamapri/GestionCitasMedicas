using Microsoft.EntityFrameworkCore;
using GestionCitasMedicas.Domain.Entities;
using GestionCitasMedicas.Infrastructure.Persistence.Context;
using GestionCitasMedicas.Ports.Out;

namespace GestionCitasMedicas.Infrastructure.Repositories;

public class CitaRepository : Repository<Cita>, ICitaRepository
{
    public CitaRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Cita>> GetByPacienteAsync(int pacienteId, CancellationToken cancellationToken = default)
    {
        return await _context.Citas.Where(c => c.PacienteId == pacienteId).Include(c => c.Medico).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Cita>> GetByMedicoAsync(int medicoId, CancellationToken cancellationToken = default)
    {
        return await _context.Citas.Where(c => c.MedicoId == medicoId).Include(c => c.Paciente).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Cita>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        return await _context.Citas.Where(c => c.FechaHora >= from && c.FechaHora <= to)
            .Include(c => c.Paciente)
            .Include(c => c.Medico)
            .ToListAsync(cancellationToken);
    }

    public async Task<Cita?> GetWithIncludesAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Citas.Include(c => c.Paciente).Include(c => c.Medico).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}
