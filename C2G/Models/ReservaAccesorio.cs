//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace C2G.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReservaAccesorio
    {
        public int id_reserva_accesorio { get; set; }
        public int id_reserva { get; set; }
        public int id_accesorio { get; set; }
        public byte cantidad { get; set; }
        public decimal cargo { get; set; }
    
        public virtual Accesorio Accesorio { get; set; }
        public virtual Reserva Reserva { get; set; }
    }
}