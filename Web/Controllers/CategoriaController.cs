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
    public class CategoriaController : Controller
    {
        IServiceCategoria serviceCategoria = new ServiceCategoria();


        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Index()
        {
            List<Categoria> lista = null;
            try
            {
                lista = serviceCategoria.GetCategorias();
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


        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Create()
        {
            return View();
        }

        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Details(int id)
        {

            Categoria categoria = null;
            try
            {
                categoria = serviceCategoria.GetCategoriaByID(id);
                if (categoria == null)
                {
                    TempData["Message"] = "No existe la categoría indicada";
                    @TempData["Action"] = "E";
                    TempData.Keep();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                @TempData["Action"] = "E";
                TempData.Keep();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Edit(int id)
        {

            Categoria categoria = null;
            try
            {
                categoria = serviceCategoria.GetCategoriaByID(id);
                if (categoria != null)
                    return View(categoria);
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    TempData["Action"] = "E";
                    TempData["Message"] = "No existe la categoría indicada";
                    TempData.Keep();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
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
                if (serviceCategoria.DeteteCategoria(id))
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
                    TempData["Message"] = "No existe la categoría indicada";
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
        public ActionResult Save(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    serviceCategoria.Save(categoria);
                    TempData["Action"] = "S";
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    TempData["Action"] = "E";
                    TempData["Message"] = "Error al procesar los datos! ";
                    TempData.Keep();
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
                @TempData["Action"] = "E";
                // Redireccion a la captura del Error
                return RedirectToAction("Index");
            }
        }



    }
}