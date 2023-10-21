using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class CargoConfiguration:IEntityTypeConfiguration<Cargo>
{
    public void Configure(EntityTypeBuilder<Cargo> builder)
    {
        builder.ToTable("Cargo");
        builder.Property(p => p.Descripcion)
        .IsRequired()
        .HasMaxLength(40);

        builder.Property(p => p.SueldoBase)
        .IsRequired()
        .HasColumnType("decimal(15,2)");
    }
}