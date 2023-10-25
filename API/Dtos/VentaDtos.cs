namespace API.Dtos;
public class VentaPDto
{
    public int Id {get;set;}
    public DateTime Fecha {get;set;}
    public int IdEmpleadoFk {get;set;}
    public int IdClienteFk {get;set;}
    public int IdFormaPagoFk {get;set;}
}

public class VentaEmpleadoDto
{
    public EmpleadoVentaDto Empleado {get;set;}
    public int NroFactura {get;set;}
    public DateTime Fecha {get;set;}
    public double TotalFactura {get;set;}

}
