using Dima.App_Start;
using Dima.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dima.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class PsicologosController : Controller
    {
        DepartamentosModel departamentosModel = new DepartamentosModel();
        PsicologosModel psicologosModel = new PsicologosModel();


        UsuariosModel usuariosModel = new UsuariosModel();


        [HttpGet]
        [FiltroPersonalizadoSesion]

        public ActionResult Index()
        {
            if (Session["idRol"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ListadoDepartamentos = departamentosModel.ConsultarDepartamentos();
            ViewBag.ListadoPsicologos = psicologosModel.ConsultarPsicologos();

            return View();
        }
    }
}