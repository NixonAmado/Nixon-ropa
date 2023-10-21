using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class EstadoPDto
    {
        public int Id {get;set;}
        public string Descripcion {get;set;}
        public int IdTipoEstadoFk  {get;set;} 
    }

    public class EstadoADto
    {
        public int Id {get;set;}
        public string Descripcion {get;set;}
    }
}