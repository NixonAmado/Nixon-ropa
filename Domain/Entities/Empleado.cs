namespace Domain.Entities
{
    public class Empleado : BaseEntity
    {
        public string Nombre {get; set;}
        public int Identificacion {get; set;}
        public DateTime FechaIngreso {get; set;}
        public int IdCargoFk {get;set;}
        public Cargo Cargo {get;set;}
        public int IdMunicipioFk {get;set;}
        public Municipio Municipio {get;set;}
        public ICollection<Orden> Ordenes {get;set;}
        public ICollection<Venta> Ventas {get;set;}

    }
}