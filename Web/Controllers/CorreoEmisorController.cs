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
    [CustomAuthorize((int)Roles.Lider)]
    public class CorreoEmisorController : Controller
    {
        //private MyContext db = new MyContext();

        // GET: CorreoEmisor
        public ActionResult Index()
        {
            IServiceCorreo serviceCorreo = new ServiceCorreo();
            CorreoEmisor correo = serviceCorreo.GetCorreo();
            //return View(db.CorreoEmisor.ToList());
            return View(correo);
        }

        // GET: CorreoEmisor/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //CorreoEmisor correoEmisor = db.CorreoEmisor.Find(id);
                IServiceCorreo service = new ServiceCorreo();
                CorreoEmisor correoEmisor = service.GetCorreo();
                return View(correoEmisor);
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

        // POST: CorreoEmisor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Correo,Clave,Estado")] CorreoEmisor correoEmisor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //db.Entry(correoEmisor).State = EntityState.Modified;
                    //db.SaveChanges();
                    IServiceCorreo serviceCorreo = new ServiceCorreo();
                    serviceCorreo.Edit(correoEmisor);
                    TempData["Action"] = "U";
                    TempData.Keep();
                    return RedirectToAction("Index");
                }
                return View(correoEmisor);
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
    }
}
