using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class FormaPagoConfiguration:IEntityTypeConfiguration<FormaPago>
{
    public void Configure(EntityTypeBuilder<FormaPago> builder)
    {
        builder.ToTable("Forma_Pago");
        builder.Property(p => p.Descripcion)
        .IsRequired()
        .HasMaxLength(40);
    }
}