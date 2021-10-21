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
                var lista = serviceReporte.GetBitacorasSesion();
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
        public JsonResult BorrarBitacorasSesion()
        {
            var status = false;
            try
            {
                
                if (serviceReporte.DeleteBitacorasSesion())
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

    }
}