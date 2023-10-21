namespace Domain.Entities
{
    public class Cliente : BaseEntity
    {
        public string Nombre { get; set; }
        public int Identificacion {get; set;}
        public int IdTipoPersonaFk { get; set; }
        public TipoPersona TipoPersona { get; set; }
        public DateTime FechaRegistro {get;set;}
        public int IdMunicipioFk {get;set;}
        public Municipio Municipio {get;set;}
        public ICollection<Venta> Ventas {get;set;}
        public ICollection<Orden> Ordenes {get;set;}

    }
}