namespace API.Dtos;
public class InventarioPDto
{
    public int Id {get;set;}
    public int  CodInventario {get; set;}
    public int  ValorVtaCop {get; set;}
    public int  ValorVtaUsd {get; set;}
    public int IdPrendaFk {get; set;}      
}

public class InventarioPrendaDto
{
    public int  CodInventario {get; set;}
    public PrendaNombreDto Prenda {get; set;}      
    public List<InventarioTallaDto> InventarioTallas {get;set;}

}