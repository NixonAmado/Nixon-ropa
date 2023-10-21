using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class MunicipioPDto
    {
        public int Id {get;set;}
        public string Nombre { get; set; }
        public int IdDepartamentoFk {get;set;}
    }
    public class MunicipioNombreDto
    {
        public int Id {get;set;}
        public string Nombre { get; set; }
    }

}