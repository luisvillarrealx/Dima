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
    public class PsicologosModel
    {

        public List<SelectListItem> ConsultarPsicologos()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44315/api/ConsultarPsicologos";
                
                HttpResponseMessage respuesta = client.GetAsync(url).GetAwaiter().GetResult();

                if (respuesta.IsSuccessStatusCode)
                {
                    var datos = respuesta.Content.ReadFromJsonAsync<List<PsicologsEnt>>().Result;
                    List<SelectListItem> PsicologosCombo = new List<SelectListItem>();
                    foreach (var item in datos)
                    {
                        PsicologosCombo.Add(new SelectListItem
                        {
                            Value = item.idPsicologo.ToString(),
                            Text = item.nombre
                        });
                    }
                    return PsicologosCombo;
                }

                return new List<SelectListItem>();
            }
        }
    }
}