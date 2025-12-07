using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GestionCitasMedicas.Infrastructure.Persistence.Context;

namespace GestionCitasMedicas.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var host = Environment.GetEnvironmentVariable("DB_HOST") ?? Environment.GetEnvironmentVariable("BD_HOST") ?? configuration["DB_HOST"];
        var port = Environment.GetEnvironmentVariable("DB_PORT") ?? configuration["DB_PORT"] ?? "3306";
        var database = Environment.GetEnvironmentVariable("DB_NAME") ?? configuration["DB_NAME"] ?? "citas_medicas_db";
        var user = Environment.GetEnvironmentVariable("DB_USER") ?? configuration["DB_USER"] ?? "root";
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? configuration["DB_PASSWORD"] ?? "";

        var connectionString = $"Server={host};Port={port};Database={database};User={user};Password={password};";
        Console.WriteLine($"Connection String: {connectionString}");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 0)), b =>
                b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
        );

        // Repositorios genéricos y UnitOfWork
        services.AddScoped(typeof(GestionCitasMedicas.Infrastructure.Repositories.IRepository<>), typeof(GestionCitasMedicas.Infrastructure.Repositories.Repository<>));
        services.AddScoped<GestionCitasMedicas.Infrastructure.UnitOfWork.IUnitOfWork, GestionCitasMedicas.Infrastructure.UnitOfWork.UnitOfWork>();
        // Repositorios especializados
        services.AddScoped<GestionCitasMedicas.Infrastructure.Repositories.IMedicoRepository, GestionCitasMedicas.Infrastructure.Repositories.MedicoRepository>();
        services.AddScoped<GestionCitasMedicas.Infrastructure.Repositories.ICitaRepository, GestionCitasMedicas.Infrastructure.Repositories.CitaRepository>();

        return services;
    }
}
