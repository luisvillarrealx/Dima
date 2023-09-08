using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace Dima_Api.Entities
{
    public class UsuariosEnt
    {
        public long idUsuario { get; set; }
        public long consecutivoUsuario { get; set; }

        public long? cedula { get; set; }

        public string nombre { get; set; }

        public string primerApellido { get; set; }

        public string segundoApellido { get; set; }

        [JsonPropertyName("correo")]
        public string correo { get; set; }

        public string contrasena { get; set; }

        public int? idRol { get; set; }

        public bool activo { get; set; }

        public int? idDepartamento { get; set; }
        public string token { get; set; }


    }
}