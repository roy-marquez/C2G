using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C2G.Controllers;
using C2G.Models;

namespace C2G.Filters
{
    public class VerificaSession : ActionFilterAttribute
    {
        private Usuario oUsuario;
 
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                oUsuario = (Usuario)HttpContext.Current.Session["User"];
                if (oUsuario == null)
                {
                    if(filterContext.Controller is AccesoController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Acceso/Login");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");
            }
        }
        
    }
}