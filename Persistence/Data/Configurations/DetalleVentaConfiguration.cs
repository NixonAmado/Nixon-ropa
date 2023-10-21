using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class DetalleVentaConfiguration:IEntityTypeConfiguration<DetalleVenta>
{
    public void Configure(EntityTypeBuilder<DetalleVenta> builder)
    {
        builder.ToTable("Detalle_venta");
        builder.Property(p => p.Cantidad)
        .IsRequired();

        builder.Property(p => p.ValorUnit)
        .HasColumnType("decimal(15,2)")
        .IsRequired();
        
        builder.HasOne(p => p.Venta)
        .WithMany(p => p.DetalleVentas)
        .HasForeignKey(p => p.IdVentaFk);

        builder.HasOne(p => p.Inventario)
        .WithMany(p => p.DetalleVentas)
        .HasForeignKey(p => p.IdInventarioFk);

        builder.HasOne(p => p.Talla)
        .WithMany(p => p.DetalleVentas)
        .HasForeignKey(p => p.IdTallaFk);

    }
}