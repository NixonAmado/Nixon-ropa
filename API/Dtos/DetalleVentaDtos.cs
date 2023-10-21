namespace API.Dtos;
public class DetalleVentaPDto
{
    public int Id {get;set;}
    public string Cantidad {get;set;}
    public double ValorUnit {get;set;}
    public int IdInventarioFk {get;set;}
    public int IdTallaFk {get;set;}
    public int IdVentaFk {get;set;}
    
}
