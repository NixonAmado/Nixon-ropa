using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class VentaConfiguration:IEntityTypeConfiguration<Venta>
{
    public void Configure(EntityTypeBuilder<Venta> builder)
    {
        builder.ToTable("Venta");
        builder.Property(p => p.Fecha)
        .IsRequired()
        .HasColumnType("DateTime");

        builder.HasOne(p => p.Empleado)
        .WithMany(p => p.Ventas)
        .HasForeignKey(p => p.IdEmpleadoFk);

        builder.HasOne(p => p.Cliente)
        .WithMany(p => p.Ventas)
        .HasForeignKey(p => p.IdClienteFk);

        builder.HasOne(p => p.FormaPago)
        .WithMany(p => p.Ventas)
        .HasForeignKey(p => p.IdFormaPagoFk);


    }
}