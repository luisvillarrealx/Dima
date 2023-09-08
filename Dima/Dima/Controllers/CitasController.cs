using Dima.App_Start;
using Dima.Entities;
using Dima.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Dima.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class CitasController : Controller
    {

        CitasModel citasModel = new CitasModel();
        LogsModel logsModel = new LogsModel();
        DepartamentosModel departamentosModel = new DepartamentosModel();
        PsicologosModel psicologosModel = new PsicologosModel();
        UsuariosModel usuariosModel = new UsuariosModel(); 

        [HttpGet]
        [FiltroPersonalizadoSesion]
        public ActionResult Index()
        {
            ViewBag.ListadoDepartamentos = departamentosModel.ConsultarDepartamentos();
            ViewBag.ListadoPsicologos = psicologosModel.ConsultarPsicologos();
            return View();
        }

        [HttpGet]
        public ActionResult AgendarCita()
        {
            ViewBag.ListadoDepartamentos = departamentosModel.ConsultarDepartamentos();
            ViewBag.ListadoPsicologos = psicologosModel.ConsultarPsicologos();
            return View();
        }

        [HttpPost]
        public ActionResult AgendarCita(CitasEnt citas)
        {
            try
            {
                citas.idPaciente = Convert.ToInt32(Session["idUsuario"]);
                citas.correoPaciente = Session["correo"].ToString();

                var agendar = citasModel.AgendarCita(citas);

                if (agendar != null)
                {
                    return RedirectToAction("Inicial", "Home");
                }

                return RedirectToAction("Inicial", "Home");
            }
            catch(Exception ex)
            {
                RegistrarLog(ex, ControllerContext);
                return RedirectToAction("Inicial", "Home");
            }
            
        }


        [HttpGet]
        [FiltroPersonalizadoSesion]
        public ActionResult ConsultarCitas(CitasEnt citas)
        {
            if (Session["idRol"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ListaCitas = citasModel.ConsultarCitas();
            ViewBag.ListadoDepartamentos = departamentosModel.ConsultarDepartamentos();
            ViewBag.ListadoPsicologos = psicologosModel.ConsultarPsicologos();
            return View(); 
        }

        private void RegistrarLog(Exception ex, ControllerContext ubicacion)
        {
            LogsEnt log = new LogsEnt();
            log.descripcion = ubicacion.RouteData.Values["controller"].ToString() + "-" + ubicacion.RouteData.Values["action"].ToString();
            logsModel.RegistrarBitacora(log);
        }
    }
}