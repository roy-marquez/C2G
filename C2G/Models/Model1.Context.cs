﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Car2GoDBEntities : DbContext
    {
        public Car2GoDBEntities()
            : base("name=Car2GoDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Accesorio> Accesorio { get; set; }
        public virtual DbSet<Auto> Auto { get; set; }
        public virtual DbSet<Modulo> Modulo { get; set; }
        public virtual DbSet<Operacion> Operacion { get; set; }
        public virtual DbSet<ReservaAccesorio> ReservaAccesorio { get; set; }
        public virtual DbSet<ReservaServicio> ReservaServicio { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolOperacion> RolOperacion { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioReserva> UsuarioReserva { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
    }
}
