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
            ViewBag.CurrentUser = oUsuario.nombre;

            List<ListReservaViewModel> lst;
            //List<ListUsuarioReservaViewModel> lstUsuarioReserva;
            //UsuarioReservaViewModel usuarioReserva;

            using (Car2GoDBEntities db = new Car2GoDBEntities())
            {
                //lstUsuarioReserva = (from usuarioReserva in db.UsuarioReserva
                //                     where usuarioReserva.id_usuario == oUsuario.id_usuario
                //                     select new ListUsuarioReservaViewModel
                //                     {
                //                         IdReserva = usuarioReserva.id_reserva
                //                     }).ToList();

                //inner join devuelve todas las reservas del Usuario Logueado
                lst = (from reserva in db.Reserva
                        join usuario_reserva in db.UsuarioReserva on reserva.id_reserva equals usuario_reserva.id_reserva
                        //join usr in db.Usuario on usuario_reserva.id_usuario equals oUsuario.id_usuario
                       where usuario_reserva.id_usuario == oUsuario.id_usuario
                       select new ListReservaViewModel
                       {
                           //Datos de Cliente
                           Nombre = oUsuario.nombre,
                           Apellido1 = oUsuario.apellido1,
                           Apellido2 = oUsuario.apellido2,
                           Email = oUsuario.email,

                           //Datos de Reservas
                           IdReserva = reserva.id_reserva,
                           FechaHoraReserva = reserva.fecha_hora_reserva,
                           IdAuto = reserva.id_auto,
                           LugarRetiro = reserva.lugar_retiro,
                           FechaRetiro = reserva.fecha_retiro,
                           HoraRetiro = reserva.hora_retiro,
                           LugarDevolucion = reserva.lugar_devolucion,
                           FechaDevolucion = reserva.fecha_devolucion,
                           HoraDevolucion = reserva.hora_devolucion,
                           CantidadDias = reserva.cantidad_dias,
                           CargosServicios = reserva.cargos_servicios,
                           CargosAccesorios = reserva.cargos_accesorios,
                           CargosSubtotal = reserva.cargos_subtotal,
                           Descuento = reserva.descuento,
                           CargosAtraso = reserva.cargos_atraso,
                           CargosDesperfecto = reserva.cargos_deperfecto,
                           CargosTotal = reserva.cargos_total,
                           MontoReembolso = reserva.monto_reembolso,
                           Estado = reserva.estado,

                           //Detalle de servicios de la reserva
                           Servicios = (from servicio in db.ReservaServicio
                                        where servicio.id_reserva == reserva.id_reserva
                                        select new ReservaServicioViewModel
                                        {
                                            Cantidad = servicio.cantidad,
                                            ServicioNombre = servicio.Servicio.nombre,
                                            ServicioDescripcion = servicio.Servicio.descripcion,
                                            CantidadDias = servicio.cantidad_dias,
                                            PrecioPorDia = servicio.precio_por_dia,
                                            Cargo = servicio.cargo

                                        }).ToList(),
                           //Detalle de accesorios de la reserva
                           Accesorios = (from accesorio in db.ReservaAccesorio
                                         where accesorio.id_reserva == reserva.id_reserva
                                         select new ReservaAccesorioViewModel
                                         {
                                             Cantidad = accesorio.cantidad,
                                             AccesorioNombre = accesorio.Accesorio.nombre,
                                             AccesorioDescripcion = accesorio.Accesorio.descripcion,
                                             CantidadDias = accesorio.cantidad_dias,
                                             PrecioPorDia = accesorio.precio_por_dia,
                                             Cargo = accesorio.cargo
                                         }).ToList()

                       }).ToList();
            }

            return View(lst);
        }

        [AuthorizeUser(idOperacion: 10)]
        public ActionResult AgregarReserva()
        {
            return View();
        }

        [AuthorizeUser(idOperacion: 11)]
        public ActionResult ConsultarReserva()
        {
            return View();
        }

        [AuthorizeUser(idOperacion: 12)]
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