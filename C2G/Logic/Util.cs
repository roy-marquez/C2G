using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2G.Logic
{
    public class Util
    {
        /** Regresa la cantidad de dias entre dos fechas pero, si la cantidad
         *  de dias es menor a 1 (retiro y devolucion el mismo dia) entoces
         *  regresa 1, siendo Un dia la cantidad mínima de dias admisibles
         */
        public int DiasEntreFechas(DateTime FechaInicio, DateTime FechaFin)
        {
            TimeSpan diferencia = FechaFin.Subtract(FechaInicio);
            return diferencia.Days < 1 ? 1 : diferencia.Days;
        }
    }
}