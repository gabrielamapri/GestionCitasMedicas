using AutoMapper;
using GestionCitasMedicas.API.Application.DTOs.Paciente;
using GestionCitasMedicas.API.Application.Interfaces;
using GestionCitasMedicas.Domain.Entities;
using GestionCitasMedicas.Infrastructure.Repositories;
using GestionCitasMedicas.Infrastructure.UnitOfWork;

namespace GestionCitasMedicas.API.Application.Services;

public class PacienteService : IPacienteService
{
    private readonly IRepository<Paciente> _pacienteRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PacienteService(IRepository<Paciente> pacienteRepo, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _pacienteRepo = pacienteRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PacienteDto> CreateAsync(CreatePacienteDto dto)
    {
        var entity = _mapper.Map<Paciente>(dto);
        await _pacienteRepo.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<PacienteDto>(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _pacienteRepo.GetByIdAsync(id);
        if (entity == null) return false;
        _pacienteRepo.Remove(entity);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<PacienteDto>> GetAllAsync()
    {
        var list = await _pacienteRepo.GetAllAsync();
        return _mapper.Map<IEnumerable<PacienteDto>>(list);
    }

    public async Task<PacienteDto?> GetByIdAsync(int id)
    {
        var entity = await _pacienteRepo.GetByIdAsync(id);
        return entity == null ? null : _mapper.Map<PacienteDto>(entity);
    }

    public async Task<PacienteDto?> GetByEmailAsync(string email)
    {
        var found = await _pacienteRepo.FindAsync(p => p.Email == email);
        var entity = found.FirstOrDefault();
        return entity == null ? null : _mapper.Map<PacienteDto>(entity);
    }

    public async Task<IEnumerable<PacienteDto>> SearchByNameAsync(string name)
    {
        var found = await _pacienteRepo.FindAsync(p => p.Nombres.Contains(name) || p.Apellidos.Contains(name));
        return _mapper.Map<IEnumerable<PacienteDto>>(found);
    }

    public async Task<PacienteDto?> UpdateAsync(int id, UpdatePacienteDto dto)
    {
        var entity = await _pacienteRepo.GetByIdAsync(id);
        if (entity == null) return null;
        _mapper.Map(dto, entity);
        _pacienteRepo.Update(entity);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<PacienteDto>(entity);
    }
}
