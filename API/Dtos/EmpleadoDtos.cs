namespace API.Dtos;
public class EmpleadoPDto
{
    public int Id { get; set;}
    public string Nombre {get; set;}
    public int Identificacion {get; set;}
    public DateTime FechaIngreso {get; set;}
    public int IdCargoFk {get;set;}
    public int IdMunicipioFk {get;set;}
}

public class EmpleadoVentaDto 
{
    public int Id {get;set;}
    public string Nombre {get;set;}
}
