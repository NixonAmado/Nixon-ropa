namespace Domain.Entities
{
    public class Pais : BaseEntity
    {
        public string Nombre {get;set;}
        public ICollection<Departamento> Departamentos {get;set;}   
    }
}