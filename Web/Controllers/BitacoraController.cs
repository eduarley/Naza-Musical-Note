using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{

    [CustomAuthorize((int)Roles.Lider)]
    public class BitacoraController : Controller
    {
        // GET: Bitacora
        public ActionResult Index()
        {
            List<Bitacora_RolServicio> lista = null;

            try
            {
                IServiceBitacora serviceBitacora = new ServiceBitacora();
                lista = serviceBitacora.GetBitacora_RolServicios();
                return View(lista);
            }
            catch (Exception ex)
            {

                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                ViewBag.Message = TempData["Message"];
                @TempData["Action"] = "E";
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }



        public ActionResult Details(int id)
        {
            try
            {
                IServiceBitacora serviceBitacora = new ServiceBitacora();
                IServiceRolServicio serviceRolServicio = new ServiceRolServicio();

                Bitacora_RolServicio bitacora_RolServicio = serviceBitacora.GetBitacora_RolServicioById(id);
                ViewBag.rolServicio = serviceRolServicio.GetRolServicioPorID(id);


                return View(bitacora_RolServicio);

            }
            catch (Exception ex)
            {

                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                ViewBag.Message = TempData["Message"];
                @TempData["Action"] = "E";
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }

    }
}