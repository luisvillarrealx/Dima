using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dima.Entities
{
    public class LogsEnt
    {
        public int idError { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }
        public int idUsuario { get; set; }
    }
}