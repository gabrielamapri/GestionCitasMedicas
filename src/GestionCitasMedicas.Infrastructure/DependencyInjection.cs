using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GestionCitasMedicas.Infrastructure.Persistence.Context;
using GestionCitasMedicas.Ports.Out;
using GestionCitasMedicas.Infrastructure.Repositories;
using GestionCitasMedicas.Infrastructure.UnitOfWork;

namespace GestionCitasMedicas.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure DbContext (expects a connection string named "DefaultConnection")
        var conn = configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrEmpty(conn))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(conn, ServerVersion.AutoDetect(conn)));
        }
        else
        {
            // If no connection string provided (e.g., during some dev scenarios), register a DbContext with in-memory provider
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("GestionCitasMedicas"));
        }

        // Repositories & unit of work
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICitaRepository, CitaRepository>();
        services.AddScoped<IMedicoRepository, MedicoRepository>();
        services.AddScoped<GestionCitasMedicas.Ports.Out.IUnitOfWork, GestionCitasMedicas.Infrastructure.UnitOfWork.UnitOfWork>();

        return services;
    }
}
