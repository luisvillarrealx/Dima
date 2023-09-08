using Dima_Api.Entities;
using Dima_Api.ModeloBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dima_Api.Models
{
    public class DepartamentosModel
    {
        public List<DepartamentosEnt> ConsultarDepartamentos()
        {
            using (var conexion = new docmedEntities())
            {
                List<DepartamentosEnt> respuesta = new List<DepartamentosEnt>();
                var datosBD = (from x in conexion.departamentos
                               select x).ToList();
                if (datosBD.Count > 0)
                {
                    foreach (var item in datosBD)
                    {
                        respuesta.Add(new DepartamentosEnt
                        {
                            idDepartamento = item.idDepartamento,
                            nombreDepartamento = item.nombreDepartamento
                        });
                    }
                }
                return respuesta;
            }
        }
    }
}