using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    [CustomAuthorize((int)Roles.Lider)]
    public class GestionCancionController : Controller
    {
        // GET: GestionCancion
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

        public ActionResult Gestion(int id)
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
                    TempData["Message"] = "No existe esa canción";
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




        public ActionResult MostrarVideo(String Url_version, int Id)
        {

            //[RegularExpression(@"https:\/\/youtu.be[^' '\n\r]+", ErrorMessage = "Formato no permitido. Ejemplo de formato: https://youtu.be/......")]
            try
            {

                Regex rx = new Regex(@"https:\/\/youtu.be[^' '\n\r]+",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
                IServiceGestionCancion serviceGestionCancion = new ServiceGestionCancion();


                ViewBag.Id = Id;

                if (rx.IsMatch(Url_version))
                {
                    ViewBag.Link = serviceGestionCancion.FormatURL(Url_version);
                }
                else
                {
                    return RedirectToAction("Gestion");
                }
                return PartialView("_PartialVideo");

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
        public ActionResult Save(int Id, string Link)
        {
            try
            {
                IServiceCancion serviceCancion = new ServiceCancion();
                Cancion cancion = serviceCancion.GetCancionByID(Id);
                cancion.Url_version = Link;
                if (serviceCancion.Save(cancion) != null)
                {
                    TempData["Action"] = "U";
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    TempData["Action"] = "E";
                    TempData["Message"] = "Error al procesar los datos! ";
                    TempData.Keep();
                }
                return RedirectToAction("Index");
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
        public JsonResult Delete(int id)
        {
            var status = false;
            try
            {
                IServiceCancion serviceCancion = new ServiceCancion();
                Cancion cancion = serviceCancion.GetCancionByID(id);
                cancion.Url_version = null;
                if (serviceCancion.Save(cancion) != null)
                {
                    status = true;
                    TempData["Action"] = "U";
                    TempData.Keep();
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    TempData["Action"] = "E";
                    TempData["Message"] = "No existe esa canción";
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





    }
}