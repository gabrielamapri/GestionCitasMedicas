using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestionCitasMedicas.Domain.Entities;

namespace GestionCitasMedicas.Infrastructure.Persistence.Configurations;

public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
{
    public void Configure(EntityTypeBuilder<Medico> builder)
    {
        builder.ToTable("Medicos");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Nombres).IsRequired();
        builder.Property(m => m.Apellidos).IsRequired();
        builder.Property(m => m.Email).IsRequired();
        builder.Property(m => m.Telefono).IsRequired();
    }
}
