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
    public class LoginController : Controller
    {
        string urlDomain = "http://localhost:";
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Viewmodels.ViewModelLogin model)
        {
            Usuario oUsuario = null;
            Rol oRol = null;
            try
            {
                if (ModelState.IsValid)
                {
                    IServiceUsuario service = new ServiceUsuario();
                    IServiceBitacora_Sesion serviceBitacora = new ServiceBitacora_Sesion();
                    oUsuario = service.GetUsuario(model.Id, model.Clave);

                    if (oUsuario != null)
                    {
                        oRol = service.GetRolByUsuario(oUsuario.IdRol);
                    }

                    if (oUsuario != null)
                    {
                        if (oUsuario.Estado)
                        {
                            oRol.Usuario = null;
                            oUsuario.Rol = oRol;
                            serviceBitacora.Save(oUsuario);
                            
                            Log.Info($"Accede {oUsuario.NombreCompleto} con el rol {oUsuario.Rol.Id}-{oUsuario.Rol.Descripcion}");
                            if (oUsuario.Primer_ingreso)
                            {
                                var request = HttpContext.Request;
                                //string url = request.Url.Scheme + "://" + request.UserHostAddress + ":" +request.Url.Port+ "/Recuperacion/Recovery?token=" + token;
                                //string url = urlDomain + request.Url.Port + "/" + "Login/Index";
                                string IdTemp = oUsuario.Id;
                                string urlParaRedirigir = urlDomain + request.Url.Port + "/" + "Usuario/ChangePassNewUser?id=" + IdTemp;
                                //return RedirectToAction("ChangePassNewUser", "Usuario", new { id= IdTemp });
                                //return RedirectToRoute(urlParaRedirigir);
                                return Redirect(urlParaRedirigir);
                            }
                            else
                            {
                                Session["User"] = oUsuario;
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {

                            @TempData["Action"] = "E";
                            TempData.Keep();
                        }
                    }
                    else
                    {
                        //Log.Warn($"{usuario.Login} se intentó conectar  y falló");
                        @TempData["Action"] = "I";
                        TempData.Keep();
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                //Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult UnAuthorized()
        {
            try
            {
                ViewBag.Message = "Página no autorizada!";

                if (Session["User"] != null)
                {
                    Usuario oUsuario = Session["User"] as Usuario;
                    Log.Warn($"El usuario {oUsuario.Nombre} {oUsuario.Apellido_materno} {oUsuario.Apellido_paterno} con el rol {oUsuario.Rol.Id}-{oUsuario.Rol.Descripcion}, intentó acceder una página sin derechos  ");
                }

                return View();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }


        public ActionResult Logout()
        {
            try
            {
                //Log.Info("Se desconectó ");
                Session["User"] = null;
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                //Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }
    }
}