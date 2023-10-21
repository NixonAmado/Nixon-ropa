using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class TipoPersonaConfiguration:IEntityTypeConfiguration<TipoPersona>
{
    public void Configure(EntityTypeBuilder<TipoPersona> builder)
    {
        builder.ToTable("Tipo_persona");
        builder.Property(p => p.Nombre)
        .IsRequired()
        .HasMaxLength(40);
    }
}