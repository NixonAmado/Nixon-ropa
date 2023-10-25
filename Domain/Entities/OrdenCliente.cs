namespace Domain.Entities
{
    public class OrdenCliente
    {
        public DateTime Fecha {get;set;}
        public Cliente Cliente {get;set;}
        public Estado Estado {get;set;}
        public decimal ValorTotalOrden {get;set;}
        public ICollection<DetalleOrden> DetalleOrdenes {get;set;} 
    
    }
}