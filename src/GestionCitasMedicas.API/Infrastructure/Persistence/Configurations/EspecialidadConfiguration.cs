using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestionCitasMedicas.Domain.Entities;

namespace GestionCitasMedicas.Infrastructure.Persistence.Configurations;

public class EspecialidadConfiguration : IEntityTypeConfiguration<Especialidad>
{
    public void Configure(EntityTypeBuilder<Especialidad> builder)
    {
        builder.ToTable("Especialidades");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nombre).IsRequired();
        builder.Property(e => e.Descripcion).IsRequired();

        builder.HasMany(e => e.Medicos)
               .WithOne(m => m.Especialidad)
               .HasForeignKey(m => m.EspecialidadId);
    }
}
