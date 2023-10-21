using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class InventarioConfiguration:IEntityTypeConfiguration<Inventario>
{
    public void Configure(EntityTypeBuilder<Inventario> builder)
    {
        builder.ToTable("Inventario");
        builder.Property(p => p.CodInventario)
        .IsRequired();
        builder.HasIndex(p => p.CodInventario)
        .IsUnique();

        builder.Property(p => p.ValorVtaCop)
        .HasColumnType("decimal(15,2)")
        .IsRequired();

        builder.Property(p => p.ValorVtaUsd)
        .HasColumnType("decimal(15,2)")
        .IsRequired();

        builder.HasOne(p => p.Prenda)
        .WithMany(p => p.Inventarios)
        .HasForeignKey(p => p.IdPrendaFk);
    }
}