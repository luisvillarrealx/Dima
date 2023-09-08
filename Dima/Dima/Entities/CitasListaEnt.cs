using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dima.Entities
{
    public class CitasListaEnt
    {
        public int idCita { get; set; }
        public string asunto { get; set; }
        public string nombrePaciente { get; set; }
        public DateTime? fecha { get; set; }
        public string departamento { get; set; }
        public string psicologo { get; set; }
        public string numeroTelefonico { get; set; }
        public string correoPaciente { get; set; }
        public string comentarioDeSesion { get; set; }
        public bool realizada { get; set; }
        public string codigoCita { get; set; }
    }
}