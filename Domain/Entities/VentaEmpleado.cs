
namespace Domain.Entities;
public class VentaEmpleado
{
    public Empleado Empleado {get;set;}
    public int Id {get;set;}
    public DateTime Fecha {get;set;}
    public double TotalFactura {get;set;}

}
