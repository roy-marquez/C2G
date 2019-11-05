using C2G.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C2G.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        //Atributo de Usuario Autorizado
        private Usuario oUsuario;
        private Car2GoDBEntities db = new Car2GoDBEntities();
        private int idOperacion;

        public AuthorizeUser(int idOperacion = 0)
        {
            this.idOperacion = idOperacion;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // Estas var me sirven para guardar a cual operacion y de cual modulo se
            // denegó acceso en caso de denegarse.
            string nombreOperacion = "";
            string nombreModulo = "";

            try
            {
                //creamos un objeto usuario apartir de la sesion vigente.
                oUsuario = (Usuario)HttpContext.Current.Session["User"];

                //creamos y llenamos una var(lista) de las operaciones preguntando por 
                //si existe en Rol la operacion(int) que nos envian como parametro
                //y que guardamos con el constructor.
                var listaMisOperaciones = from m in db.RolOperacion
                                          where m.id_rol == oUsuario.id_rol
                                          && m.id_operacion == idOperacion
                                          select m;

                //De no ser una operacion admitida para el usuario actual
                //la condicion de este if es true porque la operacion no existe
                //por tanto la lista tiene cero elementos, se comunica al usuario
                //que fue denegado su acceso a la operacion
                if (listaMisOperaciones.ToList().Count() == 0)
                {
                    var oOperacion = db.Operacion.Find(idOperacion);
                    int? idModulo = oOperacion.id_modulo;
                    nombreOperacion = getNombreDeOperacion(idOperacion);
                    nombreModulo = getNombreDelModulo(idOperacion);
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion + "&modulo=" + nombreModulo + "&msjeErrorException=");
                }
            }
            catch (Exception ex)
            {

                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion + "&modulo=" + nombreModulo + "&msjeErrorException=" + ex.Message);
            }

            //base.OnAuthorization(filterContext);
        }

        //regresa el nombre de operacion con base en su numero identificador
        public string getNombreDeOperacion(int idOperacion)
        {
            var operacion = from op in db.Operacion
                            where op.id_operacion == idOperacion
                            select op.nombre;
            string nombreOperacion;
            try
            {
                nombreOperacion = operacion.First();
            }
            catch (Exception)
            {
                nombreOperacion = "";
            }
            return nombreOperacion;
        }

        //regresa el nombre de operacion con base en su numero identificador
        public string getNombreDelModulo(int idModulo)
        {
            var modulo = from m in db.Modulo
                         where m.id_modulo == idModulo
                         select m.nombre;

            string nombreModulo;
            try
            {
                nombreModulo = modulo.First();
            }
            catch (Exception)
            {

                nombreModulo = "";
            }
            return nombreModulo;
        }
    }
}