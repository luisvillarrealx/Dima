using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dima.Entities
{
    public class PsicologsEnt
    {

        public string nombre { get; set; }
        public int? idInformacionPsicologo { get; set; }
        public int idPsicologo { get; set; }
        public string foto { get; set; }
        public string universidad { get; set; }
        public string gradoAcademico { get; set; }
        public string descripcion { get; set; }
    }
}