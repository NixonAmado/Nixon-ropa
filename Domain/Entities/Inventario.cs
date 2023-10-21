
namespace Domain.Entities
{
    public class Inventario : BaseEntity
    {
        public int  CodInventario {get; set;}
        public int  ValorVtaCop {get; set;}
        public int  ValorVtaUsd {get; set;}
        public int IdPrendaFk {get; set;}
        public Prenda Prenda {get; set;}
        public ICollection<DetalleVenta> DetalleVentas {get; set;}
        public ICollection<Talla> Tallas {get;set;} = new HashSet<Talla>();
        public ICollection<InventarioTalla> InventariosTallas {get;set;}



    }
}