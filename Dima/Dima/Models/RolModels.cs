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
    public class RolModels
    {
        public List<SelectListItem> ConsultarRoles()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44315/api/ConsultarRoles";
                HttpResponseMessage respuesta = client.GetAsync(url).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    var datos = respuesta.Content.ReadFromJsonAsync<List<RolesEnt>>().Result;
                    List<SelectListItem> RolesCombo = new List<SelectListItem>();
                    foreach (var item in datos)
                    {
                        RolesCombo.Add(new SelectListItem
                        {
                            Value = item.idRol.ToString(),
                            Text = item.nombreRol
                        });
                    }
                    return RolesCombo;
                }

                return new List<SelectListItem>();
            }
        }
    }
}