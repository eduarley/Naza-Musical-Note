using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize((int)Roles.Lider, (int)Roles.Integrante)]
        public ActionResult Index()
        {
            return View();
        }


        [CustomAuthorize((int)Roles.Lider, (int)Roles.Integrante)]
        public ActionResult Contact()
        {
            return View();
        }
    }
}