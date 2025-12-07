using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using GestionCitasMedicas.Infrastructure.Persistence.Context;

namespace GestionCitasMedicas.Infrastructure.Persistence.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Load env if present
            try
            {
                DotNetEnv.Env.Load();
            }
            catch
            {
                // ignore
            }

            var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "127.0.0.1";
            var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
            var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "citas_medicas_db";
            var user = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? string.Empty;

            var connectionString = $"Server={host};Port={port};Database={database};User={user};Password={password};";

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 0)));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
