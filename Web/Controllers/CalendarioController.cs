using ApplicationCore.Services;
using Infraestructure.Models;
using Newtonsoft.Json;
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
    public class CalendarioController : Controller
    {

        public ActionResult Index()
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
                participantes = serviceUsuario.GetUsuariosActivos();
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
                TempData.Keep();
                ViewBag.Message = TempData["Message"];
                @TempData["Action"] = "E";
            }


            return View();
        }


        public ActionResult GetEvents()
        {
            List<RolServicio> lista = new List<RolServicio>();
            IServiceCalendario serviceCalendario = new ServiceCalendario();

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
                lista = serviceCalendario.GetEvents();
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
            string json = JsonConvert.SerializeObject(lista, settings);
            return Content(json);

        }



        
        [HttpPost]
        [CustomAuthorize((int)Roles.Lider)]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            IServiceCalendario serviceCalendario = new ServiceCalendario();
            try
            {
                if (serviceCalendario.DeleteEvent(eventID))
                    status = true;
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
                //return RedirectToAction("List");
            }
            return new JsonResult { Data = new { status = status } };
        }


        public int GetPrimeraCategoria()
        {
            int id = -1;
            IServiceCalendario serviceCalendario = new ServiceCalendario();
            try
            {
                id = serviceCalendario.GetPrimerIDCategoria();
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
                //return RedirectToAction("List");
            }
            return id;
        }

        public ActionResult GetPuestosPorCategoria(int id)
        {
            List<Puesto> lista = new List<Puesto>();
            IServiceCalendario serviceCalendario = new ServiceCalendario();
            try
            {
                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Error = (sender, args) =>
                    {
                        args.ErrorContext.Handled = true;
                    },
                };
                lista = serviceCalendario.GetPuestosPorCategoria(id);
                string json = JsonConvert.SerializeObject(lista, settings);
                return Content(json);
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
                //return RedirectToAction("List");
            }
            return null;
        }





        [HttpPost]
        [CustomAuthorize((int)Roles.Lider)]
        public JsonResult SaveEvent(
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
                    status = true;

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
                //return RedirectToAction("List");
            }

            return new JsonResult { Data = new { status = status } };
        }





    }
}