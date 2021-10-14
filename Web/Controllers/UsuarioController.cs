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
        IServiceRol serviceRol = new ServiceRol();
        string urlDomain = "http://localhost:";

        // GET: Usuario
        [CustomAuthorize((int)Roles.Lider, (int)Roles.Integrante)]
        public ActionResult Index()
        {
            List<Usuario> lista = null;
            try
            {
                lista = serviceUsuario.GetUsuarios();
                return View(lista);
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
                    return RedirectToAction("Index");
                }
                else
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
        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Create()
        {
            try
            {
                //Para que el toggle del estado se ponga en activo por defecto
                Usuario usuario = new Usuario()
                {
                    Estado = true
                };
                ViewBag.Rol = serviceRol.GetRoles();
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Lider)]
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
                            @TempData["Action"] = "E";
                            TempData["Message"] = "Hubo un error al enviar el correo electrónico de primer ingreso al Usuario creado, por ende, el Usuario no se guardará en la base de datos del sistema";
                            TempData.Keep();
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["Action"] = "E";
                        TempData["Message"] = "Error al procesar los datos! ";
                        TempData.Keep();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    //MOSTRAR TEMPDATA EN LA PAGINA CREAR
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
            }
            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Update(Usuario usuario)
        {
            try
            {
                usuario.Fecha_creacion = DateTime.Now;
                if (ModelState.IsValid)
                {
                    if (serviceUsuario.Save(usuario) != null)
                    {
                        @TempData["Action"] = "U";
                        TempData.Keep();
                    }
                    else
                    {
                        TempData["Action"] = "E";
                        TempData["Message"] = "Error al procesar los datos! ";
                        TempData.Keep();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["Action"] = "E";
                    TempData["Message"] = "Error al procesar los datos! ";
                    TempData.Keep();
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
            }
            return RedirectToAction("Index");
        }





        // GET: Usuario/Edit/5
        [CustomAuthorize((int)Roles.Lider)]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                Usuario usuario = serviceUsuario.GetUsuarioByID(id);
                if (usuario != null)
                {
                    ViewBag.Rol = serviceRol.GetRoles();
                    return View(usuario);
                }
                else
                {
                    TempData["Action"] = "E";
                    TempData["Message"] = "Error al procesar los datos! ";
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



        [CustomAuthorize((int)Roles.Lider)]
        public JsonResult Delete(string id)
        {
            var status = false;
            try
            {
                Usuario oUser = (Usuario)Session["User"];
                Usuario usuario = serviceUsuario.GetUsuarioByID(id);
                if (oUser.Id != usuario.Id)
                {
                    usuario.Estado = false;
                    serviceUsuario.Save(usuario);
                    @TempData["Action"] = "D";
                    TempData.Keep();
                    status = true;
                }
                else
                {
                    @TempData["Action"] = "E";
                    TempData["Message"] = "No puedes desactivar tu propio usuario!";
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

        public ActionResult ChangePassNewUser(string id)
        {

            Viewmodels.ViewModelPassNewUser model = new Viewmodels.ViewModelPassNewUser();
            model.IdUsuario = id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambioClave(Viewmodels.ViewModelPassNewUser model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario oUser = serviceUsuario.GetUsuarioByID(model.IdUsuario);

                    if (oUser != null)
                    {
                        oUser.Clave = model.NewClave;
                        //oUser.Primer_ingreso = false;
                        serviceUsuario.SaveClavePrimerIngreso(oUser);
                        return View("ChangeComplet");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Default", "Error");
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
                var request = HttpContext.Request;
                //string url = request.Url.Scheme + "://" + request.UserHostAddress + ":" +request.Url.Port+ "/Recuperacion/Recovery?token=" + token;
                string url = urlDomain + request.Url.Port + "/" + "Login/Index";
                //string url = urlDomain + "Login/Index";
                MailMessage mmsg = new MailMessage();
                mmsg.To.Add(new MailAddress(usuario.Correo));

                mmsg.Subject = "Bienvenido a Naza Musical Note - Contraseña para primer ingreso";
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;
                string ClaveTemp = ApplicationCore.Utils.Cryptography.DecrypthAES(usuario.Clave);
                mmsg.Body = "<p>¡Hola " + usuario.Nombre + ", bienvenid@ a Naza Musical Note!</p><br><br><p>Su registro ha sido correcto. Se le asigó la contraseña: </p><p style='font-weight: bold'>" + ClaveTemp + "</p><br><br><p>Para iniciar sesión </p><a href='" + url + "'>presione aquí</a><br><br><p>Recuerde que cuando ingrese se le solicitará realizar un cambio de contraseña para brindar una mayor seguridad a su cuenta.</p><br><br><p>¡Gracias por usar nuestro sistema!</p><br><br><img src=\"https://www.whdl.org/sites/default/files/ES_logotipo.jpg\" width =\"40%\" height=\"10%\"/>";
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
                TempData["Action"] = "E";
                TempData["Message"] = "Hubo un error al enviar el correo electrónico de primer ingreso al Usuario creado, por ende, el Usuario no se guardará en la base de datos del sistema";
                TempData.Keep();
                return false;
            }
        }


        public ActionResult UserProfile(string id)
        {
            try
            {

                Usuario usuario = (Usuario)Session["User"];

                if (usuario != null)
                {
                    return View(usuario);
                }
                else
                {
                    @TempData["Action"] = "E";
                    TempData["Message"] = "Error al procesar los datos!";
                    TempData.Keep();
                    return RedirectToAction("default", "Error");
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
                return RedirectToAction("Default", "Error");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUserProfile(Usuario usuario, HttpPostedFileBase ImageFile)
        {
            string errores = "";
            MemoryStream target = new MemoryStream();
            try
            {
                //usuario.Clave = serviceUsuario.GetSha256(usuario.Clave);

                usuario.Rol = serviceRol.GetRolById(usuario.IdRol);
                byte[] foto = null;


                Usuario usuarioTemp = serviceUsuario.GetUsuarioByID(usuario.Id);
                if (usuarioTemp != null)
                {
                    if(usuarioTemp.Foto != null)
                    {
                        foto = usuarioTemp.Foto;
                        if(foto != usuario.Foto)
                        {
                            if (ImageFile != null)
                            {
                                ImageFile.InputStream.CopyTo(target);
                                usuario.Foto = target.ToArray();
                                ModelState.Remove("Foto");
                            }
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    if (serviceUsuario.Save(usuario) != null)
                    {
                        @TempData["Action"] = "U";
                        Session["User"] = usuario;
                        return RedirectToAction("UserProfile");
                    }
                    else
                    {
                        @TempData["Action"] = "E";
                        TempData["Message"] = "Error editar.";
                        TempData.Keep();
                        return RedirectToAction("UserProfile");
                    }

                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);
                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();
                    return RedirectToAction("UserProfile");
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
                return RedirectToAction("Default", "Error");
            }
        }



        [HttpPost]
        public JsonResult ChangePasswordProfile(string id, string pass, string confirmPass)
        {
            var status = false;
            try
            {
                if(pass == confirmPass)
                {
                    Usuario usuario = serviceUsuario.GetUsuarioByID(id);
                    if(usuario != null)
                    {
                        usuario.Clave = serviceUsuario.GetEncryptedPass(pass);
                        if (serviceUsuario.Save(usuario) != null)
                        {
                            @TempData["Action"] = "P";
                            status = true;
                        }
                    }
                }
                else
                {
                    status = false;
                    @TempData["Action"] = "E";
                    TempData["Message"] = "La clave no es la misma a la confirmación";
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