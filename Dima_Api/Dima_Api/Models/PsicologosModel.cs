using Dima_Api.Entities;
using Dima_Api.ModeloBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dima_Api.Models
{
    public class PsicologosModel
    {
        public List<PsicologosEnt> ConsultarPsicologos()
        {
            List<PsicologosEnt> listaPsicologos = new List<PsicologosEnt>();
            using ( var conexion = new docmedEntities())
            {
                var consulta = conexion.ConsultarPsicologos().ToList();

                if (consulta.Count() > 0)
                {
                    foreach (var item in consulta)
                    {
                        PsicologosEnt psicologos = new PsicologosEnt(item.nombre, item.idInformacionPsicologo, item.idUsuario, item.foto, item.universidad, item.gradoAcademico, item.descripcion);
                        listaPsicologos.Add(psicologos);
                    }
                }
            }

            return listaPsicologos;
        }

        public PsicologosEnt ConsultarPsicologo(int idPsicologo)
        {
            using (var conexion = new docmedEntities())
            {
                var consulta =  conexion.ConsultarPsicologo(idPsicologo).FirstOrDefault();

                if (consulta != null)
                {
                    string foto = consulta.foto;
                    
                    PsicologosEnt psicologos = new PsicologosEnt(consulta.nombre
                                                                    , consulta.idInformacionPsicologo
                                                                    , consulta.idUsuario
                                                                    , foto
                                                                    , consulta.universidad
                                                                    , consulta.gradoAcademico
                                                                    , consulta.descripcion);
                    return psicologos;
                }

                return null;
            }
        }
    }
}