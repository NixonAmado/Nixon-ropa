using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class TipoEstadoConfiguration:IEntityTypeConfiguration<TipoEstado>
{
    public void Configure(EntityTypeBuilder<TipoEstado> builder)
    {
        builder.ToTable("Tipo_estado");
        builder.Property(p => p.Descripcion)
            .IsRequired()
            .HasMaxLength(40);
    }
}