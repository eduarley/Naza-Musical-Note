using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        IServiceUsuario serviceUsuario = new ServiceUsuario();
        string urlDomain = "http://localhost:63782/";
        private MyContext db = new MyContext();

        // GET: Usuario
        public ActionResult Index()
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                lista = serviceUsuario.ListaUsuarios();
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
            
            return View(lista);
        }

        // GET: Usuario/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = null;
            try
            {
                usuario = serviceUsuario.GetUsuarioByID(id);
                if (usuario == null)
                {
                    TempData["Message"] = "No existe el usuario";
                    @TempData["Action"] = "E";
                    TempData.Keep();
                    //return HttpNotFound();
                }else
                    return View(usuario);
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
            return RedirectToAction("Index");
            
        }

        // GET: Usuario/Create
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Lider)]
        public ActionResult Create()
        {
            try
            {
                ViewBag.Rol = db.Rol.ToList();
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
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Lider)]
        public ActionResult New(Usuario usuario)
        {
            try
            {
                usuario.Fecha_creacion = DateTime.Now;
                usuario.Clave = serviceUsuario.AsignarClaveNewUser();
                usuario.Primer_ingreso = true;
                if (ModelState.IsValid)
                {
                    if (serviceUsuario.Save(usuario) != null)
                    {
                        if (EnviarCorreo(usuario))
                        {
                            @TempData["Action"] = "S";
                            TempData.Keep();
                        }
                        else
                        {
                            @TempData["Action"] = "C";
                            TempData.Keep();
                        }
                    }
                    else
                    {
                        TempData["Action"] = "E";
                        TempData["Message"] = "Error al procesar los datos! ";
                        TempData.Keep();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create", usuario);
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

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Lider)]
        public ActionResult Update(Usuario usuario)
        {
            try
            {
                usuario.Fecha_creacion = DateTime.Now;
                if (ModelState.IsValid)
                {
                    if (serviceUsuario.Save(usuario) != null)
                    {
                        @TempData["Action"] = "S";
                        TempData.Keep();
                    }
                    else
                    {
                        TempData["Action"] = "E";
                        TempData["Message"] = "Error al procesar los datos! ";
                        TempData.Keep();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create");
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

        }





        // GET: Usuario/Edit/5
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Lider)]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = serviceUsuario.GetUsuarioByID(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            try
            {
                ViewBag.Rol = db.Rol.ToList();
                return View(usuario);
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



        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Lider)]
        public ActionResult Delete(string id)
        {
            try
            {
                Usuario oUser = (Usuario)Session["User"];
                Usuario usuario = serviceUsuario.GetUsuarioByID(id);
                if (oUser.Id != usuario.Id)
                {
                    usuario.Estado = false;
                    serviceUsuario.Save(usuario);

                    @TempData["Action"] = "T";
                    TempData.Keep();
                    return RedirectToAction("Index");
                }
                else
                {
                    @TempData["Action"] = "N";
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
            
        }

        public ActionResult ChangePassNewUser(string id)
        {
            Viewmodels.ViewModelPassNewUser model = new Viewmodels.ViewModelPassNewUser();
            model.IdUsuario = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult CambioClave(Viewmodels.ViewModelPassNewUser model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                Usuario oUser = serviceUsuario.GetUsuarioByID(model.IdUsuario);

                if (oUser != null)
                {
                    oUser.Clave = model.NewClave;
                    oUser.Primer_ingreso = false;
                    serviceUsuario.Save(oUser);
                    return View("ChangeComplet");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }

        public ActionResult ChangeComplet()
        {
            return View();
        }

        private bool EnviarCorreo(Usuario usuario)
        {

            try
            {
                string url = urlDomain + "Login/Index";
                MailMessage mmsg = new MailMessage();
                mmsg.To.Add(new MailAddress(usuario.Correo));

                mmsg.Subject = "Bienvenido a Naza Musical Note - Contraseña para primer ingreso";
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;
                string ClaveTemp = ApplicationCore.Utils.Cryptography.DecrypthAES(usuario.Clave);
                mmsg.Body = "<p>¡Hola " + usuario.Nombre + ", bienvenido a Naza Musical Note!</p><br><br><p>Su registro ha sido correcto. Se le asigó la contraseña: </p><p style='font-weight: bold'>" + ClaveTemp + "</p><br><br><p>Para iniciar sesión </p><a href='" + url + "'>presione aquí</a><br><br><p>Recuerde que cuando ingrese se le solicitará realizar un cambio de contraseña para brindar una mayor seguridad a su cuenta.</p><br><br><p>¡Gracias por usar nuestro sistema!</p>";
                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                mmsg.IsBodyHtml = true;
                mmsg.From = new MailAddress("nazamusicalnote@gmail.com");
                //mmsg.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential("nazamusicalnote@gmail.com", "132413242021");

                smtp.Port = 587;
                smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Send(mmsg);
                //email.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                serviceUsuario.NoGuardarUsuario(usuario.Id);
                TempData["Action"] = "C";
                TempData.Keep();
                return false;
                //RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}