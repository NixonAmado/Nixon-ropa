using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class TipoEstadoPDto
    {
        public int Id {get;set;}
        public string Descripcion {get;set;}
    
    }

    public class TipoEstadoADto
    {
        public int Id {get;set;}
        public string Descripcion {get;set;}
        public List<EstadoADto> Estados {get;set;}
    
    }
}