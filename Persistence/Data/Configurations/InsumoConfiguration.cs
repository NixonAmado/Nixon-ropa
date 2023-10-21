using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class InsumoConfiguration:IEntityTypeConfiguration<Insumo>
{
    public void Configure(EntityTypeBuilder<Insumo> builder)
    {
        builder.ToTable("Insumo");
        builder.Property(p => p.Nombre)
            .IsRequired()
            .HasMaxLength(40);
        builder.HasIndex(e => e.Nombre)
        .IsUnique();
        
        builder.Property(p => p.ValorUnitCop)
            .IsRequired()
            .HasColumnType("decimal(20,2)");

        builder.Property(p => p.StockMin)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(p => p.StockMax)
            .IsRequired()
            .HasMaxLength(10);

        builder
        .HasMany(p => p.Proveedores)
        .WithMany(p => p.Insumos)
        .UsingEntity<InsumoProveedor>(

            j => j
            .HasOne(p => p.Proveedor)
            .WithMany(p => p.InsumosProveedores)
            .HasForeignKey(p => p.IdProveedorFk),
            j => j
            .HasOne(p => p.Insumo)
            .WithMany(p => p.InsumosProveedores)
            .HasForeignKey(p => p.IdInsumoFk),
            j =>
            {
                j.HasKey(t => new {t.IdProveedorFk,t.IdInsumoFk});
            });    
    }
}