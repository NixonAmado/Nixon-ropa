using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class EmpleadoConfiguration:IEntityTypeConfiguration<Empleado>
{
    public void Configure(EntityTypeBuilder<Empleado> builder)
    {
        builder.ToTable("Empleado");
        builder.Property(p => p.Nombre)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(p => p.Identificacion)
            .IsRequired();
            
        builder.HasIndex(p => p.Identificacion)
        .IsUnique();

        builder.Property(p => p.FechaIngreso)
            .IsRequired()
            .HasColumnType("DateTime");

        builder.HasOne(p => p.Cargo)
        .WithMany(p => p.Empleados)
        .HasForeignKey(p => p.IdCargoFk);

        builder.HasOne(p => p.Municipio)
        .WithMany(p => p.Empleados)
        .HasForeignKey(p => p.IdMunicipioFk);

    }
}