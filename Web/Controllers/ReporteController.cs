using ApplicationCore.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{

    [CustomAuthorize((int)Roles.Lider)]
    public class ReporteController : Controller
    {

        IServiceReporte serviceReporte = new ServiceReporte();
        IServiceBitacora_Sesion serviceBitacora = new ServiceBitacora_Sesion();
        // GET: Reporte
        public ActionResult ReporteSesion()
        {
            return View();
        }

        public ActionResult DescargarReporteSesion()
        {


            try
            {
                Usuario usuario = (Usuario)Session["User"];
                IServiceReporte serviceReporte = new ServiceReporte();
                var lista = serviceBitacora.GetBitacorasSesion();
                var rptH = new ReportClass();
                rptH.FileName = Server.MapPath("/Reports/ReporteAccesos.rpt");
                rptH.Load();
                rptH.SetDataSource(lista);
                rptH.SetParameterValue("NombreUsuario", usuario.NombreCompleto);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                //En PDF
                Stream stream = rptH.ExportToStream(ExportFormatType.PortableDocFormat);
                rptH.Dispose();
                rptH.Close();
                return new FileStreamResult(stream, "application/pdf");


                //En Excel
                //Stream stream = rptH.ExportToStream(ExportFormatType.Excel);
                //rptH.Seek(0, SeekOrigin.Begin);
                //return File(stream, "application/vnd.ms-excel", "reporte.xls");

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

        [HttpPost]
        public JsonResult DeleteBitacorasSesion()
        {
            var status = false;
            try
            {
                
                if (serviceBitacora.DeleteBitacorasSesion())
                {
                    status = true;
                    TempData["Action"] = "D";
                    TempData.Keep();
                }
                else
                {
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




        //REPORTE USUARIOS



        public ActionResult ReporteUsuariosIngresados()
        {
            return View();
        }


        [HttpPost]
        public ActionResult MostrarReporteUsuariosIngresados(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                Usuario usuario = (Usuario)Session["User"];
                IServiceReporte serviceReporte = new ServiceReporte();
                var lista = serviceReporte.GetUsuariosIngresadosByFechas(fechaInicio, fechaFin);
                var rptH = new ReportClass();
                rptH.FileName = Server.MapPath("/Reports/ReporteUsuariosIngresados.rpt");
                rptH.Load();
                rptH.SetDataSource(lista);
                rptH.SetParameterValue("NombreUsuario", usuario.NombreCompleto);
                rptH.SetParameterValue("fechaInicio", fechaInicio);
                rptH.SetParameterValue("fechaFin", fechaFin);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                //En PDF
                Stream stream = rptH.ExportToStream(ExportFormatType.PortableDocFormat);
                rptH.Dispose();
                rptH.Close();
                return new FileStreamResult(stream, "application/pdf");
                //return PartialView("_DetalleReporteUsuarios");
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







        //REPORTE SERVICIOS





        public ActionResult ReporteServicios()
        {
            try
            {
                IServiceUsuario serviceUsuario = new ServiceUsuario();
                ViewBag.Usuarios = serviceUsuario.GetUsuarios();
                return View();
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

        [HttpPost]
        public ActionResult MostrarReporteServicios(string idUsuario)
        {
            try
            {
                IServiceUsuario serviceUsuario = new ServiceUsuario();
                Usuario usuario = (Usuario)Session["User"];
                Usuario usuarioConsultar = serviceUsuario.GetUsuarioByID(idUsuario);
                IServiceReporte serviceReporte = new ServiceReporte();
                var lista = serviceReporte.GetServiciosByUsuario(idUsuario);
                var rptH = new ReportClass();
                rptH.FileName = Server.MapPath("/Reports/ReporteServicios.rpt");
                rptH.Load();

                List<Viewmodels.ViewModelReporteServicio> listaViewModel = new List<Viewmodels.ViewModelReporteServicio>();


                foreach (var item in lista)
                {
                    listaViewModel.Add(new Viewmodels.ViewModelReporteServicio()
                    {
                        Fecha = item.RolServicio.FechaInicio,
                        Puesto = item.Puesto.Descripcion,
                        Titulo = item.RolServicio.Titulo
                    }); 
                }



                rptH.SetDataSource(listaViewModel);
                rptH.SetParameterValue("NombreUsuario", usuario.NombreCompleto);
                rptH.SetParameterValue("UsuarioAConsultar", usuarioConsultar.NombreCompleto);
                rptH.SetParameterValue("CedulaUsuarioAConsultar", usuarioConsultar.Id);
                rptH.SetParameterValue("Apariciones", listaViewModel.Count());
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                //En PDF
                Stream stream = rptH.ExportToStream(ExportFormatType.PortableDocFormat);
                rptH.Dispose();
                rptH.Close();
                return new FileStreamResult(stream, "application/pdf");
                //return PartialView("_DetalleReporteUsuarios");
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

    }
}