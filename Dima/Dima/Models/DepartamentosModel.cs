using Dima.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Dima.Models
{
    public class DepartamentosModel
    {

        public List<SelectListItem> ConsultarDepartamentos()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44315/api/ConsultarDepartamentos";
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["Token"].ToString());
                 HttpResponseMessage respuesta = client.GetAsync(url).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    var datos = respuesta.Content.ReadFromJsonAsync<List<DepartamentosEnt>>().Result;
                    List<SelectListItem> DepartamentosCombo = new List<SelectListItem>();
                    foreach (var item in datos)
                    {
                        DepartamentosCombo.Add(new SelectListItem 
                        { 
                            Value = item.idDepartamento.ToString(), 
                            Text = item.nombreDepartamento
                        });
                    }
                    return DepartamentosCombo;
                }

                return new List<SelectListItem>();
            }
        }
    }
}