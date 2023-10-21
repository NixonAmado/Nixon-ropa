using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class OrdenConfiguration:IEntityTypeConfiguration<Orden>
{
    public void Configure(EntityTypeBuilder<Orden> builder)
    {
        builder.ToTable("Orden");
        builder.Property(p => p.Fecha)
        .IsRequired()
        .HasColumnType("DateTime")
        .HasMaxLength(40);

        builder.HasOne(p => p.Empleado)
        .WithMany(p => p.Ordenes)
        .HasForeignKey(p => p.IdEmpleadoFk);

        builder.HasOne(p => p.Cliente)
        .WithMany(p => p.Ordenes)
        .HasForeignKey(p => p.IdClienteFk);

        builder.HasOne(p => p.Estado)
        .WithMany(p => p.Ordenes)
        .HasForeignKey(p => p.IdEstadoFk);

    }
}