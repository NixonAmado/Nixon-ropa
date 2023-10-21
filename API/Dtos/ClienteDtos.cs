namespace API.Dtos;
public class ClientePDto
{
    public int Id {get; set;}
    public string Nombre { get; set; }
    public int Identificacion {get; set;}
    public int IdTipoPersonaFk { get; set; }
    public DateTime FechaRegistro {get;set;}
    public int IdMunicipioFk {get;set;}
    
}

public class ClienteOrdenDto 
{
    public int Id {get; set;}
    public string Nombre { get; set; }
    public MunicipioNombreDto Municipio {get;set;}
}
