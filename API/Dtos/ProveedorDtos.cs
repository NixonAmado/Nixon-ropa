namespace API.Dtos;
    public class ProveedorPDto
    {
        public int Id {get;set;}
        public int NitProveedor {get; set;}
        public string Nombre {get; set;}
        public int IdTipoPersonaFk {get;set;}
        public int IdMunicipioFk {get;set;}

    }
