using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestionCitasMedicas.Domain.Entities;

namespace GestionCitasMedicas.Infrastructure.Persistence.Configurations;

public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Pacientes");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nombres).IsRequired();
        builder.Property(p => p.Apellidos).IsRequired();
        builder.Property(p => p.DocumentoIdentidad).IsRequired();
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.Telefono).IsRequired();
        builder.Property(p => p.FechaNacimiento).HasColumnType("datetime(6)");
        builder.Property(p => p.FechaRegistro).HasColumnType("datetime(6)");

        builder.HasMany(p => p.Citas)
               .WithOne(c => c.Paciente)
               .HasForeignKey(c => c.PacienteId);
    }
}
