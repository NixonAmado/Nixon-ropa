using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class PrendaConfiguration:IEntityTypeConfiguration<Prenda>
{
    public void Configure(EntityTypeBuilder<Prenda> builder)
    {
        builder.ToTable("Prenda");
        builder.Property(p => p.Nombre)
        .IsRequired()
        .HasMaxLength(40);

        builder.Property(p => p.IdPrenda)
        .IsRequired();
        builder.HasIndex(p => p.IdPrenda)
        .IsUnique();

        builder.Property(p => p.ValorUnitCop)
        .IsRequired()
        .HasColumnType("decimal(15,2)")
        .HasMaxLength(40);

        builder.Property(p => p.ValorUnitUsd)
        .IsRequired()
        .HasColumnType("decimal(15,2)")
        .HasMaxLength(40);

        builder.HasOne(p => p.Estado)
        .WithMany(p => p.Prendas)
        .HasForeignKey(p => p.IdEstadoFk);

        builder.HasOne(p => p.TipoProteccion)
        .WithMany(p => p.Prendas)
        .HasForeignKey(p => p.IdTipoProteccionFk);

        builder.HasOne(p => p.Genero)
        .WithMany(p => p.Prendas)
        .HasForeignKey(p => p.IdGeneroFk);
     
        builder
        .HasMany(p => p.Insumos)
        .WithMany(p => p.Prendas)
        .UsingEntity<InsumoPrenda>(

            j => j
            .HasOne(p => p.Insumo)
            .WithMany(p => p.InsumosPrendas)
            .HasForeignKey(p => p.IdInsumoFk),
            j => j
            .HasOne(p => p.Prenda)
            .WithMany(p => p.InsumosPrendas)
            .HasForeignKey(p => p.IdPrendaFk),
            j =>
            {
                j.HasKey(t => new {t.IdInsumoFk,t.IdPrendaFk});
            });    
    }
}
