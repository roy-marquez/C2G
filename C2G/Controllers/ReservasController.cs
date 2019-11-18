using C2G.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C2G.Models;
using C2G.Models.ViewModels;

namespace C2G.Controllers
{
    public class ReservasController : Controller
    {
        private Usuario oUsuario;

        public ActionResult Index()
        {
            oUsuario = (Usuario)Session["User"];
            

            List<ListReservaViewModel> lst;
            //List<ListUsuarioViewModel> DataUsuario;

            using (Car2GoDBEntities db = new Car2GoDBEntities())
            {

                //inner join devuelve todas las reservas de un usuario determinado
                lst = (from reserva in db.Reserva
                       join usuario_reserva in db.UsuarioReserva on reserva.id_reserva equals usuario_reserva.id_reserva
                       join usr in db.Usuario on usuario_reserva.id_usuario equals oUsuario.id_usuario
                       select new ListReservaViewModel
                       {
                           IdReserva = reserva.id_reserva,
                           FechaHoraReserva = reserva.fecha_hora_reserva,
                           IdAuto = reserva.id_auto,
                           LugarRetiro = reserva.lugar_retiro,
                           FechaRetiro = reserva.fecha_retiro,
                           HoraRetiro = reserva.hora_retiro,
                           LugarDevolucion = reserva.lugar_devolucion,
                           FechaDevolucion = reserva.fecha_devolucion,
                           HoraDevolucion = reserva.hora_devolucion,
                           CargosServicios = reserva.cargos_servicios,
                           CargosAccesorios = reserva.cargos_accesorios,
                           CargosSubtotal = reserva.cargos_subtotal,
                           Descuento = reserva.descuento,
                           CargosAtraso = reserva.cargos_atraso,
                           CargosDesperfecto = reserva.cargos_deperfecto,
                           CargosTotal = reserva.cargos_total,
                           Estado = reserva.estado
                       }).ToList();
            }

            return View(lst);
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