namespace Domain.Entities
{
    public class Genero : BaseEntity
    {
        public string Descripcion {get;set;}
        public ICollection<Prenda> Prendas {get;set;}
        
    }
}