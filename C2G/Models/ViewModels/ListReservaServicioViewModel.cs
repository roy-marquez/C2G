using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2G.Models.ViewModels
{
    public class ListReservaServicioViewModel
    {
        public int IdReservaServicio { get; set; }

        public int IdReserva { get; set; }

        //Se usa para obtener las siguietnes propiedades
        public int IdServicio { get; set; }

        //Se obtiene de catalogo de servicios
        public string ServicioNombre { get; set; }

        //se obtiene de catalogo de servicios
        public string ServicioDescripcion { get; set; }

        public Byte Cantidad { get; set; }

        public int CantidadDias { get; set; }

        public decimal PrecioPorDia { get; set; }

        public decimal Cargo { get; set; }
    }
}