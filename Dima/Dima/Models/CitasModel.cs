using Dima.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;

namespace Dima.Models
{
    public class CitasModel
    {
        public string AgendarCita(CitasEnt entidad)
        {
            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(entidad);
                string url = "https://localhost:44315/api/AgendarCita";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer","TOEKN" ); 
                HttpResponseMessage respuesta = client.PostAsync(url, body).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<string>().Result;

                return String.Empty;
            }
        }

        public List<CitasListaEnt> ConsultarCitas()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44315/api/ConsultarCitas";
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                HttpResponseMessage respuesta = client.GetAsync(url).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                
                    return respuesta.Content.ReadFromJsonAsync<List<CitasListaEnt>>().Result;


                
                return new List<CitasListaEnt>();
            }
        }
    }
}