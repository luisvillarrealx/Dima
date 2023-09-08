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
    public class LogsController : ApiController
    {
        public LogsModel logsModel;

        public LogsController() {

            logsModel = new LogsModel();
        
        }

        [HttpPost]
        [Route("api/RegistrarError")]
        public void RegistrarError(LogsEnt logs)
        {
            logsModel.RegistrarError(logs);
        }

    }
}