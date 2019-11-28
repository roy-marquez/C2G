using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace C2G.Models.ViewModels
{
    public class UsuarioViewModel
    {
        [Required]
        public int IdUsuario { get; set; }

        [StringLength(100, ErrorMessage = "Máximo 100 carácteres")]
        [Required(ErrorMessage = "Campo obligatorio")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(100, ErrorMessage = "Máximo 100 carácteres")]
        [Required(ErrorMessage = "Campo obligatorio")]
        [DataType(DataType.Password)]
        [Display(Name = "Reingrese Password")]
        [Compare("Password", ErrorMessage = "Passwords No coinciden")]
        public string RePassword { get; set; }

        [Display(Name = "Tipo de ID")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string DocIdTipo { get; set; }

        [Display(Name = "Número de ID")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string DocIdNum { get; set; }
        
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }

        [Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Apellido1 { get; set; }

        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }

        [Display(Name = "E-mail (Usuario en Login)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        //[DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Formato Email Incorrecto")]
        public string Email { get; set; }

        [Display(Name = "Tipo de Licencia")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string LicenciaTipo { get; set; }

        [Display(Name = "Número de Licencia")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string LicenciaNum { get; set; }

        public string Nacionalidad { get; set; }

        [Display(Name = "País de Residencia")]
        public string PaisResidencia { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required]
        public int IdRol { get; set; }
    }
}