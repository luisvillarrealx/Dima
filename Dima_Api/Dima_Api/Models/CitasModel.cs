using Dima_Api.Entities;
using Dima_Api.ModeloBD;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace Dima_Api.Models
{
    public class CitasModel
    {
        LogsModel logsModel;

        public CitasModel()
        {
            logsModel = new LogsModel();
        }

        public string AgendarCita(CitasEnt citas)
        {
            string codigo = String.Empty;
            string asunto = "Confirmación de Cita";

           using (var conexion = new docmedEntities())
            {
                ObjectParameter output = new ObjectParameter("respuesta", typeof(string));
                conexion.AgendarCita(
                    citas.idPaciente,
                    citas.asunto,
                    citas.nombrePaciente,
                    citas.fecha,
                    citas.departamento,
                    citas.idPsicologo,
                    citas.numeroTelefonico,
                    citas.correoPaciente,
                    citas.comentarioDeSesion,
                    output);
                codigo = output.Value.ToString();
            }

            string body = "Su código de cita es el siguiente: " + codigo;

            logsModel.EnviarCorreo(citas.correoPaciente, asunto, body);

            return codigo;
        }
        public List<CitasListaEnt> ConsultarCitas()
        {
                using (var conexion = new docmedEntities())
                {
                    List<CitasListaEnt> respuesta = new List<CitasListaEnt>();

                    var datoscitas = (from citas in conexion.citas
                                      join psicologos in conexion.usuarios on citas.idPsicologo equals psicologos.idUsuario
                                      join departamentos in conexion.departamentos on citas.departamento equals departamentos.idDepartamento
                                     select new
                                     {
                                         citas
                                         , psicologos.nombre
                                         , psicologos.primerApellido
                                         , psicologos.segundoApellido
                                         , departamentos.nombreDepartamento
                                     }).ToList();

                    if (datoscitas.Count > 0)
                    {
                        foreach (var item in datoscitas)
                        {
                            respuesta.Add(new CitasListaEnt
                            {
                                idCita = item.citas.idCita,
                                asunto = item.citas.asunto,
                                nombrePaciente = item.citas.nombrePaciente,
                                fecha = item.citas.fecha,
                                departamento = item.nombreDepartamento,
                                psicologo = item.nombre +" "+item.primerApellido +" "+ item.segundoApellido,
                                numeroTelefonico = item.citas.numeroTelefonico,
                                correoPaciente = item.citas.correoPaciente,
                                comentarioDeSesion = item.citas.comentarioDeSesion,
                                codigoCita = item.citas.codigoCita,
                            });
                        }
                    }
                    return respuesta;
                }
            
        }
}
}