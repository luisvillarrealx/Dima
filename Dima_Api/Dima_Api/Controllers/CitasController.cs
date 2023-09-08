using Dima_Api.Entities;
using Dima_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dima_Api.Controllers
{
    public class CitasController : ApiController
    {
        CitasModel citasModel = new CitasModel();

        // POST api/<controller>
        [HttpPost]
        [Route("api/AgendarCita")]
        public string AgendarCita(CitasEnt citas)
        {
            return citasModel.AgendarCita(citas);
        }
        [HttpGet]
        [Route("api/ConsultarCitas")]
        public List<CitasListaEnt> ConsultarCitas()
        {
            return citasModel.ConsultarCitas();
        }
    }
}