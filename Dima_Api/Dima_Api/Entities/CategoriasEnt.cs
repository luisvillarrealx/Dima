using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dima_Api.Entities
{
    public class CategoriasEnt
    {
        public CategoriasEnt(int idCategoria, string nombre)
        {
            this.idCategoria = idCategoria;
            this.nombre = nombre;
        }

        public int idCategoria { get; set; }
        public string nombre { get; set; }
    }
}