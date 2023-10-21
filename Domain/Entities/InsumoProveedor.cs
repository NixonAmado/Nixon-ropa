namespace Domain.Entities
{
    public class InsumoProveedor 
    {
        public int IdInsumoFk  {get;set;} 
        public Insumo Insumo  {get;set;} 
        public int IdProveedorFk  {get;set;} 
        public Proveedor Proveedor  {get;set;} 

    }
}