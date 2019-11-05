using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C2G.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string pw)
        {
            try
            {
                //Abro conexion y valido datos ingresados contra datos en el modelo de db
                using (Models.Car2GoDBEntities db = new Models.Car2GoDBEntities())
                {
                    var oUser = (from d in db.Usuario
                                where d.email == user.Trim() && d.password == pw.Trim()
                                select d).FirstOrDefault();
                    
                    //Si la comprobación falla
                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o contraseña inválida";
                        return View();
                    }

                    //Si la comprobacion es positiva se crea sesion para el objeto Usuario
                    Session["User"] = oUser;
                }

                //Se redirecciona al Vista Index del HomeControler.
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}