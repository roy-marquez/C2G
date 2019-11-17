using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2G.Models.ViewModels
{
    public class Reserva
    {
        public int IdReserva { get; set; }

        public DateTime FechaHoraReserva { get; set; }

        public int IdAuto { get; set; }

        public string LugarRetiro { get; set; }

        public DateTime FechaRetiro { get; set; }

        public DateTime HoraRetiro { get; set; }

        public string LugarDevolucion{ get; set; }

        public DateTime FechaDevolucion { get; set; }

        public DateTime HoraDevolucion { get; set; }

        public decimal CargosServicios { get; set; }

        public decimal CargosAccesorios { get; set; }

        public decimal CargosTotal { get; set; }

        public string Estado { get; set; }

        public decimal Descuento { get; set; }

        public List<Servicio> Servicios { get; set; }

        public List<Accesorio> Accesorios { get; set; }
    }
}