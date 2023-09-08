using Dima_Api.Entities;
using Dima_Api.ModeloBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dima_Api.Models
{
    public class RolesModel
    {
        public List<RolesEnt> ConsultarRoles()
        {
            using (var conexion = new docmedEntities())
            {
                List<RolesEnt> respuesta = new List<RolesEnt>();
                var datosBD = (from x in conexion.roles
                               select x).ToList();
                if (datosBD.Count > 0)
                {
                    foreach (var item in datosBD)
                    {
                        respuesta.Add(new RolesEnt
                        {
                            idRol = item.idRol,
                            nombreRol = item.nombreRol
                        });
                    }
                }
                return respuesta;
            }
        }
    }
}