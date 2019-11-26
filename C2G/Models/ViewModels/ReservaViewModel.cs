using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace C2G.Models.ViewModels
{
    public class ReservaViewModel
    {
        //DATOS DEL CLIENTE
        public string Nombre { get; set; }

        public string Apellido1 { get; set; }

        public string Apellido2 { get; set; }

        public string Email { get; set; }

        //DATOS DE RESERVA
        public int IdReserva { get; set; }

        public DateTime FechaHoraReserva { get; set; }

        public int IdAuto { get; set; }

        //RETIRO DE AUTO
        [Display(Name = "Lugar de Retiro")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string LugarRetiro { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        [Display(Name = "Fecha de Retiro")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime FechaRetiro { get; set; }

        [Display(Name = "Hora de Retiro")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime HoraRetiro { get; set; }

        //DEVOLUCION DE AUTO
        [Display(Name = "Lugar de Devolución")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string LugarDevolucion{ get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        [Display(Name = "Fecha de Devolución")]
        public DateTime FechaDevolucion { get; set; }

        [Display(Name = "Hora de Devolución")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime HoraDevolucion { get; set; }

        // PAGO INICIAL
        public int  CantidadDias { get; set; }

        public decimal CargosServicios { get; set; }

        public decimal CargosAccesorios { get; set; }

        public decimal CargosSubtotal { get; set; }
        
        public decimal Descuento { get; set; }
        

        //PAGO FINAL CIERRE
        public decimal CargosAtraso { get; set; }
        
        public decimal CargosDesperfecto { get; set; }

        public decimal CargosTotal { get; set; }

        public decimal MontoReembolso { get; set; }

        public string Estado { get; set; }

        //SERVICIOS Y ACCESORIOS
        public List<ReservaServicio> Servicios { get; set; }

        public List<ReservaAccesorio> Accesorios { get; set; }
    }
}