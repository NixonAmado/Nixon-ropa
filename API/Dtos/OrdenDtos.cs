namespace API.Dtos;

    public class OrdenPDto
    {
    public int Id {get;set;} 
    public DateTime Fecha {get;set;}
    public int IdEmpleadoFk {get;set;}
    public int IdClienteFk {get;set;}
    public int IdEstadoFk {get;set;}
    }

    public class OrdenClienteDto
    {
    public DateTime Fecha {get;set;}
    public ClienteOrdenDto Cliente {get;set;}
    public EstadoADto Estado {get;set;}
    public decimal ValorTotalOrden {get;set;}
    public List<DetalleOrdenEndCuatroDto> DetalleOrden {get;set;} 
    
    }
