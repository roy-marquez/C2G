using C2G.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C2G.Models;
using C2G.Models.ViewModels;
using C2G.Logic;
using System.Data;
using System.Data.SqlClient;

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

        //REGISTRO NUEVO+++++++++++++++
        //Sobre carga del Metodo
        [AuthorizeUser(idOperacion: 10)]
        public ActionResult AgregarReserva()
        {
            ViewBag.Title = "Agregar Reserva";
            Usuario user = (Usuario)Session["User"];
            ViewBag.NombreCompleto = user.nombre+" "+user.apellido1+" "+user.apellido2;
            ViewBag.Email = user.email;
            return View();
        }

        [HttpPost]
        public ActionResult AgregarReserva(ReservaViewModel model)
        {
            Util u = new Util();
            
            try
            {
                //Si todas las validaciones fueron correctas
                if (ModelState.IsValid) {
                    using (Car2GoDBEntities db = new Car2GoDBEntities())
                    {
                                           
                        /** Inserto la reserva en la DB usando el SP_InsertarReserva
                         *  El cual regresa el id_reserva que se acaba de insertar se 
                         *  guarda en el int IdReservaNueva y se usara para actualizar las
                         *  tablas de ReservaServicio, ReservaAccesorio, UsuarioReserva 
                         */
                        int IdReservaNueva = (int)db.SP_InsertarReserva(
                            DateTime.Now,
                            model.IdAuto,

                            model.LugarRetiro,                          
                            model.FechaRetiro,
                            model.HoraRetiro,

                            model.LugarDevolucion,
                            model.FechaDevolucion,
                            model.HoraDevolucion,

                            u.DiasEntreFechas(model.FechaRetiro, model.FechaDevolucion),
                            model.CargosServicios,
                            model.CargosAccesorios,
                            model.CargosSubtotal,
                            model.Descuento,

                            model.CargosAtraso,
                            model.CargosDesperfecto,
                            model.CargosTotal,
                            model.MontoReembolso,
                            model.Estado

                            ).SingleOrDefault();
                        db.SaveChanges();

                        //Agregamos lista de servicios 
                        AgregarReservaServiciosEnDB(db, model, IdReservaNueva);

                        //Agregamos lista de accesorios, si los hay...
                        AgregarReservaAccesoriosEnDB(db,model, IdReservaNueva);

                        //Registrar Reserva en UsuarioReserva
                        RegistrarUsuarioReservaEnDB(db, IdReservaNueva);
                    }
                    //Si todas las inserciones fueron correctas regresamos al index de reservas (historial)
                    return Redirect("~/Reservas/");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); ;
            }
        }

        private void AgregarReservaServiciosEnDB( Car2GoDBEntities db, ReservaViewModel model, int IdReservaNueva)
        {
            
            /* Si la insercion de la nueva reserva es correcta, la variable
             * IdReservaNueva sera mayor a cero, entonces inserto la lista de servicios en la tabla
             * [dbo.ReservaServicio] utilizando el procedimiento almacenado
             * [SP_InsertarReservaServicios]  el cual usa internamente el tipo personalizado 
             * [DatosReservaServicios] que recibe una lista de parametros en formato tabla.
             * */
            if (IdReservaNueva > 0)
            {
                var dtServicios = new DataTable();
                dtServicios.Columns.Add("id_reserva", typeof(int));
                dtServicios.Columns.Add("id_servicio", typeof(int));
                dtServicios.Columns.Add("cantidad", typeof(Byte));
                dtServicios.Columns.Add("cantidad_dias", typeof(int));
                dtServicios.Columns.Add("precio_por_dia", typeof(decimal));
                dtServicios.Columns.Add("cargo", typeof(decimal));

                foreach (ReservaServicio rs in model.Servicios)
                {
                    /* Nótese como el primer parametro es la varible IdReservaNueva
                     * que fue regresada en la operacion de insercion de reserva
                     */
                    dtServicios.Rows.Add(
                        IdReservaNueva,
                        rs.id_servicio,
                        rs.cantidad,
                        rs.cantidad_dias,
                        rs.precio_por_dia,
                        rs.cargo);
                }
                var parametros_servicios = new SqlParameter("@lst_servicios", SqlDbType.Structured);
                parametros_servicios.Value = dtServicios;
                parametros_servicios.TypeName = "dbo.DatosReservaServicios";

                db.Database.ExecuteSqlCommand("exec SP_InsertarReservaServicios @lst_servicios", parametros_servicios);
            }
        }

        private void AgregarReservaAccesoriosEnDB(Car2GoDBEntities db, ReservaViewModel model, int IdReservaNueva)
        {

            /* Si la insercion de la nueva reserva es correcta, la variable
             * IdReservaNueva sera mayor a cero, y ademas la lista de accesorios sea mayor a cero
             * entonces inserto la lista de acc en la tabla
             * [dbo.ReservaAccesorio] utilizando el procedimiento almacenado
             * [SP_InsertarReservaServicios]  el cual usa internamente el tipo personalizado 
             * [DatosReservaServicios] que recibe una lista de parametros en formato tabla.
             * */
            if (model.Accesorios.Count>0 && IdReservaNueva > 0)
            {
                var dtAccesorios = new DataTable();
                dtAccesorios.Columns.Add("id_reserva", typeof(int));
                dtAccesorios.Columns.Add("id_accesorio", typeof(int));
                dtAccesorios.Columns.Add("cantidad", typeof(Byte));
                dtAccesorios.Columns.Add("cantidad_dias", typeof(int));
                dtAccesorios.Columns.Add("precio_por_dia", typeof(decimal));
                dtAccesorios.Columns.Add("cargo", typeof(decimal));

                foreach (ReservaAccesorio ra in model.Accesorios)
                {
                    /* Nótese como el primer parametro es la varible IdReservaNueva
                     * que fue regresada en la operacion de insercion de reserva
                     */
                    dtAccesorios.Rows.Add(
                        IdReservaNueva,
                        ra.id_accesorio,
                        ra.cantidad,
                        ra.cantidad_dias,
                        ra.precio_por_dia,
                        ra.cargo);
                }
                var parametros_accesorios = new SqlParameter("@lst_accesorios", SqlDbType.Structured);
                parametros_accesorios.Value = dtAccesorios;
                parametros_accesorios.TypeName = "dbo.DatosReservaAccesorios";

                db.Database.ExecuteSqlCommand("exec SP_InsertarReservaAccesorios @lst_accesorios", parametros_accesorios);
            }
        }

        private void RegistrarUsuarioReservaEnDB (Car2GoDBEntities db, int IdReservaNueva)
        {
            oUsuario = (Usuario)Session["User"];
            int IdUsuario = oUsuario.id_usuario;

            var oUsuarioReserva = new UsuarioReserva
            {
                id_reserva = IdReservaNueva,
                id_usuario = IdUsuario
            };

            db.UsuarioReserva.Add(oUsuarioReserva);
            db.SaveChanges();
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