using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GestionCitasMedicas.API.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Application.Mappings.MappingProfile));

        // Servicios de aplicación
        services.AddScoped<Application.Interfaces.IPacienteService, Application.Services.PacienteService>();
        services.AddScoped<Application.Interfaces.IMedicoService, Application.Services.MedicoService>();
        services.AddScoped<Application.Interfaces.ICitaService, Application.Services.CitaService>();

        return services;
    }
}
