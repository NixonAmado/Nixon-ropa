using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class MunicipioConfiguration:IEntityTypeConfiguration<Municipio>
{
    public void Configure(EntityTypeBuilder<Municipio> builder)
    {
        builder.ToTable("Municipio");
        builder.Property(P => P.Nombre)
        .IsRequired()
        .HasMaxLength(40);

        builder.HasOne(p => p.Departamento)
        .WithMany(p => p.Municipios)
        .HasForeignKey(p => p.IdDepartamentoFk);

    }
}