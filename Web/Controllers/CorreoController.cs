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
    public class CorreoController : Controller
    {
        // GET: Correo
        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Index()
        {
            IServiceCorreo service = new ServiceCorreo();
            CorreoEmisor correo = service.GetCorreo();
            return View(correo);
        }

        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Edit(int id)
        {

            CorreoEmisor correo = null;
            IServiceCorreo serviceCorreo = new ServiceCorreo();

            try
            {
                correo = serviceCorreo.GetCorreo();
                if (correo != null)
                    return View(correo);
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    TempData["Action"] = "E";
                    TempData["Message"] = "No existe la canción indicada";
                    TempData.Keep();
                    return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Save([Bind(Include = "Id,Correo,Clave,Estado")] CorreoEmisor correo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    IServiceCorreo serviceCorreo = new ServiceCorreo();
                    serviceCorreo.Edit(correo);
                    TempData["Action"] = "S";
                }
                //else
                //{
                //    // Valida Errores si Javascript está deshabilitado
                //    Util.ValidateErrors(this);
                //    TempData["Action"] = "E";
                //    TempData["Message"] = "Error al procesar los datos! ";
                //    TempData.Keep();
                //    //return RedirectToAction("Create", cancion);
                //    return RedirectToAction("Index");
                //}
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                ViewBag.Message = TempData["Message"];
                @TempData["Action"] = "E";
                // Redireccion a la captura del Error
                return RedirectToAction("Index");
            }
        }
    }
}