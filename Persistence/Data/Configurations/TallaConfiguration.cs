using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class TallaConfiguration:IEntityTypeConfiguration<Talla>
{
    public void Configure(EntityTypeBuilder<Talla> builder)
    {
        builder.ToTable("Talla");
        builder.Property(p => p.Descripcion)
        .IsRequired()
        .HasMaxLength(40);
        builder.HasIndex(p => p.Descripcion)
        .IsUnique();
    
    builder
        .HasMany(p => p.Inventarios)
        .WithMany(p => p.Tallas)
        .UsingEntity<InventarioTalla>(
            j => j
            .HasOne(p => p.Inventario)
            .WithMany(p => p.InventariosTallas)
            .HasForeignKey(p => p.IdInventarioFk),
            j => j
            .HasOne(p => p.Talla)
            .WithMany(p => p.InventariosTallas)
            .HasForeignKey(p => p.IdTallaFk),
            j =>
            {
                j.HasKey(t => new {t.IdInventarioFk,t.IdTallaFk});
            });    
    }
}