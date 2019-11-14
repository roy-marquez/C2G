using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2G.Models.ViewModels
{
    public class ListUsuarioViewModel
    {
        public int IdUsuario { get; set; }

        public string Password { get; set; }

        public string DocIdTipo { get; set; }

        public string DocIdNum { get; set; }

        public string Nombre { get; set; }

        public string Apellido1 { get; set; }

        public string Apellido2 { get; set; }

        public string Email { get; set; }

        public string LicenciaTipo { get; set; }

        public string LicenciaNum { get; set; }

        public string Nacionalidad { get; set; }

        public string PaisResidencia { get; set; }

        public string Direccion { get; set; }

        public int IdRol { get; set; }
    }
}