using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2G.Models.ViewModels
{
    public class ReservaAccesorioViewModel
    {
        public int IdReservaAccesorio { get; set; }

        public int IdReserva { get; set; }

        //Se usa para obtener las siguietnes propiedades
        public int IdAccesorio { get; set; }

        //Se obtiene de catalogo de accesorios
        public string AccesorioNombre { get; set; }

        //se obtiene de catalogo de accesorios
        public string AccesorioDescripcion { get; set; }

        public Byte Cantidad { get; set; }

        public int CantidadDias { get; set; }

        public decimal PrecioPorDia { get; set; }

        public decimal Cargo { get; set; }
    }
}