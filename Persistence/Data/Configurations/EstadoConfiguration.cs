using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class EstadoConfiguration:IEntityTypeConfiguration<Estado>
{
    public void Configure(EntityTypeBuilder<Estado> builder)
    {
        builder.ToTable("Estado");
        builder.Property(p => p.Descripcion)
            .IsRequired()
            .HasMaxLength(40);

        builder.HasOne(p => p.TipoEstado)
        .WithMany(p => p.Estados)
        .HasForeignKey(p => p.IdTipoEstadoFk);

    }
}