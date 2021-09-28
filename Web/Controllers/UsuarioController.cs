using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

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
            var usuario = serviceUsuario.ListaUsuarios();
            return View(usuario.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = serviceUsuario.GetUsuarioByID(id);
            //byte[] Cadena = usuario.Foto;
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.IdRol = new SelectList(db.Rol, "Id", "Descripcion");
            return View();
        }

        // POST: Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdRol,Nombre,Apellido_paterno,Apellido_materno,Clave,Telefono,Correo,Foto,Token_recovery,Fecha_creacion,Primer_ingreso,Estado")] Usuario usuario, HttpPostedFileBase ImageFile)
        {
            try
            {
                MemoryStream target = new MemoryStream();

                // Cuando es Insert Image viene en null porque se pasa diferente
                if (usuario.Foto == null)
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        usuario.Foto = target.ToArray();
                        ModelState.Remove("Foto");
                    }
                }

                if (ModelState.IsValid)
                {
                    usuario.Fecha_creacion = DateTime.Now;
                    usuario.Clave = serviceUsuario.AsignarClaveNewUser();
                    usuario.Primer_ingreso = true;
                    serviceUsuario.Save(usuario);
                    EnviarCorreo(usuario);

                    @TempData["Action"] = "S";
                    TempData.Keep();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Create");
                }
            }
            catch (Exception ex)
            {
                @TempData["Action"] = "E";
                TempData.Keep();
                return View("Index");
            }

        }

        // GET: Usuario/Edit/5
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
            ViewBag.IdRol = new SelectList(db.Rol, "Id", "Descripcion", usuario.IdRol);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario, HttpPostedFileBase ImageFile)
        {
            try
            {
                MemoryStream target = new MemoryStream();

                // Cuando es Insert Image viene en null porque se pasa diferente
                if (usuario.Foto == null)
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        usuario.Foto = target.ToArray();
                        ModelState.Remove("Foto");
                    }
                }
                else
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        if (usuario.Foto != target.ToArray())
                        {
                            usuario.Foto = target.ToArray();
                            ModelState.Remove("Foto");
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    Usuario usuarioTemp = serviceUsuario.GetUsuarioByID(usuario.Id);
                    usuario.Fecha_creacion = usuarioTemp.Fecha_creacion;

                    if (usuario.IdRol != usuarioTemp.IdRol)
                    {
                        usuarioTemp.IdRol = usuario.IdRol;
                    }

                    if (usuario.Nombre != usuarioTemp.Nombre)
                    {
                        usuarioTemp.Nombre = usuario.Nombre;
                    }

                    if (usuario.Apellido_paterno != usuarioTemp.Apellido_paterno)
                    {
                        usuarioTemp.Apellido_paterno = usuario.Apellido_paterno;
                    }

                    if (usuario.Apellido_materno != usuarioTemp.Apellido_materno)
                    {
                        usuarioTemp.Apellido_materno = usuario.Apellido_materno;
                    }

                    if (usuario.Telefono != usuarioTemp.Telefono)
                    {
                        usuarioTemp.Telefono = usuario.Telefono;
                    }

                    if (usuario.Correo != usuarioTemp.Correo)
                    {
                        usuarioTemp.Correo = usuario.Correo;
                    }

                    if (usuario.Foto != usuarioTemp.Foto)
                    {
                        usuarioTemp.Foto = usuario.Foto;
                    }

                    if (usuario.Estado != usuarioTemp.Estado)
                    {
                        usuarioTemp.Estado = usuario.Estado;
                    }

                    serviceUsuario.Save(usuarioTemp);

                    @TempData["Action"] = "V";
                    TempData.Keep();


                    return RedirectToAction("Index");
                }
                ViewBag.IdRol = new SelectList(db.Rol, "Id", "Descripcion", usuario.IdRol);
                //using (MyContext ctx = new MyContext())
                //{
                //    ViewBag.ListaRol = (IEnumerable<Rol>)ctx.Rol.ToList<Rol>();
                //}

                @TempData["Action"] = "X";
                TempData.Keep();
                return View("Index");
            }
            catch (Exception ex)
            {
                @TempData["Action"] = "X";
                TempData.Keep();
                return View("Index");
            }
            //return RedirectToAction("Edit");
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(string id)
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
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
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

        private void EnviarCorreo(Usuario usuario)
        {
            string url = urlDomain + "Login/Index";
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();
            mmsg.To.Add(new MailAddress(usuario.Correo));

            mmsg.Subject = "Bienvenido a Naza Musical Note - Contraseña para primer ingreso";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;
            string ClaveTemp = ApplicationCore.Utils.Cryptography.DecrypthAES(usuario.Clave);
            mmsg.Body = "<p>¡Hola " + usuario.Nombre + ", bienvenido a Naza Musical Note!</p><br><br><p>Su registro ha sido correcto. Se le asigó la contraseña: </p><p style='font-weight: bold'>" + ClaveTemp + "</p><br><br><p>Para iniciar sesión </p><a href='" + url + "'>presione aquí</a><br><br><p>Recuerde que cuando ingrese se le solicitará realizar un cambio de contraseña para brindar una mayor seguridad a su cuenta.</p><br><br><p>¡Gracias por usar nuestro sistema!</p>";
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

            try
            {
                smtp.Send(mmsg);
                //email.Dispose();
            }
            catch (Exception ex)
            {
                serviceUsuario.NoGuardarUsuario(usuario.Id);
                TempData["Action"] = "C";
                TempData.Keep();
                RedirectToAction("Index");
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