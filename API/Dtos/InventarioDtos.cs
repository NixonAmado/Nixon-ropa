namespace API.Dtos;
public class InventarioPDto
{
    public int Id {get;set;}
    public int  CodInventario {get; set;}
    public int  ValorVtaCop {get; set;}
    public int  ValorVtaUsd {get; set;}
    public int IdPrendaFk {get; set;}      
}