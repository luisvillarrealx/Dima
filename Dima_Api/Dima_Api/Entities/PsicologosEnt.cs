using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dima_Api.Entities
{
    public class PsicologosEnt
    {
        public PsicologosEnt(string nombre, int? idInformacionPsicologo, int idPsicologo, string foto, string universidad, string gradoAcademico, string descripcion)
        {
            this.nombre = nombre;
            this.idInformacionPsicologo = idInformacionPsicologo;
            this.idPsicologo = idPsicologo;
            this.foto = foto;
            this.universidad = universidad;
            this.gradoAcademico = gradoAcademico;
            this.descripcion = descripcion;
        }

        public string nombre { get; set; }
        public int? idInformacionPsicologo { get; set; }
        public int idPsicologo { get; set; }
        public string foto { get; set; }
        public string universidad { get; set; }
        public string gradoAcademico { get; set; }
        public string descripcion { get; set; }
    }
}