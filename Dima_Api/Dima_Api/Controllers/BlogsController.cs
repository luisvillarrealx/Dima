using Dima_Api.Entities;
using Dima_Api.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dima_Api.Controllers
{
    public class BlogsController : ApiController
    {
        BlogsModel blogModel = new BlogsModel();

        [Authorize]
        [Route("api/InsertarBlog")]
        [HttpPost]
        public int InsertarBlog(BlogsEnt blogs)
        {
            return blogModel.InsertarBlog(blogs);
        }

        [Authorize]
        [Route("api/ConsultarBlogs")]
        [HttpGet]
        public List<BlogsEnt> ConsultarBlogs()
        {
            return blogModel.ConsultarBlogs();
        }

        [Authorize]
        [HttpPut]
        [Route("api/ActualizarBlog")]
        public int ActualizarBlog(BlogsEnt blogs)
        {
            return blogModel.ActualizarBlog(blogs);
        }

        [Authorize]
        [Route("api/EliminarBlog")]
        [HttpDelete]
        public int EliminarBlog(int idBlog)
        {
            return blogModel.EliminarBlog(idBlog);
        }

        [Authorize]
        [Route("api/ConsultarCategorias")]
        [HttpGet]
        public List<CategoriasEnt> ConsultarCategorias()
        {
            return blogModel.ConsultarCategorias();
        }

    }
}