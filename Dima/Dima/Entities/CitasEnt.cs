using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dima.Entities
{
    public class CitasEnt
    {
        public int? idCita { get; set; }
        public int? idPaciente { get; set; }
        public string asunto { get; set; }
        public string nombrePaciente { get; set; }
        public DateTime? fecha { get; set; }
        public int? departamento { get; set; }
        public int? idPsicologo { get; set; }
        public string numeroTelefonico { get; set; }
        public string correoPaciente { get; set; }
        public string comentarioDeSesion { get; set; }
        public bool realizada { get; set; }
        public string codigoCita { get; set; }
    }
}