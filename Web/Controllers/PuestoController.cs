﻿using System;
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
        private static String Action;
        private static String Message;

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
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }

        [CustomAuthorize((int)Roles.Lider)]
        // GET: Puesto/Details/5
        public ActionResult Details(int id)
        {
            Puesto puesto = servicePuesto.GetPuestoById(id);
            return View(puesto);
        }

        
        // GET: Puesto/Create
        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Create()
        {
            ViewBag.Categoria = serviceCategoria.GetCategoriasActivas();
            return View();
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

            Action = "S";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    
                    servicePuesto.Save(puesto);
                    TempData["Action"] = Action;
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos!";
                    TempData.Keep();

                    return View("Create", puesto);
                }

                // redirigir
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! ";
                TempData["MessageError"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Index", "Puesto");
            }
        }

        // GET: Puesto/Edit/5
        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Edit(int? id)
        {
            List<Categoria> categorias = null;
            categorias = serviceCategoria.GetCategoriasActivas();
            //ViewBag.IdCategoria = serviceCategoria.LlenarCombo().ToList();
            ViewBag.Categoria = categorias;
            Puesto puesto = servicePuesto.GetPuestoById(id.Value);
            //ViewBag.ElId = (puesto.IdCategoria);
            
            //ViewBag.IdCategoria = new SelectList(db.Categoria, "Id", "Descripcion");
            return View(puesto);
        }

        // POST: Puesto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Edit(Puesto puesto)
        {
            Action = "U";
            try
            {
                //Puesto puesto = servicePuesto.GetPuestoById(id);
                // Response.StatusCode = 500;

                if (ModelState.IsValid)
                {
                    servicePuesto.Save(puesto);
                    TempData["Action"] = Action;
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
                TempData["Message"] = "Error al procesar los datos!";
                TempData["MessageError"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Index", "Puesto");
            }
        }

        [CustomAuthorize((int)Roles.Lider)]
        public JsonResult Delete(int id)
        {
            var status = false;
            Action = "D";
            try
            {
                Puesto puesto = servicePuesto.GetPuestoById(id);
                servicePuesto.BorrarPuesto(id);

                @TempData["Action"] = Action;
                TempData.Keep();
                status = true;
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!";
                TempData["MessageError"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}
