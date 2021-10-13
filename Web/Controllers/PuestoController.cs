using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ApplicationCore.Services;
using Infraestructure.Models;
using Web.Security;

namespace Web.Controllers
{
    public class PuestoController : Controller
    {
        IServiceCategoria serviceCategoria = new ServiceCategoria();
        IServicePuesto servicePuesto = new ServicePuesto();



        // GET: Puesto
        [CustomAuthorize((int)Roles.Lider, (int)Roles.Integrante)]
        public ActionResult Index()
        {
            try
            {
                List<Puesto> listaPuestos = servicePuesto.GetPuestos();
                return View(listaPuestos);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                @TempData["Message"] = ex.Message;
                @TempData["Action"] = "E";
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }

        [CustomAuthorize((int)Roles.Lider)]
        // GET: Puesto/Details/5
        public ActionResult Details(int id)
        {
            Puesto puesto = null;
            try
            {
                puesto = servicePuesto.GetPuestoById(id);
                if (puesto == null)
                {
                    TempData["Message"] = "No existe el puesto indicado";
                    @TempData["Action"] = "E";
                    TempData.Keep();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                @TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
            return View(puesto);
        }

        
        // GET: Puesto/Create
        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Create()
        {
            try
            {
                ViewBag.Categoria = serviceCategoria.GetCategoriasActivas();
                return View();
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

        // POST: Puesto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Save(Puesto puesto)
        {
            /*
            if (puesto.Estado == true && puesto.Categoria.Estado == false)
            {
                TempData["Message"] = "No puedes activar este puesto porque su categoría está desactivada.";
                @TempData["Action"] = "E";
                TempData.Keep();
            }
            */


            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    
                    servicePuesto.Save(puesto);
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

                // redirigir
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

        // GET: Puesto/Edit/5
        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Edit(int? id)
        {
            List<Categoria> categorias = null;
            try
            {
                categorias = serviceCategoria.GetCategoriasActivas();
                ViewBag.Categoria = categorias;
                Puesto puesto = servicePuesto.GetPuestoById(id.Value);
                if(puesto != null)
                    return View(puesto);
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    TempData["Action"] = "E";
                    TempData["Message"] = "No existe el puesto indicado";
                    TempData.Keep();
                    return RedirectToAction("Index");
                }
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

        // POST: Puesto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Edit(Puesto puesto)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    servicePuesto.Save(puesto);
                    TempData["Action"] = "U";
                    return RedirectToAction("Index");
                }
                ViewBag.IdCategoria = serviceCategoria.GetCategorias();
                //ViewBag.IdCategoria = new SelectList(db.Categoria, "Id", "Descripcion");
                return View(puesto);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Index", "Puesto");
            }
        }

        [CustomAuthorize((int)Roles.Lider)]
        public JsonResult Delete(int id)
        {
            var status = false;
            try
            {

                Puesto puesto = servicePuesto.GetPuestoById(id);
                if (servicePuesto.DeletePuesto(id))
                {
                    @TempData["Action"] = "D";
                    TempData.Keep();
                    status = true;
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    TempData["Action"] = "E";
                    TempData["Message"] = "No existe el puesto indicado";
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
