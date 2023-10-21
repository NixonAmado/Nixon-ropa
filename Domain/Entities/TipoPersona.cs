namespace Domain.Entities
{
    public class TipoPersona : BaseEntity
    {
        public string Nombre {get;set;}
        public ICollection<Proveedor> Proveedores {get;set;}
        public ICollection<Cliente> Clientes {get;set;}

    }
}