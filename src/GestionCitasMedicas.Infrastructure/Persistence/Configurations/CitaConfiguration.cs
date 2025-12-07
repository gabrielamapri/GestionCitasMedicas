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
        builder.Property(c => c.Motivo).IsRequired();
        builder.Property(c => c.Estado).IsRequired();
        builder.Property(c => c.FechaHora).HasColumnType("datetime(6)");
        builder.Property(c => c.FechaRegistro).HasColumnType("datetime(6)");
    }
}
