//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace C2G.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReservaServicio
    {
        public int id_reserva_servicio { get; set; }
        public int id_reserva { get; set; }
        public int id_servicio { get; set; }
        public byte cantidad { get; set; }
        public decimal cargo { get; set; }
    
        public virtual Reserva Reserva { get; set; }
        public virtual Servicio Servicio { get; set; }
    }
}
