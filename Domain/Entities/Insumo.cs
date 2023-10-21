namespace Domain.Entities
{
    public class Insumo : BaseEntity
    {
        public string Nombre { get; set; }   
        public double ValorUnitCop { get; set; }   
        public int StockMin { get; set; }   
        public int StockMax { get; set; }   
        public ICollection<Prenda> Prendas {get;set;} = new HashSet<Prenda>();
        public ICollection<InsumoPrenda> InsumosPrendas {get;set;}
        public ICollection<Proveedor> Proveedores {get;set;} = new HashSet<Proveedor>();
        public ICollection<InsumoProveedor> InsumosProveedores {get;set;}

    }
}