using C2G.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C2G.Controllers
{
    public class ReservasController : Controller
    {
        // GET: Reservas
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(idOperacion:10)]
        public ActionResult AgregarReserva()
        {
            return View();
        }

        [AuthorizeUser(idOperacion:11)]
        public ActionResult ConsultarReserva()
        {
            return View();
        }

        [AuthorizeUser(idOperacion:12)]
        public ActionResult ModificarReserva()
        {
            return View();
        }

        [AuthorizeUser(idOperacion: 13)]
        public ActionResult EliminarReserva()
        {
            return View();
        }
    }
}