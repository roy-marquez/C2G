using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace C2G.Models.ViewModels
{
    public class ListReservaViewModel
    {
        //DATOS DEL CLIENTE
        public string Nombre { get; set; }

        [Display(Name = "Primer Apellido")]
        public string Apellido1 { get; set; }

        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }

        public string Email { get; set; }

        [Required]
        public int IdReserva { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha y Hora de Reserva")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime FechaHoraReserva { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public int IdAuto { get; set; }

        [Display(Name = "Lugar de retiro")]
        public string LugarRetiro { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de retiro")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime FechaRetiro { get; set; }

        // Recordar usar HTMLHelper Editorfor 
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [DataType(DataType.Time)]
        [Display(Name = "Hora de retiro")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime HoraRetiro { get; set; }

        [Display(Name = "Lugar de Devolución")]
        public string LugarDevolucion{ get; set; }

        [Display(Name = "Cantidad de Días")]
        public int  CantidadDias { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de devolución")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime FechaDevolucion { get; set; }

        // Recordar usar HTMLHelper Editorfor 
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [DataType(DataType.Time)]
        [Display(Name = "Hora de retiro")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime HoraDevolucion { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public decimal CargosServicios { get; set; }

        public decimal? CargosAccesorios { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public decimal CargosSubtotal { get; set; }
        
        public decimal? Descuento { get; set; }

        public decimal? CargosAtraso { get; set; }
        
        public decimal? CargosDesperfecto { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public decimal CargosTotal { get; set; }

        public decimal MontoReembolso { get; set; }

        [StringLength(20, ErrorMessage = "Máximo 20 carácteres")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Estado { get; set; }
        
        [Required(ErrorMessage = "Campo obligatorio")]
        public List<ReservaServicioViewModel> Servicios { get; set; }

        public List<ReservaAccesorioViewModel> Accesorios { get; set; }
    }
}