using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestionCitasMedicas.Domain.Entities;

namespace GestionCitasMedicas.Infrastructure.Persistence.Configurations;

public class CitaConfiguration : IEntityTypeConfiguration<Cita>
{
    public void Configure(EntityTypeBuilder<Cita> builder)
    {
        builder.ToTable("Citas");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FechaHora).HasColumnType("datetime(6)");
        builder.Property(c => c.Motivo).IsRequired();
        builder.Property(c => c.Estado).IsRequired();
        builder.Property(c => c.Observaciones).IsRequired(false);
        builder.Property(c => c.FechaRegistro).HasColumnType("datetime(6)");

        builder.HasOne(c => c.Paciente)
               .WithMany(p => p.Citas)
               .HasForeignKey(c => c.PacienteId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Medico)
               .WithMany(m => m.Citas)
               .HasForeignKey(c => c.MedicoId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
