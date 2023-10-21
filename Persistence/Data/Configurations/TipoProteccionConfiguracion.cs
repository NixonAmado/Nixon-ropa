using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class TipoProteccionConfiguration:IEntityTypeConfiguration<TipoProteccion>
{
    public void Configure(EntityTypeBuilder<TipoProteccion> builder)
    {
        builder.ToTable("Tipo_Proteccion");
        builder.Property(p => p.Descripcion)
        .IsRequired()
        .HasMaxLength(50);

    }
}