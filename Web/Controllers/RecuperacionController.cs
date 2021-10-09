﻿using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class RecuperacionController : Controller
    {
        string urlDomain = "http://localhost:";
        // GET: Recuperacion
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send(Usuario usuario)
        {
            Usuario oUsuario = null;
            try
            {
                if (ModelState.IsValid)
                {
                    IServiceUsuario service = new ServiceUsuario();
                    string token = service.GetSha256(Guid.NewGuid().ToString());

                    oUsuario = service.UsuarioARecuperar(usuario.Id, usuario.Correo);

                    if (oUsuario != null)
                    {
                        oUsuario.Token_recovery = token;
                        service.Save(oUsuario);

                        var request = HttpContext.Request;
                        //string url = request.Url.Scheme + "://" + request.UserHostAddress + ":" +request.Url.Port+ "/Recuperacion/Recovery?token=" + token;
                        string url = urlDomain + request.Url.Port + "/Recuperacion/Recovery?token=" + token;
                        //string url = urlDomain+"Recuperacion/Recovery?token="+token;
                        System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();
                        mmsg.To.Add(new MailAddress(oUsuario.Correo));

                        mmsg.Subject = "Recuperación de contraseña - Naza Musical Note";
                        mmsg.SubjectEncoding = System.Text.Encoding.UTF8;
                        mmsg.Body = "<p>Saludos!</p><br><br><p>Adjuntamos su enlace de recuperación de contraseña:</p><br><br><a href='" + url + "'>Ingrese aquí para recuperar su contraseña</a><br><br><p>¡Gracias por usar nuestro sistema!</p><br><br><img src=\"~/Content/dist/img/logo Naza music note full primary.png\" width =\"40%\" height=\"10%\"/>";
                        mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                        mmsg.IsBodyHtml = true;
                        mmsg.From = new System.Net.Mail.MailAddress("nazamusicalnote@gmail.com");
                        //mmsg.Priority = MailPriority.Normal;

                        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                        smtp.Credentials = new System.Net.NetworkCredential("nazamusicalnote@gmail.com", "132413242021");

                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        //smtp.UseDefaultCredentials = false;
                        smtp.Host = "smtp.gmail.com";

                        smtp.Send(mmsg);
                        //email.Dispose();
                        TempData["Action"] = "S";


                        //Log.Info($"Accede {oUsuario.Nombre} {oUsuario.Apellidos} con el rol {oUsuario.Rol.IdRol}-{oUsuario.Rol.Descripcion}");

                    }
                    else
                    {
                        //Log.Warn($"{usuario.Login} se intentó conectar  y falló");
                        @TempData["Action"] = "N";
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
                //TempData["Message"] = ex.Message;
                //TempData.Keep();
                //return RedirectToAction("Default", "Error");

                @TempData["Action"] = "E";
                //@TempData["Mensaje"] = ex.Message + " " + ex.InnerException.ToString();
                @TempData["Mensaje"] = "Se ha producido un error al intentar enviar el correo de recuperación";
                TempData.Keep();
                return View("Index");
            }
        }

        public ActionResult Recovery(string token)
        {
            Viewmodels.ViewModelChangePass model = new Viewmodels.ViewModelChangePass();
            model.token = token;
            return View(model);
        }

        public ActionResult ChangeComplet()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecoveryPass(Viewmodels.ViewModelChangePass model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                IServiceUsuario service = new ServiceUsuario();
                Usuario oUser = service.ConsultarPorToken(model.token);

                if (oUser != null)
                {
                    oUser.Clave = model.NewClave;
                    service.Save(oUser);
                    return View("ChangeComplet");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }
    }
}