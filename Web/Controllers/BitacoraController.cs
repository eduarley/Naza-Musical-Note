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
                if(bitacora_RolServicio != null)
                {
                    ViewBag.rolServicio = serviceRolServicio.GetRolServicioPorID(bitacora_RolServicio.IdRolServicio);

                }
                else
                {
                    TempData["Message"] = "No existe la bitácora a consultar";
                    ViewBag.Message = TempData["Message"];
                    @TempData["Action"] = "E";
                    TempData.Keep();
                    return RedirectToAction("Index");
                }

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


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var status = false;
            try
            {
                IServiceBitacora serviceBitacora = new ServiceBitacora();
                if (serviceBitacora.DeleteBitacora_RolServicioById(id))
                {
                    status = true;
                    TempData["Action"] = "D";
                    TempData.Keep();
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    TempData["Action"] = "E";
                    TempData["Message"] = "No existe la bitácora indicada";
                    TempData.Keep();
                }
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
            return new JsonResult { Data = new { status = status } };
        }




        [HttpPost]
        public ActionResult DeleteAll()
        {
            var status = false;
            try
            {
                IServiceBitacora serviceBitacora = new ServiceBitacora();
                if (serviceBitacora.DeleteAllBitacora_RolServicio())
                {
                    status = true;
                    TempData["Action"] = "D-all";
                    TempData.Keep();
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    TempData["Action"] = "E";
                    TempData["Message"] = "Hubo un error al eliminar";
                    TempData.Keep();
                }
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
            return new JsonResult { Data = new { status = status } };
        }

    }
}