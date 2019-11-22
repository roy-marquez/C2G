using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace C2G.Models.ViewModels
{
    public class AutoViewModel
    {
        [Required]
        public int IdAuto { get; set; }

        [StringLength(20, ErrorMessage = "Máximo 20 carácteres")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Placa { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 carácteres")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Marca { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 carácteres")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Modelo { get; set; }

        [StringLength(30, ErrorMessage = "Máximo 30 carácteres")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Transmision { get; set; }

        [StringLength(30, ErrorMessage = "Máximo 30 carácteres")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Combustible { get; set; }

        [StringLength(30, ErrorMessage = "Máximo 30 carácteres")]
        public string Color { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Año")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime Anio{ get; set; }

        [Display(Name = "Dispone de Rack")]
        public Byte Rack { get; set; }

        [StringLength(20, ErrorMessage = "Máximo 20 carácteres")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Tipo { get; set; }

        [Display(Name = "Ruta de Imagen")]
        public string ImagenRuta { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 carácteres")]
        public string Lugar { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Estado { get; set; }

    }
}