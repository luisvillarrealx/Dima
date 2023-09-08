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
    public class RolesController : ApiController
    {
        RolesModel model = new RolesModel();


        [Route("api/ConsultarRoles")]
        [HttpGet]
        [AllowAnonymous]
        public List<RolesEnt> ConsultarRoles()
        {
            return model.ConsultarRoles();
        }
    }
}
