using ApplicationCore.Services;
using Infraestructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class RolServicioController : Controller
    {
        // GET: RolServicio
        [CustomAuthorize((int)Roles.Lider, (int)Roles.Integrante)]
        public ActionResult Index()
        {
            IServiceRolServicio serviceRolServicio = new ServiceRolServicio();
            try
            {
                return View(serviceRolServicio.GetRolServicios());
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
            ViewBag.Usuarios = null;
            ViewBag.Canciones = null;
            ViewBag.Puestos = null;
            ViewBag.CategoriasPuestos = null;
            List<Usuario> participantes = null;
            List<Cancion> canciones = null;
            List<Puesto> puestos = null;
            List<Categoria> categorias = null;
            IServiceUsuario serviceUsuario = new ServiceUsuario();
            IServiceCancion serviceCancion = new ServiceCancion();
            IServicePuesto servicePuesto = new ServicePuesto();
            IServiceCategoria serviceCategoria = new ServiceCategoria();
            try
            {
                participantes = serviceUsuario.GetIntegrantesActivos();
                participantes.Insert(0, new Usuario() { Id = null, Nombre = "Sin Asignar" });
                canciones = serviceCancion.GetCancionesActivas();
                puestos = servicePuesto.GetPuestosActivos();
                categorias = serviceCategoria.GetCategoriasActivas();
                var usuarioQuery = participantes.Select(p => new { p.Id, DisplayText = p.NombreCompleto });
                ViewBag.Usuarios = usuarioQuery;
                if (canciones.Count > 0)
                    ViewBag.Canciones = canciones;
                if (puestos.Count > 0)
                    ViewBag.Puestos = puestos;
                if (categorias.Count > 0)
                    ViewBag.CategoriasPuestos = categorias;


            }
            catch (Exception ex)
            {

                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                ViewBag.Message = TempData["Message"];
                @TempData["Action"] = "E";
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Index");
            }


            return View();
        }



        [CustomAuthorize((int)Roles.Lider, (int)Roles.Integrante)]
        public ActionResult Details(int id)
        {
            IServiceRolServicio serviceRolServicio = new ServiceRolServicio();
            RolServicio rs = null;
            try
            {
                rs = serviceRolServicio.GetRolServicioPorID(id);
                if (rs == null)
                {
                    TempData["Message"] = "No existe el rol de servicio! ";
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
                ViewBag.Message = TempData["Message"];
                @TempData["Action"] = "E";
                TempData.Keep();
                return RedirectToAction("Index");
            }
            return View(rs);
        }

        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Edit(int id)
        {
            RolServicio rs = null;
            ViewBag.Usuarios = null;
            ViewBag.Canciones = null;
            ViewBag.Puestos = null;
            ViewBag.CategoriasPuestos = null;
            List<Usuario> participantes = null;
            List<Cancion> canciones = null;
            List<Puesto> puestos = null;
            List<Categoria> categorias = null;
            IServiceUsuario serviceUsuario = new ServiceUsuario();
            IServiceCancion serviceCancion = new ServiceCancion();
            IServicePuesto servicePuesto = new ServicePuesto();
            IServiceCategoria serviceCategoria = new ServiceCategoria();
            IServiceRolServicio serviceRolServicio = new ServiceRolServicio();
            try
            {
                participantes = serviceUsuario.GetIntegrantesActivos();
                participantes.Insert(0, new Usuario() { Id = null, Nombre = "Sin Asignar" });
                canciones = serviceCancion.GetCancionesActivas();
                puestos = servicePuesto.GetPuestosActivos();
                categorias = serviceCategoria.GetCategoriasActivas();
                var usuarioQuery = participantes.Select(p => new { p.Id, DisplayText = p.NombreCompleto });
                ViewBag.Usuarios = usuarioQuery;
                if (canciones.Count > 0)
                    ViewBag.Canciones = canciones;
                if (puestos.Count > 0)
                    ViewBag.Puestos = puestos;
                if (categorias.Count > 0)
                    ViewBag.CategoriasPuestos = categorias;

                rs = serviceRolServicio.GetRolServicioPorID(id);
                if (rs != null)
                {
                    RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "No existe el rol de servicio! ";
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
                ViewBag.Message = TempData["Message"];
                @TempData["Action"] = "E";
                TempData.Keep();
                return RedirectToAction("Index");
            }
            return View(rs);
        }


        public ActionResult CargarRolServicio(int id)
        {
            RolServicio rs = new RolServicio();
            IServiceRolServicio serviceRolServicio = new ServiceRolServicio();
            // Configuración del Serializador  
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Error = (sender, args) =>
                {
                    args.ErrorContext.Handled = true;
                },
            };
            try
            {
                rs = serviceRolServicio.GetRolServicioPorID(id);
            }
            catch (Exception ex)
            {

                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                ViewBag.Message = TempData["Message"];
                @TempData["Action"] = "E";
            }
            string json = JsonConvert.SerializeObject(rs, settings);
            return Content(json);
        }


        [HttpPost]
        [CustomAuthorize((int)Roles.Lider)]
        public JsonResult Delete(int eventID)
        {
            IServiceCalendario serviceCalendario = new ServiceCalendario();
            var status = false;
            try
            {
                if (serviceCalendario.DeleteEvent(eventID))
                {
                    status = true;
                    TempData["Action"] = "D";
                    TempData.Keep();

                }
                else
                {
                    TempData["Message"] = "Error al procesar los datos! ";
                    @TempData["Action"] = "E";
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
        [CustomAuthorize((int)Roles.Lider)]
        public JsonResult Save(
              RolServicio rs,
              int[] cancion,
              DateTime HoraInicio,
              DateTime HoraFin,
              List<int> IdPuestos,
              List<string> IdUsuarios)
        {
            var status = false;
            try
            {
                IServiceCancion serviceCancion = new ServiceCancion();
                IServiceCalendario serviceCalendario = new ServiceCalendario();
                Usuario usuario = null;
                if (Session["User"] != null)
                {
                    usuario = Session["User"] as Usuario;
                    rs.IdUsuario_Propietario = usuario.Id;
                }
                rs.Fecha_creacion = DateTime.Now;
                rs.Color = "green";
                rs.Estado = true;
                rs.IsFullDay = false;
                DateTime start = new DateTime(rs.FechaInicio.Year, rs.FechaInicio.Month, rs.FechaInicio.Day, HoraInicio.Hour, HoraInicio.Minute, 0);
                DateTime end = new DateTime(rs.FechaInicio.Year, rs.FechaInicio.Month, rs.FechaInicio.Day, HoraFin.Hour, HoraFin.Minute, 0);
                rs.FechaInicio = start;
                rs.FechaFin = end;
                rs.Cancion = serviceCancion.GetListaCancionesByID(cancion);
                List<Usuario_RolServicio> PuestosAsignados = serviceCalendario.Generar_Lista_Usuario_RolServicio(IdPuestos, IdUsuarios);


                if (serviceCalendario.SaveEvent(rs, PuestosAsignados) != null)
                {
                    status = true;
                    TempData["Action"] = "S";
                    TempData.Keep();
                }
                else
                {
                    TempData["Message"] = "Error al procesar los datos! ";
                    @TempData["Action"] = "E";
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