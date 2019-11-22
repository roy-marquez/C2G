using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2G.Models.ViewModels
{
    public class ListAutoViewModel
    {
        public int IdAuto { get; set; }

        public string Placa { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string Transmision { get; set; }

        public string Combustible { get; set; }

        public string Color { get; set; }

        public DateTime Anio { get; set; }

        public byte Rack { get; set; }

        public string Tipo { get; set; }

        public string ImagenRuta { get; set; }

        public string Lugar { get; set; }

        public string Estado { get; set; }

    }
}