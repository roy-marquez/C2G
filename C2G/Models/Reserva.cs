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
    
    public partial class Reserva
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reserva()
        {
            this.ReservaAccesorio = new HashSet<ReservaAccesorio>();
            this.ReservaServicio = new HashSet<ReservaServicio>();
            this.UsuarioReserva = new HashSet<UsuarioReserva>();
        }
    
        public int id_reserva { get; set; }
        public System.DateTime fecha_hora_reserva { get; set; }
        public int id_auto { get; set; }
        public string lugar_retiro { get; set; }
        public System.DateTime fecha_retiro { get; set; }
        public System.DateTime hora_retiro { get; set; }
        public string lugar_devolucion { get; set; }
        public System.DateTime fecha_devolucion { get; set; }
        public System.DateTime hora_devolucion { get; set; }
        public decimal cargos_servicios { get; set; }
        public Nullable<decimal> cargos_accesorios { get; set; }
        public decimal cargos_subtotal { get; set; }
        public Nullable<decimal> descuento { get; set; }
        public Nullable<decimal> cargos_atraso { get; set; }
        public Nullable<decimal> cargos_deperfecto { get; set; }
        public decimal cargos_total { get; set; }
        public string estado { get; set; }
    
        public virtual Auto Auto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservaAccesorio> ReservaAccesorio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservaServicio> ReservaServicio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioReserva> UsuarioReserva { get; set; }
    }
}
