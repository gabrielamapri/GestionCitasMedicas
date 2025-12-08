using AutoMapper;
using GestionCitasMedicas.Application.DTOs.Cita;
using GestionCitasMedicas.Application.Interfaces;
using GestionCitasMedicas.Domain.Entities;
using GestionCitasMedicas.Ports.Out;

namespace GestionCitasMedicas.Application.Services;

public class CitaService : ICitaService
{
    private readonly ICitaRepository _citaRepo;
    private readonly IRepository<Paciente> _pacienteRepo;
    private readonly IMedicoRepository _medicoRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    // business configuration (could be moved to settings)
    private const int SlotDurationMinutes = 30;
    private const int CancellationWindowHours = 2;

    public CitaService(ICitaRepository citaRepo, IRepository<Paciente> pacienteRepo, IMedicoRepository medicoRepo, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _citaRepo = citaRepo;
        _pacienteRepo = pacienteRepo;
        _medicoRepo = medicoRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CitaDto> CreateAsync(CreateCitaDto dto)
    {
        var paciente = await _pacienteRepo.GetByIdAsync(dto.PacienteId);
        var medico = await _medicoRepo.GetByIdAsync(dto.MedicoId);
        if (paciente == null) throw new InvalidOperationException("Paciente no existe");
        if (medico == null) throw new InvalidOperationException("Medico no existe");

        var now = DateTime.UtcNow;
        if (dto.FechaHora <= now) throw new InvalidOperationException("No se pueden programar citas en el pasado");

        var start = dto.FechaHora;
        var end = start.AddMinutes(SlotDurationMinutes);

        var medicoAvailable = await _medicoRepo.IsAvailableAsync(dto.MedicoId, start, end);
        if (!medicoAvailable) throw new InvalidOperationException("Medico no disponible en ese horario");

        var pacienteCitas = await _citaRepo.GetByPacienteAsync(dto.PacienteId);
        var pacienteOverlap = pacienteCitas.Any(c => c.Estado == "Programada" && c.FechaHora < end && c.FechaHora.AddMinutes(SlotDurationMinutes) > start);
        if (pacienteOverlap) throw new InvalidOperationException("Paciente ya tiene otra cita en ese horario");

        var entity = _mapper.Map<Cita>(dto);
        entity.Estado = "Programada";
        entity.FechaRegistro = DateTime.UtcNow;

        await _citaRepo.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<CitaDto>(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _citaRepo.GetByIdAsync(id);
        if (entity == null) return false;
        _citaRepo.Remove(entity);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<CitaDto>> GetAllAsync()
    {
        var list = await _citaRepo.GetAllAsync();
        return _mapper.Map<IEnumerable<CitaDto>>(list);
    }

    public async Task<CitaDto?> GetByIdAsync(int id)
    {
        var entity = await _citaRepo.GetWithIncludesAsync(id);
        return entity == null ? null : _mapper.Map<CitaDto>(entity);
    }

    public async Task<IEnumerable<CitaDto>> GetByDateAsync(DateTime date)
    {
        var list = await _citaRepo.GetByDateRangeAsync(date.Date, date.Date.AddDays(1).AddTicks(-1));
        return _mapper.Map<IEnumerable<CitaDto>>(list);
    }

    public async Task<IEnumerable<CitaDto>> GetByMedicoAsync(int medicoId)
    {
        var list = await _citaRepo.GetByMedicoAsync(medicoId);
        return _mapper.Map<IEnumerable<CitaDto>>(list);
    }

    public async Task<IEnumerable<CitaDto>> GetByPacienteAsync(int pacienteId)
    {
        var list = await _citaRepo.GetByPacienteAsync(pacienteId);
        return _mapper.Map<IEnumerable<CitaDto>>(list);
    }

    public async Task<bool> CancelAsync(int id)
    {
        var entity = await _citaRepo.GetWithIncludesAsync(id);
        if (entity == null) return false;

        var now = DateTime.UtcNow;
        if (entity.Estado != "Programada") return false;
        var hoursUntil = (entity.FechaHora - now).TotalHours;
        if (hoursUntil < CancellationWindowHours) return false;

        entity.Estado = "Cancelada";
        _citaRepo.Update(entity);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<CitaDto?> UpdateAsync(int id, CreateCitaDto dto)
    {
        var entity = await _citaRepo.GetWithIncludesAsync(id);
        if (entity == null) return null;

        var now = DateTime.UtcNow;
        if (dto.FechaHora <= now) throw new InvalidOperationException("No se pueden programar citas en el pasado");

        var start = dto.FechaHora;
        var end = start.AddMinutes(SlotDurationMinutes);

        var medicoCitas = await _citaRepo.GetByMedicoAsync(dto.MedicoId);
        var medicoOverlap = medicoCitas.Any(c => c.Id != id && c.Estado == "Programada" && c.FechaHora < end && c.FechaHora.AddMinutes(SlotDurationMinutes) > start);
        if (medicoOverlap) throw new InvalidOperationException("Medico no disponible en ese horario");

        var pacienteCitas = await _citaRepo.GetByPacienteAsync(dto.PacienteId);
        var pacienteOverlap = pacienteCitas.Any(c => c.Id != id && c.Estado == "Programada" && c.FechaHora < end && c.FechaHora.AddMinutes(SlotDurationMinutes) > start);
        if (pacienteOverlap) throw new InvalidOperationException("Paciente ya tiene otra cita en ese horario");

        entity.FechaHora = dto.FechaHora;
        entity.Motivo = dto.Motivo;
        entity.PacienteId = dto.PacienteId;
        entity.MedicoId = dto.MedicoId;
        _citaRepo.Update(entity);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<CitaDto>(entity);
    }
}
