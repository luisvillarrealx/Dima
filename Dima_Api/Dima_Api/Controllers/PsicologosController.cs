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
    public class PsicologosController : ApiController
    {
        PsicologosModel psicologosModel;

        public PsicologosController()
        {
            psicologosModel = new PsicologosModel();
        }

        [Route("api/ConsultarPsicologos")]
        [HttpGet]
        [AllowAnonymous]
        public List<PsicologosEnt> ConsultarPsicologos()
        {
            return psicologosModel.ConsultarPsicologos();
        }

        [Route("api/ConsultarPsicologo")]
        [HttpGet]
        [AllowAnonymous]
        public PsicologosEnt ConsultarPsicologo(int idPsicologo)
        {
            return psicologosModel.ConsultarPsicologo(idPsicologo);
        }
    }
}