
namespace API.Dtos;
public class DetalleOrdenPDto
{
    public int Id {get;set;}
        public int IdOrden {get; set;}
        public string CantidadProducir {get;set;}
        public string CantidadProducida {get;set;}
        public int IdEstadoFk {get;set;}
        public int IdPrendaFk {get;set;}
        public int IdColorFk {get;set;}
}
public class DetalleOrdenEndCuatroDto
{
    public int Id {get;set;}
    public int IdOrden {get; set;}
    public PrendaOrdenDto Prenda {get; set;}
    public string CantidadPrendas {get;set;}
    public double ValorTotalPesos {get;set;}
    public double ValorTotalDolares {get;set;}
}
