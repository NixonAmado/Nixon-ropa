using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class InventarioTallaConfiguration:IEntityTypeConfiguration<InventarioTalla>
{
    public void Configure(EntityTypeBuilder<InventarioTalla> builder)
    {
        builder.ToTable("Inventario_Talla");
        builder.Property(p => p.Cantidad)
        .IsRequired();   
    }
}