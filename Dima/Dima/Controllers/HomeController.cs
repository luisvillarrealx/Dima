using Dima.Entities;
using Dima.Models;
using Dima.App_Start;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Dima.Controllers
{
    [OutputCache(NoStore=true,Duration=0)]
    public class HomeController : Controller
    {
        UsuariosModel usuariosModel = new UsuariosModel();
        LogsModel logsModel = new LogsModel();
        DepartamentosModel departamentosModel = new DepartamentosModel();
        PsicologosModel psicologosModel = new PsicologosModel();
        CitasModel citasModel = new CitasModel();
        RolModels rolesModel = new RolModels();

        [HttpGet]
        public ActionResult Index()
        {
            Session.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Inicio(UsuariosEnt entidad)
        {
            try
            {
                var resultado = usuariosModel.ValidarUsuario(entidad);

                if (resultado != null && RecaptchaValido())
                {
        

                    Session["idRol"] = resultado.idRol;
                    Session["idUsuario"] = resultado.idUsuario;
                    Session["correo"] = resultado.correo;
                    Session["consecutivo"] = resultado.consecutivoUsuario;
                    Session["token"] = resultado.token; 
                    return RedirectToAction("Inicial", "Home");
                }
                else
                {
                    ViewBag.mensaje = "Sus credenciales no fueron validados";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                RegistrarLog(ex, ControllerContext);
                return View("Index");
            }
        }
        [HttpGet]
        [FiltroPersonalizadoSesion]
        public ActionResult EntrarSistema()
        {

            ViewBag.ListadoDepartamentos = departamentosModel.ConsultarDepartamentos();
            ViewBag.ListadoPsicologos = psicologosModel.ConsultarPsicologos();

            return View("Inicio");
        }

        public bool RecaptchaValido()
        {
            var resultado = false;
            var recaptchaResultado = Request.Form["g-recaptcha-response"];
            var secretKey = ConfigurationManager.AppSettings["SecretKey"];
            var apiURL = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestURL = string.Format(apiURL, secretKey, recaptchaResultado);
            var request = (HttpWebRequest)WebRequest.Create(requestURL);
            using (WebResponse respuesta = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(respuesta.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var esValido = jResponse.Value<bool>("success");
                    resultado = esValido ? true : false;
                }
            }
            return resultado;
        }

        [HttpGet]
        [FiltroPersonalizadoSesion]
        public ActionResult Inicial() 
        {
            ViewBag.ListadoDepartamentos = departamentosModel.ConsultarDepartamentos();
            ViewBag.ListadoPsicologos = psicologosModel.ConsultarPsicologos();

            if (Session["idRol"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            try 
            { 
              return View("Inicio"); 
            }
            catch (Exception ex) 
            {
                RegistrarLog(ex, ControllerContext);
                return View("Index"); 
            }
        }


        [HttpGet]
        [FiltroPersonalizadoSesion]
        public ActionResult CerrarSesion()
        {
            try
            {
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                RegistrarLog(ex, ControllerContext);
                return View();
            }
        }

        [HttpGet]
        public ActionResult Registro()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                RegistrarLog(ex, ControllerContext);
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult Registro(UsuariosEnt entidad)
        {
            try
            {
                if (usuariosModel.RegistrarUsuario(entidad) > 0)
                    return View("Index");
                else
                {
                    ViewBag.mensaje = "No se pudo registrar su cuenta";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                RegistrarLog(ex, ControllerContext);
                return View("Index");
            }
        }

        [HttpPost]
        [FiltroPersonalizadoSesion]
        public ActionResult AgendarCita(UsuariosEnt entidad)
        {
            return View("Inicio");
        }

        [HttpGet]
        public ActionResult RecuperarContrasena()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                RegistrarLog(ex,ControllerContext);
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult RecuperarContrasena(UsuariosEnt entidad)
        {
            try
            {
                usuariosModel.RecuperarContrasena(entidad);
                return View("Index");
            }
            catch (Exception ex)
            {
                RegistrarLog(ex, ControllerContext);
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult ValidarCorreo(string correo)
        {
            return Json(usuariosModel.ValidarCorreo(correo), JsonRequestBehavior.AllowGet);
        }

        private void RegistrarLog(Exception ex, ControllerContext ubicacion)
        {
            LogsEnt log = new LogsEnt();
            log.descripcion = ubicacion.RouteData.Values["controller"].ToString() + "-" + ubicacion.RouteData.Values["action"].ToString();
            logsModel.RegistrarBitacora(log);
        }

        [HandleError]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ConsultarUsuarios()
        {

            if (Session["idRol"] == null)
            {
                return RedirectToAction("Index", "Home");
            } 
            else if (Session["idRol"].ToString() != "1")
            {
                return RedirectToAction("Error", "Home");
            }
            ViewBag.ListaUsuarios = usuariosModel.ConsultarUsuarios();
            ViewBag.ListadoDepartamentos = departamentosModel.ConsultarDepartamentos();
            ViewBag.ListadoPsicologos = psicologosModel.ConsultarPsicologos();

            return View();
        }

        [HttpGet]
        public ActionResult EditarUsuarios(long q)
        {
             var resultado = usuariosModel.ConsultarUsuario(q);

             ViewBag.ListadoDepartamentos = departamentosModel.ConsultarDepartamentos();
            ViewBag.ListadoPsicologos = psicologosModel.ConsultarPsicologos(); 
            ViewBag.ListadoRoles = rolesModel.ConsultarRoles();

            return View(resultado);
        }
        [HttpPost]
        public ActionResult EditarUsuarios(UsuariosEnt entidad)
        {
            usuariosModel.EditarUsuarios(entidad);
            return RedirectToAction("ConsultarUsuarios", "Home");
        }
    }
}