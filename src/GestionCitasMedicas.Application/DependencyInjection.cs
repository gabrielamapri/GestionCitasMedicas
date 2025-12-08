using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GestionCitasMedicas.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Mappings.MappingProfile));

        // Servicios de aplicación
        services.AddScoped<Interfaces.IPacienteService, Services.PacienteService>();
        services.AddScoped<Interfaces.IMedicoService, Services.MedicoService>();
        services.AddScoped<Interfaces.ICitaService, Services.CitaService>();

        return services;
    }
}
