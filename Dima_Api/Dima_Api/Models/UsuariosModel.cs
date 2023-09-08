using Dima_Api.App_Start;
using Dima_Api.Entities;
using Dima_Api.ModeloBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dima_Api.Models
{
    public class UsuariosModel
    {
        LogsModel logsModel;
        TokenGenerate modelToken = new TokenGenerate();

        public UsuariosModel() {
            logsModel = new LogsModel();
        }
        
        public UsuariosEnt ValidarUsuario(UsuariosEnt usuarios)
        {
            using (var conexion = new docmedEntities())
            {
                var consulta = conexion.ValidarUsuario(usuarios.correo, usuarios.contrasena).FirstOrDefault();


                if (consulta != null)
                {
                    UsuariosEnt respuesta = new UsuariosEnt();
                    respuesta.idUsuario = consulta.idUsuario;
                    respuesta.idRol = consulta.idRol;  
                    respuesta.correo = consulta.correo;
                    respuesta.consecutivoUsuario = consulta.idUsuario;
                    respuesta.token = modelToken.GenerateTokenJwt(consulta.idUsuario.ToString());
                    return respuesta;
                }

                return null;
            }
        }

        public int RegistrarUsuario(UsuariosEnt usuarios)
        {
            using (var conexion = new docmedEntities())
            {
                return conexion.RegistrarCredenciales(usuarios.correo, usuarios.contrasena);
            }
        }

        public string ValidarCorreo(string correo)
        {
            using (var conexion = new docmedEntities())
            {
                //var consulta = conexion.ValidarCorreo(correo).FirstOrDefault();
                var consulta = (from x in conexion.usuarios
                                 where x.correo == correo
                                 select x).FirstOrDefault();

                if (consulta == null)
                    return string.Empty;
                else
                {
                    return "Su cuenta de correo ya se encuentra registrada.";
                }
            }
        }

        public void RecuperarContrasena(UsuariosEnt usuarios)
        {
            string contrasena = "";
            string asunto = "Recuperar Contraseña";

            using (var conexion = new docmedEntities())
            {
                contrasena = conexion.RecuperarContrasena(usuarios.correo).FirstOrDefault();
            }

            string body = "Su contraseña es la siguiente: " + contrasena;

            logsModel.EnviarCorreo(usuarios.correo, asunto, body);
        }

        public List<UsuariosEnt> ConsultarUsuarios()
        {
            using (var conexion = new docmedEntities())
            {
                List<UsuariosEnt> respuesta = new List<UsuariosEnt>();

                var datosusuarios = (from x in conexion.usuarios
                                     select x).ToList();


                if (datosusuarios.Count > 0)
                {
                    foreach (var item in datosusuarios)
                    {
                        respuesta.Add(new UsuariosEnt
                        {
                            idUsuario=item.idUsuario,
                            cedula=item.cedula,
                            nombre=item.nombre,
                            primerApellido=item.primerApellido,
                            segundoApellido=item.segundoApellido,
                            correo=item.correo,
                            contrasena=item.contrasena,
                            idRol=item.idRol,
                            idDepartamento=item.idDepartamento
                        });
                    }
                }
                return respuesta;
            }

        }

        public UsuariosEnt ConsultarUsuario(long q)
        {
            using (var conexion = new docmedEntities())
            {
                UsuariosEnt respuesta = new UsuariosEnt();
                var datosBD = (from x in conexion.usuarios
                               where x.idUsuario == q
                               select x).FirstOrDefault();

                if (datosBD != null)
                {
                    respuesta.idUsuario = datosBD.idUsuario;
                    respuesta.nombre= datosBD.nombre;
                    respuesta.primerApellido= datosBD.primerApellido;
                    respuesta.segundoApellido= datosBD.segundoApellido;
                    respuesta.correo= datosBD.correo;
                    respuesta.contrasena= datosBD.contrasena;
                    respuesta.idRol= datosBD.idRol;
                    respuesta.idDepartamento=datosBD.idDepartamento;

                }

                return respuesta;
            }
        }
        public void EditarUsuarios(UsuariosEnt entidad)
        {
            using (var conexion = new docmedEntities())
            {
                var respuesta = (from x in conexion.usuarios
                                 where x.idUsuario == entidad.idUsuario
                                 select x).FirstOrDefault();

                if (respuesta != null)
                {
                    respuesta.idRol = entidad.idRol; 
                    respuesta.activo = entidad.activo;
                    respuesta.idDepartamento = entidad.idDepartamento;
                    respuesta.nombre = entidad.nombre;
                    respuesta.segundoApellido = entidad.segundoApellido;
                    respuesta.primerApellido = entidad.primerApellido;


                    conexion.SaveChanges();
                }
            }
        }
    }
}