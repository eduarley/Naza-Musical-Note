using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
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
            }
            return View();
        }





        public ActionResult Create()
        {

            List<Estado> listaEstado = new List<Estado>()
            {
                new Estado(){ Descripcion= "Activo", Valor = true},
                new Estado(){ Descripcion= "Inactivo", Valor = false}
            };
            ViewBag.Estado = listaEstado;

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    TempData["Message"] = "Error al procesar los datos! ";
                    TempData.Keep();
                    return RedirectToAction("Create", cancion);
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