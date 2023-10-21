using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class ClienteConfiguration:IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Cliente");
        builder.Property(p => p.Nombre)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(p => p.Identificacion)
            .IsRequired();
        builder.HasIndex(p => p.Identificacion)
            .IsUnique();

        builder.Property(p => p.FechaRegistro)
            .IsRequired()
            .HasColumnType("DateTime");

        builder.HasOne(p => p.TipoPersona)
        .WithMany(p => p.Clientes)
        .HasForeignKey(p => p.IdTipoPersonaFk);

        builder.HasOne(p => p.Municipio)
        .WithMany(p => p.Clientes)
        .HasForeignKey(p => p.IdMunicipioFk);

    }
}