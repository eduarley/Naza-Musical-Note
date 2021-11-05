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
    [CustomAuthorize((int)Roles.Lider, (int)Roles.Integrante)]
    public class CancionController : Controller
    {
        // GET: Cancion
        public ActionResult Index()
        {
            List<Cancion> lista = null;
            
            try
            {
                IServiceCancion serviceCancion = new ServiceCancion();
                lista = serviceCancion.GetCanciones();
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




        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Create()
        {
            //Para que el toggle del estado se ponga en activo por defecto
            Cancion cancion = new Cancion()
            {
                Estado = true
            };

            return View(cancion);
        }

        public ActionResult Details(int id)
        {
            IServiceCancion serviceCancion = new ServiceCancion();
            IServiceGestionCancion serviceGestionCancion = new ServiceGestionCancion();
            Cancion cancion = null;
            ViewBag.UrlFormat = null;
            try
            {
                cancion = serviceCancion.GetCancionByID(id);
                

                
                if (cancion == null)
                {
                    TempData["Message"] = "No existe la canción indicada";
                    @TempData["Action"] = "E";
                    TempData.Keep();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.UrlFormat = serviceGestionCancion.FormatURL(cancion.Url_version);
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
            return View(cancion);
        }

        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Edit(int id)
        {

            Cancion cancion = null;
            IServiceCancion serviceCancion = new ServiceCancion();
    
            try
            {
                cancion = serviceCancion.GetCancionByID(id);
                if (cancion != null)
                    return View(cancion);
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
        [CustomAuthorize((int)Roles.Lider)]
        public JsonResult Delete(int id)
        {
            var status = false;
            try
            {
                IServiceCancion serviceCancion = new ServiceCancion();
                if (serviceCancion.DeteteCancion(id))
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
                    TempData["Message"] = "No existe la canción indicada";
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
            }

            return new JsonResult { Data = new { status = status } };
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Save(Cancion cancion)
        {
            cancion.Fecha_creacion = DateTime.Now;
            
            try
            {
                if (ModelState.IsValid)
                {
                    IServiceCancion serviceCancion = new ServiceCancion();
                    serviceCancion.Save(cancion);
                    TempData["Action"] = "S";
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    TempData["Action"] = "E";
                    TempData["Message"] = "Error al procesar los datos! ";
                    TempData.Keep();
                    //return RedirectToAction("Create", cancion);
                    return RedirectToAction("Index");
                }
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