using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrdenCliente
    {
        public DateTime Fecha {get;set;}
        public Cliente Cliente {get;set;}
        public Estado Estado {get;set;}
        public decimal ValorTotalOrden {get;set;}
        public ICollection<DetalleOrden> DetalleOrden {get;set;} 
    
    }
}