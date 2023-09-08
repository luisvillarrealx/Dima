using Dima_Api.Entities;
using Dima_Api.ModeloBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dima_Api.Models
{
    public class BlogsModel
    {
        public BlogsModel() { }
        
        public int InsertarBlog(BlogsEnt blogs)
        {
            using(var conexion = new docmedEntities()){

                return conexion.InsertarBlog(blogs.idUsuario,blogs.idCategoria,blogs.titulo,blogs.descripcion,blogs.foto);
            
            }

        }

        public List<BlogsEnt> ConsultarBlogs()
        {
            List<BlogsEnt> listaBlogs = new List<BlogsEnt>();

            using (var conexion = new docmedEntities())
            {
                var consulta = conexion.ConsultarBlogs().ToList();

                if (consulta.Count() > 0)
                {
                    foreach (var blogs in consulta)
                    {
                        BlogsEnt blogsEnt = new BlogsEnt(blogs.idBlog, blogs.titulo, blogs.descripcion, blogs.fecha, blogs.foto, blogs.nombre);
                        listaBlogs.Add(blogsEnt);
                    }
                }

            }

            return listaBlogs;
        }

        public int ActualizarBlog(BlogsEnt blogs)
        {
            using (var conexion = new docmedEntities())
            {

                return conexion.ActualizarBlog(blogs.idBlog,blogs.titulo,blogs.descripcion,blogs.foto);

            }
        }

        public int EliminarBlog(int idBlog)
        {
            using (var conexion = new docmedEntities())
            {

                return conexion.EliminarBlog(idBlog);

            }
        }

        public List<CategoriasEnt> ConsultarCategorias()
        {
            List<CategoriasEnt> listaCategorias = new List<CategoriasEnt>();

            using (var conexion = new docmedEntities())
            {
                var consulta = conexion.ConsultarCategorias().ToList();

                if (consulta.Count() > 0)
                {
                    foreach (var categorias in consulta)
                    {
                        CategoriasEnt categoriasEnt = new CategoriasEnt(categorias.idCategoria,categorias.nombre);
                        listaCategorias.Add(categoriasEnt);
                    }
                    
                }
            }

            return listaCategorias;
        }
    }
}