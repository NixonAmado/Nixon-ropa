namespace API.Dtos;
    public class PrendaPDto
    {
        public int Id {get;set;}
        public int IdPrenda {get;set;}
        public string Nombre { get; set; }
        public double ValorUnitCop {get;set;}
        public double ValorUnitUsd {get;set;}
        public int  IdTipoProteccionFk {get;set;}
        public int  IdGeneroFk {get;set;}
        public int  IdEstadoFk {get;set;}
    }

     public class PrendaOrdenDto
    {
        public int IdPrenda {get;set;}
        public string Nombre { get; set; }
        public double ValorUnitCop {get;set;}
        public double ValorUnitUsd {get;set;}
    }

         public class PrendaNombreDto
    {
        public string Nombre { get; set; }
    }