using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        string urlDomain = "http://localhost:63782/";
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
                    oUsuario = service.GetUsuario(model.Id, model.Clave);

                    if (oUsuario != null)
                    {
                        oRol = service.RolDelUsuario(oUsuario.IdRol);
                    }

                    if (oUsuario != null)
                    {
                        if (oUsuario.Estado)
                        {
                            oRol.Usuario = null;
                            oUsuario.Rol = oRol;
                            //Log.Info($"Accede {oUsuario.Nombre} {oUsuario.Apellidos} con el rol {oUsuario.Rol.IdRol}-{oUsuario.Rol.Descripcion}");
                            if (oUsuario.Primer_ingreso)
                            {
                                string IdTemp = oUsuario.Id;
                                string urlParaRedirigir = urlDomain + "Usuario/ChangePassNewUser?id=" + IdTemp;
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

                return View("Index");
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
                ViewBag.Message = "Un Authorized Page!";

                if (Session["User"] != null)
                {
                    Usuario oUsuario = Session["User"] as Usuario;
                    //Log.Warn($"El usuario {oUsuario.Nombre} {oUsuario.Apellidos} con el rol {oUsuario.Rol.IdRol}-{oUsuario.Rol.Descripcion}, intentó acceder una página sin derechos  ");
                }

                return View();
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