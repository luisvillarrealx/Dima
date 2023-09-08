using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dima.Entities
{
    public class BlogsEnt
    {
        public BlogsEnt() { }

        public BlogsEnt(int idBlog, string titulo, string descripcion, DateTime fecha, string foto, string nombreCategoria)
        {
            this.idBlog = idBlog;
            this.titulo = titulo;
            this.descripcion = descripcion;
            this.fecha = fecha;
            this.foto = foto;
            this.nombreCategoria = nombreCategoria;
        }

        public int idBlog { get; set; }
        public int? idUsuario { get; set; }
        public int? idCategoria { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }
        public string foto { get; set; }
        public string nombreCategoria { get; set; }

        public HttpPostedFileBase archivo { get; set; }
    }
}