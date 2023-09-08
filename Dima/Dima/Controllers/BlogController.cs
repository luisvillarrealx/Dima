using Dima.App_Start;
using Dima.Entities;
using Dima.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Dima.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class BlogController : Controller
    {
        DepartamentosModel departamentosModel = new DepartamentosModel();
        PsicologosModel psicologosModel = new PsicologosModel();
        BlogsModel blogsModel = new BlogsModel();

        [HttpGet]
        [FiltroPersonalizadoSesion]
        public ActionResult Index()
        {
            if (Session["idRol"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ListadoDepartamentos = departamentosModel.ConsultarDepartamentos();
            ViewBag.ListadoPsicologos = psicologosModel.ConsultarPsicologos();
            var consultarBlogs = blogsModel.ConsultarBlogs();
            ViewBag.ListadoBlogs = consultarBlogs.Take(3);
            ViewBag.ConsultarCategoriasBlog = ConsultarCategoriasBlog(consultarBlogs);

            return View();
        }

        [HttpGet]
        public ActionResult ConsultarBlogs()
        {
            ViewBag.ListadoDepartamentos = departamentosModel.ConsultarDepartamentos();
            ViewBag.ListadoPsicologos = psicologosModel.ConsultarPsicologos();
            ViewBag.ListadoBlogs = blogsModel.ConsultarBlogs();
            return View();
        }

        [HttpGet]
        public ActionResult ActualizarBlog(int idBlog)
        {
            ViewBag.ListadoDepartamentos = departamentosModel.ConsultarDepartamentos();
            ViewBag.ListadoPsicologos = psicologosModel.ConsultarPsicologos();
            var resultado = blogsModel.ConsultarBlogs().Where(data => data.idBlog == idBlog).FirstOrDefault();
            ViewBag.foto = resultado.foto;
            return View(resultado);
        }

        [HttpPost]
        public ActionResult ActualizarBlog(BlogsEnt blogs)
        {
            if(blogs.archivo != null)
            {
                String blogImagePath = ConfigurationManager.AppSettings["blogImagePath"].ToString();
                string ruta = Path.Combine(Server.MapPath(blogImagePath), Path.GetFileName(blogs.archivo.FileName));
                blogs.archivo.SaveAs(ruta);

                blogs.foto = blogs.archivo.FileName;
                blogs.archivo = null;
            }
            
            var actualizar = blogsModel.ActualizarBlog(blogs);

            if (actualizar > 0)
            {
                return RedirectToAction("ConsultarBlogs", "Blog");
            }

            return View();
        }

        [HttpPost]
        public ActionResult InsertarBlog(BlogsEnt blogs)
        {
            String blogImagePath = ConfigurationManager.AppSettings["blogImagePath"].ToString();
            string ruta = Path.Combine(Server.MapPath(blogImagePath), Path.GetFileName(blogs.archivo.FileName));
            blogs.archivo.SaveAs(ruta);

            blogs.foto = blogs.archivo.FileName;
            blogs.archivo = null;
            blogs.idUsuario = int.Parse(Session["idUsuario"].ToString());

            var insertar = blogsModel.InsertarBlog(blogs);

            return RedirectToAction("ConsultarBlogs", "Blog");
        }

        public List<string> ConsultarCategoriasBlog(List<BlogsEnt> blogs)
        {
            List<string> result = new List<string>();
            string categorias = "";

            
            foreach (var list in blogs.GroupBy(data=>data.nombreCategoria))
            {
                var contador = blogs.Where(data => data.nombreCategoria == list.Key).Count();
                if (contador > 0)
                {
                    categorias = list.Key+ " (" + contador.ToString() + ")";

                    result.Add(categorias);
                }
            }
            return result;
        }
    }
}