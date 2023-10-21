using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class ProveedorConfiguration:IEntityTypeConfiguration<Proveedor>
{
    public void Configure(EntityTypeBuilder<Proveedor> builder)
    {
        builder.ToTable("Proveedor");
        builder.Property(p => p.Nombre)
        .IsRequired()
        .HasMaxLength(40);

        builder.Property(p => p.NitProveedor)
        .IsRequired();
        
        builder.HasIndex(p => p.NitProveedor)
        .IsUnique();


        builder.HasOne(p => p.TipoPersona)
        .WithMany(p => p.Proveedores)
        .HasForeignKey(p => p.IdTipoPersonaFk);

        builder.HasOne(p => p.Municipio)
        .WithMany(p => p.Proveedores)
        .HasForeignKey(p => p.IdMunicipioFk);

    }
}