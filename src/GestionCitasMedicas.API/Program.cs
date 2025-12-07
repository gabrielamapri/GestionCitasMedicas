using DotNetEnv;
using GestionCitasMedicas.Infrastructure;
using GestionCitasMedicas.API.Application;

var builder = WebApplication.CreateBuilder(args);

// Load .env if present (optional)
var currentDir = Directory.GetCurrentDirectory();
var envPath = Path.Combine(currentDir, ".env");
if (!File.Exists(envPath))
{
    envPath = Path.Combine(currentDir, "..", "..", ".env");
}
if (File.Exists(envPath))
{
    Env.Load(envPath);
}

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
