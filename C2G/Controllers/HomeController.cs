using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C2G.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.CurrentUser = Session["User"];
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Ubicacion()
        {
            
            ViewBag.CurrentUser = Session["User"];
            return View();
        }
    }
}