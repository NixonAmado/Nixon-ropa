using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class DetalleOrdenConfiguration:IEntityTypeConfiguration<DetalleOrden>
{
    public void Configure(EntityTypeBuilder<DetalleOrden> builder)
    {
        builder.ToTable("Detalle_orden");
        builder.Property(p => p.IdOrden)
        .IsRequired();


        builder.Property(p => p.CantidadProducir)
        .IsRequired();  

        builder.Property(p => p.CantidadProducida)
        .IsRequired();

        builder.HasOne(p => p.Prenda)
        .WithMany(p => p.DetalleOrdenes)
        .HasForeignKey(p => p.IdPrendaFk);

        builder.HasOne(p => p.Color)
        .WithMany(p => p.DetalleOrdenes)
        .HasForeignKey(p => p.IdColorFk);

        builder.HasOne(p => p.Estado)
        .WithMany(p => p.DetalleOrdenes)
        .HasForeignKey(p => p.IdEstadoFk);
                                

    }
}