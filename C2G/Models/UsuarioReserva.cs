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
    
    public partial class UsuarioReserva
    {
        public int id_usuario_reserva { get; set; }
        public int id_usuario { get; set; }
        public int id_reserva { get; set; }
    
        public virtual Reserva Reserva { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}