using AutoMapper;
using GestionCitasMedicas.API.Application.DTOs.Medico;
using GestionCitasMedicas.API.Application.Interfaces;
using GestionCitasMedicas.Domain.Entities;
using GestionCitasMedicas.Infrastructure.Repositories;
using GestionCitasMedicas.Infrastructure.UnitOfWork;

namespace GestionCitasMedicas.API.Application.Services;

public class MedicoService : IMedicoService
{
    private readonly IMedicoRepository _medicoRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MedicoService(IMedicoRepository medicoRepo, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _medicoRepo = medicoRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MedicoDto> CreateAsync(CreateMedicoDto dto)
    {
        var entity = _mapper.Map<Medico>(dto);
        await _medicoRepo.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<MedicoDto>(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _medicoRepo.GetByIdAsync(id);
        if (entity == null) return false;
        _medicoRepo.Remove(entity);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<MedicoDto>> GetAllAsync()
    {
        var list = await _medicoRepo.GetAllAsync();
        return _mapper.Map<IEnumerable<MedicoDto>>(list);
    }

    public async Task<MedicoDto?> GetByEmailAsync(string email)
    {
        var found = await _medicoRepo.FindAsync(m => m.Email == email);
        var entity = found.FirstOrDefault();
        return entity == null ? null : _mapper.Map<MedicoDto>(entity);
    }

    public async Task<MedicoDto?> GetByIdAsync(int id)
    {
        var entity = await _medicoRepo.GetByIdAsync(id);
        return entity == null ? null : _mapper.Map<MedicoDto>(entity);
    }

    public async Task<IEnumerable<MedicoDto>> SearchByEspecialidadAsync(int especialidadId)
    {
        var found = await _medicoRepo.GetByEspecialidadAsync(especialidadId);
        return _mapper.Map<IEnumerable<MedicoDto>>(found);
    }

    public async Task<MedicoDto?> UpdateAsync(int id, UpdateMedicoDto dto)
    {
        var entity = await _medicoRepo.GetByIdAsync(id);
        if (entity == null) return null;
        _mapper.Map(dto, entity);
        _medicoRepo.Update(entity);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<MedicoDto>(entity);
    }
}
