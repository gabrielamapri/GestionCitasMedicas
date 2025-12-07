using AutoMapper;
using GestionCitasMedicas.Domain.Entities;
using GestionCitasMedicas.API.Application.DTOs.Paciente;
using GestionCitasMedicas.API.Application.DTOs.Medico;
using GestionCitasMedicas.API.Application.DTOs.Especialidad;
using GestionCitasMedicas.API.Application.DTOs.Cita;

namespace GestionCitasMedicas.API.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Paciente
        CreateMap<Paciente, PacienteDto>();
        CreateMap<CreatePacienteDto, Paciente>();
        CreateMap<UpdatePacienteDto, Paciente>();

        // Medico
        CreateMap<Medico, MedicoDto>();
        CreateMap<CreateMedicoDto, Medico>();
        CreateMap<UpdateMedicoDto, Medico>();

        // Especialidad
        CreateMap<Especialidad, EspecialidadDto>();
        CreateMap<CreateEspecialidadDto, Especialidad>();

        // Cita
        CreateMap<Cita, CitaDto>();
        CreateMap<CreateCitaDto, Cita>();
    }
}
