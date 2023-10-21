using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class EmpresaConfiguration:IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.ToTable("Empresa");
        builder.Property(p => p.Nit)
        .IsRequired();

        builder.HasIndex(p => p.Nit)
        .IsUnique();

        builder.Property(p => p.RazonSocial)
        .IsRequired()
        .HasMaxLength(70);

        builder.Property(p => p.RepresentanteLegal)
        .IsRequired()
        .HasMaxLength(40);

        builder.Property(p => p.FechaCreacion)
        .IsRequired()
        .HasColumnType("DateTime");

        builder.HasOne(p => p.Municipio)
        .WithMany(p => p.Empresas)
        .HasForeignKey(p => p.IdMunicipioFk);

    }
}