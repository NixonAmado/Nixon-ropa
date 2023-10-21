using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class DepartamentoConfiguration:IEntityTypeConfiguration<Departamento>
{
    public void Configure(EntityTypeBuilder<Departamento> builder)
    {
        builder.ToTable("Departamento");
        builder.Property(p => p.Nombre)
        .IsRequired()
        .HasMaxLength(40);

        builder.HasOne(p => p.Pais)
        .WithMany(p => p.Departamentos)
        .HasForeignKey(p => p.IdPaisFk);

    }
}