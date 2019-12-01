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
using System.Net.Mail;
using System.Net;
using System.Text;

namespace C2G.Controllers
{
    public class ReservasController : Controller
    {
        private Usuario oUsuario;

        [AuthorizeUser(idOperacion: 10)]
        public ActionResult Index()
        {
            oUsuario = (Usuario)Session["User"];
            //ViewBag.CurrentUser = oUsuario.nombre;

            // Lista historial de reservas de un cliente
            List<ListReservaViewModel> lst;

            using (Car2GoDBEntities db = new Car2GoDBEntities())
            {
                //inner join devuelve todas las reservas del Usuario Logueado
                lst = (from reserva in db.Reserva
                       join usuario_reserva in db.UsuarioReserva on reserva.id_reserva equals usuario_reserva.id_reserva
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

        //Recibe el id de la reserva, usando el metodo EnviarCorreo envia confirmacion a cliente a su correo registrado
        public JsonResult ConfirmacionReserva(int id)
        {
            oUsuario = (Usuario)Session["User"];
            string emailBody = "<h3>Confirmación de Reserva</h3>";
            emailBody += "<p>Estimado(a)," + oUsuario.nombre + " " + oUsuario.apellido1 + "</p>";
            emailBody += "<p>Se ha registrado su reserva con los siguientes datos generales: </p>";
            emailBody += "<hr> ";
           
            using (Car2GoDBEntities db = new Car2GoDBEntities())
            {
                Reserva reserva = new Reserva();
                reserva = db.Reserva.Find(id);
                emailBody += "<h4>Datos Generales</h4> ";
                emailBody += "<p>IdReserva: " + reserva.id_reserva + "</p>";
                emailBody += "<p>Fecha y hora de reserva: " + reserva.fecha_hora_reserva + "</p>";
                emailBody += "<p>IdAuto: " + reserva.id_auto + "</p> <br/><hr/>";

                //Retiro
                emailBody += "<h4>Retiro</h4> ";
                emailBody += "<p>Lugar de Retiro: " + reserva.lugar_retiro + "</p>";
                emailBody += "<p>Fecha de Retiro: " + reserva.fecha_retiro.ToShortDateString() + "</p>";
                emailBody += "<p>Hora de Retiro: " + reserva.hora_retiro.ToShortTimeString()+ "</p> <hr/>";

                //Devolucion
                emailBody += "<h4>Devolución</h4> ";
                emailBody += "<p>Lugar de Devolución: " + reserva.lugar_devolucion + "</p>";
                emailBody += "<p>Fecha de Devolución: " + reserva.fecha_devolucion.ToShortDateString() + "<p>";
                emailBody += "<p>Hora de Devolución: " + reserva.hora_devolucion.ToShortTimeString() + "</p> <hr/>";

                //Dias y cargos
                emailBody += "<h4>Cantidad de Días y Cargos </h4> ";
                emailBody += "<p>Cantidad de Dias: " + reserva.cantidad_dias + "</p>";
                emailBody += "<p>Cargos por servicios: " + reserva.cargos_servicios + "</p>";
                emailBody += "<p>Cargos por accesorios: " + reserva.cargos_accesorios + "</p><br/><hr/>";

                emailBody += "<p><strong>SubTotal: </strong>" + reserva.cargos_subtotal + "</p>";
                emailBody += "<p>El monto a reembolsar por deposito es: " + reserva.monto_reembolso + "</p> ";
            }
            emailBody += "<p> Muchas gracias. </p> <p><strong> Car2Go Costa Rica </strong> </p>";
            bool result = false;
            result = EnviarCorreo(oUsuario.email, "Car2Go >> Confirmacion de Reserva #"+id, emailBody);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public bool EnviarCorreo(string toEmail, string subject, string emailBody)
        {
            try
            {
                //El email y el password del servidor de correo utilizado debe configurarse en Web.config
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                //Configuracion del Email
                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;

                //Datos y configuracion del Servidor SMTP
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                client.Send(mailMessage);
                
                return true;
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al enviar confirmacion por email: " + ex.Message.ToString();
                return false;            
            }
            
        }

        //REGISTRO NUEVO+++++++++++++++
        //Sobre carga del Metodo
        [AuthorizeUser(idOperacion: 10)]
        public ActionResult AgregarReserva()
        {
            ViewBag.Title = "Agregar Reserva";
            Usuario user = (Usuario)Session["User"];
            ViewBag.NombreCompleto = user.nombre + " " + user.apellido1 + " " + user.apellido2;
            ViewBag.Email = user.email;

            //Listas para cada tipo de Auto, solo autos disponibles en momento de consulta
            List<AutoViewModel> lstAutos4x4 = ListaAutosPorTipo("4x4");
            List<AutoViewModel> lstAutosSedan = ListaAutosPorTipo("sedan");
            List<AutoViewModel> lstAutosEconomico = ListaAutosPorTipo("economico");

            //Listas de Items DropDownList para cada tipo de Auto con Etiqueta de Agrupamiento
            ViewBag.ItemsAutos4x4 = ItemsAutosAgrupados(lstAutos4x4, "4x4");
            ViewBag.ItemsAutosSedan = ItemsAutosAgrupados(lstAutosSedan, "Sedan");
            ViewBag.ItemsAutosEconomico = ItemsAutosAgrupados(lstAutosEconomico, "Compactos");

            //List<SelectListItem> itemsAutos4x4 = lstAutos4x4.ConvertAll(d =>
            //{
            //    return new SelectListItem()
            //    {
            //        Text = (d.Marca + ", " + d.Modelo + ", " + d.Transmision + ", " + d.Combustible + ", " + d.Color),
            //        Value = d.IdAuto.ToString(),
            //        Selected = false
            //    };
            //});

            return View();
        }

        // Abre una conexion a base de datos y devuelve una lista
        // de Autos del tipo que se pasa como parámetro: 4x4, sedan, economico
        private List<AutoViewModel> ListaAutosPorTipo(string tipo)
        {
            List<AutoViewModel> lstAutos = null;
            using (Car2GoDBEntities db = new Car2GoDBEntities())
            {
                lstAutos = (from d in db.Auto
                            where d.tipo == tipo && d.estado == "disponible"
                            select new AutoViewModel
                            {
                                IdAuto = d.id_auto,
                                Placa = d.placa,
                                Marca = d.marca,
                                Modelo = d.modelo,
                                Transmision = d.transmision,
                                Combustible = d.combustible,
                                Color = d.color,
                                Anio = (DateTime)d.anio,
                                Rack = d.rack,
                                Tipo = d.tipo,
                                Lugar = d.lugar,
                                Estado = d.estado
                            }
                            ).ToList();
            }
            return lstAutos;
        }

        // Convierte una Lista de AutoViewModels a Lista de SelectItems
        // Se usan para poblar la las dropDownList de la vista de AgregarReserva.
        private List<SelectListItem> ItemsAutos(List<AutoViewModel> lstAutos)
        {
            return lstAutos.ConvertAll(d =>
               {
                   return new SelectListItem()
                   {
                       Text = (d.Placa + ", " + d.Marca + ", " + d.Modelo + ", " + d.Transmision + ", " + d.Combustible + ", " + d.Color),
                       Value = d.IdAuto.ToString(),
                       Selected = false
                   };
               });
        }

        // Convierte una Lista de AutoViewModels a Lista de SelectItems
        // Se usan para poblar la las dropDownList de la vista de AgregarReserva.
        private List<SelectListItem> ItemsAutosAgrupados(List<AutoViewModel> lstAutos, string grupo)
        {
            var g = new SelectListGroup() { Name = grupo };

            return lstAutos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = (d.Placa + ", " + d.Marca + ", " + d.Modelo + ", " + d.Transmision + ", " + d.Combustible + ", " + d.Color),
                    Value = d.IdAuto.ToString(),
                    Selected = false,
                    Group = g
                };
            });
        }

        [HttpPost]
        public ActionResult AgregarReserva(ReservaViewModel model)
        {
            Util u = new Util();

            try
            {
                //Si todas las validaciones fueron correctas
                if (ModelState.IsValid)
                {
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
                        AgregarReservaAccesoriosEnDB(db, model, IdReservaNueva);

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

        private void AgregarReservaServiciosEnDB(Car2GoDBEntities db, ReservaViewModel model, int IdReservaNueva)
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
             * IdReservaNueva sera mayor a cero, y ademas la lista de accesorios sera mayor a cero
             * entonces inserto la lista de acc en la tabla
             * [dbo.ReservaAccesorio] utilizando el procedimiento almacenado
             * [SP_InsertarReservaServicios]  el cual usa internamente el tipo personalizado 
             * [DatosReservaServicios] que recibe una lista de parametros en formato tabla.
             * */
            if (model.Accesorios.Count > 0 && IdReservaNueva > 0)
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

        public static List<ReservaServicioViewModel> ServiciosDisponibles()
        {
            List<ReservaServicioViewModel> lstSrv = new List<ReservaServicioViewModel>();
            
            using (var db = new Car2GoDBEntities()) {

                foreach (Servicio srv in db.Servicio)
                {
                    ReservaServicioViewModel rsvm = new ReservaServicioViewModel();
                    rsvm.IdServicio = srv.id_servicio;
                    rsvm.ServicioNombre = srv.nombre;
                    rsvm.ServicioDescripcion= srv.descripcion;
                    rsvm.Cantidad = 0;
                    rsvm.CantidadDias = 1;
                    rsvm.PrecioPorDia = srv.precio;
                    rsvm.Cargo = rsvm.Cantidad * rsvm.CantidadDias * rsvm.PrecioPorDia;

                    lstSrv.Add(rsvm);   
                }
            }
            return lstSrv;
        }

        public static List<ReservaAccesorioViewModel> AccesoriosDisponibles()
        {
            List<ReservaAccesorioViewModel> lstAcc = new List<ReservaAccesorioViewModel>();

            using (var db = new Car2GoDBEntities())
            {

                foreach (Accesorio acc in db.Accesorio)
                {
                    ReservaAccesorioViewModel ravm = new ReservaAccesorioViewModel();
                    ravm.IdAccesorio = acc.id_accesorio;
                    ravm.AccesorioNombre = acc.nombre;
                    ravm.AccesorioDescripcion = acc.descripcion;
                    ravm.Cantidad = 0;
                    ravm.CantidadDias = 1;
                    ravm.PrecioPorDia = (int)acc.precio;
                    ravm.Cargo = ravm.Cantidad * ravm.CantidadDias * ravm.PrecioPorDia;

                    lstAcc.Add(ravm);
                }
            }
            return lstAcc;
        }

        private void RegistrarUsuarioReservaEnDB(Car2GoDBEntities db, int IdReservaNueva)
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