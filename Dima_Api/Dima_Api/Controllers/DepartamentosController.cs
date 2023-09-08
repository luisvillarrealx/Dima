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
    public class DepartamentosController : ApiController
    {
        DepartamentosModel model = new DepartamentosModel();


        [Route("api/ConsultarDepartamentos")]
        [HttpGet]
        [AllowAnonymous]
        public List<DepartamentosEnt> ConsultarDepartamentos()
        {
            return model.ConsultarDepartamentos();
        }
    }
}
