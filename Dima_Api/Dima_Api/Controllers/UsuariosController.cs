 using Dima_Api.Entities;
using Dima_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Dima_Api.Controllers
{
    public class UsuariosController : ApiController
    {
        UsuariosModel model = new UsuariosModel();
        [HttpPost]
        [Route("api/ValidarUsuario")]
        public UsuariosEnt ValidarUsuario(UsuariosEnt usuarios)
        {
            return model.ValidarUsuario(usuarios);
        }

        [HttpPost]
        [Route("api/RegistrarUsuario")]
        public int RegistrarUsuario(UsuariosEnt usuarios)
        {
            return model.RegistrarUsuario(usuarios);
        }

        [HttpGet]
        [Route("api/ValidarCorreo")]
        public string ValidarCorreo(string correo)
        {
            return model.ValidarCorreo(correo);
        }

        [HttpPost]
        [Route("api/RecuperarContrasena")]
        public void RecuperarContrasena(UsuariosEnt usuarios)
        {
            model.RecuperarContrasena(usuarios);
        }

        [HttpGet]
        [Route("api/ConsultarUsuarios")]
        public List<UsuariosEnt> ConsultarUsuarios()
        {
            return model.ConsultarUsuarios();
        }

        [HttpGet]
        [Authorize]
        [Route("api/ConsultarUsuario")]
        public UsuariosEnt ConsultarUsuario(long q)
        {
            return model.ConsultarUsuario(q);
        }

        [HttpPut]
        [Route("api/EditarUsuarios")]
        public void EditarUsuarios(UsuariosEnt usuarios)
        {
            model.EditarUsuarios(usuarios);
        }
    }
}