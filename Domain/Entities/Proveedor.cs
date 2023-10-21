namespace Domain.Entities
{
    public class Proveedor : BaseEntity
    {
        public int NitProveedor {get; set;}
        public string Nombre {get; set;}
        public int IdTipoPersonaFk {get;set;}
        public TipoPersona TipoPersona {get;set;}
        public int IdMunicipioFk {get;set;}
        public Municipio Municipio {get;set;}
        public ICollection<Insumo> Insumos {get;set;} = new HashSet<Insumo>();
        public ICollection<InsumoProveedor> InsumosProveedores {get;set;}


    }
}