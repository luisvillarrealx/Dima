using Dima.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;
using System.Web.Mvc;

namespace Dima.Models
{
    public class BlogsModel
    {
        public int InsertarBlog(BlogsEnt blogs)
        {
            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(blogs);
                string url = "https://localhost:44315/api/InsertarBlog";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                HttpResponseMessage respuesta = client.PostAsync(url, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<int>().Result;

                return 0;
            }
        }

        public List<BlogsEnt> ConsultarBlogs()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44315/api/ConsultarBlogs";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                HttpResponseMessage respuesta = client.GetAsync(url).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    return respuesta.Content.ReadFromJsonAsync<List<BlogsEnt>>().Result;
                }

                return new List<BlogsEnt>();
            }
        }

        public int ActualizarBlog(BlogsEnt blogs)
        {
            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(blogs);
                string url = "https://localhost:44315/api/ActualizarBlog";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                HttpResponseMessage respuesta = client.PutAsync(url, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<int>().Result;

                return 0;
            }
        }

       

        public int EliminarBlog(int idBlog)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44315/api/EliminarBlog?idBlog=" + idBlog;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                HttpResponseMessage respuesta = client.DeleteAsync(url).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<int>().Result;

                return 0;
            }
        }

        public List<SelectListItem> ConsultarCategorias()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44315/api/ConsultarCategorias";
                
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                HttpResponseMessage respuesta = client.GetAsync(url).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    var datos = respuesta.Content.ReadFromJsonAsync<List<CategoriasEnt>>().Result;
                    List<SelectListItem> CategoriasCombo = new List<SelectListItem>();
                    foreach (var item in datos)
                    {
                        CategoriasCombo.Add(new SelectListItem
                        {
                            Value = item.idCategoria.ToString(),
                            Text = item.nombre
                        });
                    }
                    return CategoriasCombo;
                }

                return new List<SelectListItem>();
            }
        }
    }
}