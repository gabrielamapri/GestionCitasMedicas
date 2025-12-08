using AutoMapper;
using GestionCitasMedicas.Domain.Entities;
using GestionCitasMedicas.Application.DTOs.Paciente;
using GestionCitasMedicas.Application.DTOs.Medico;
using GestionCitasMedicas.Application.DTOs.Especialidad;
using GestionCitasMedicas.Application.DTOs.Cita;

namespace GestionCitasMedicas.Application.Mappings;

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
