
namespace API.Dtos;
public class EmpresaPDto
{
    public int Id {get;set;}
    public int Nit {get;set;}
    public string RazonSocial {get;set;}
    public string RepresentanteLegal {get;set;}
    public DateTime FechaCreacion {get;set;}
    public int IdMunicipioFk {get;set;}
}
