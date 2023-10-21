using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class InsumoPrendaConfiguration:IEntityTypeConfiguration<InsumoPrenda>
{
    public void Configure(EntityTypeBuilder<InsumoPrenda> builder)
    {
        builder.ToTable("Insumo_Prenda");

        builder.Property(p => p.Cantidad)
        .IsRequired();
}
}