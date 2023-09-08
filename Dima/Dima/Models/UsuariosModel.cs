using Dima.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;
using static System.Net.WebRequestMethods;

namespace Dima.Models
{
    public class UsuariosModel
    {
        public UsuariosEnt ValidarUsuario(UsuariosEnt usuarios)
        {
            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(usuarios);
                string url = "https://localhost:44315/api/ValidarUsuario";
                HttpResponseMessage respuesta = client.PostAsync(url, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<UsuariosEnt>().Result;

                return usuarios; 
            }
        }

        public int RegistrarUsuario(UsuariosEnt usuarios)
        {
            if(usuarios.idRol == 0)
            {
                usuarios.idRol = 3;
            }
            
            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(usuarios);
                string url = "https://localhost:44315/api/RegistrarUsuario";
                HttpResponseMessage respuesta = client.PostAsync(url, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<int>().Result;

                return 0;
            }
        }

        public void RecuperarContrasena(UsuariosEnt usuarios)
        {

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(usuarios);
                string url = "https://localhost:44315/api/RecuperarContrasena";
                HttpResponseMessage respuesta = client.PostAsync(url, body).GetAwaiter().GetResult();
            }
            
        }

        public string ValidarCorreo(string correo)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44315/api/ValidarCorreo?correo=" + correo;
                HttpResponseMessage respuesta = client.GetAsync(url).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<string>().Result;

                return "ERROR";
            }
            
        }


        public List<UsuariosEnt> ConsultarUsuarios()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44315/api/ConsultarUsuarios";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());

                HttpResponseMessage respuesta = client.GetAsync(url).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)

                    return respuesta.Content.ReadFromJsonAsync<List<UsuariosEnt>>().Result;



                return new List<UsuariosEnt>();
            }
        }
        public UsuariosEnt ConsultarUsuario(long q)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44315/api/ConsultarUsuario?q=" + q;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                HttpResponseMessage respuesta = client.GetAsync(url).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<UsuariosEnt>().Result;

                return new UsuariosEnt();
            }
        }
        public void EditarUsuarios(UsuariosEnt entidad)
        {
            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(entidad);
                string url = "https://localhost:44315/api/EditarUsuarios";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                HttpResponseMessage respuesta = client.PutAsync(url, body).GetAwaiter().GetResult();
            }
        }
    }
}