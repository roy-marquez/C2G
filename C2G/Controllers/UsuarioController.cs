using C2G.Models;
using C2G.Models.ViewModels;
using C2G.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C2G.Controllers
{
    public class UsuarioController : Controller
    {
        // OBTENER REGISTROS ++++++++++++++++++++
        public ActionResult Index()
        {
            List<ListUsuarioViewModel> lst;

            using (Car2GoDBEntities db = new Car2GoDBEntities())
            {
                lst = (from d in db.Usuario
                       select new ListUsuarioViewModel
                       {
                           IdUsuario = d.id_usuario,
                           Password = d.password,
                           DocIdTipo = d.doc_id_tipo,
                           DocIdNum = d.doc_id_num,
                           Nombre = d.nombre,
                           Apellido1 = d.apellido1,
                           Apellido2 = d.apellido2,
                           Email = d.email,
                           LicenciaTipo = d.licencia_tipo,
                           LicenciaNum = d.licencia_num,
                           Nacionalidad = d.nacionalidad,
                           PaisResidencia = d.pais_residencia,
                           Direccion = d.direccion,
                           IdRol = d.id_rol
                       }).ToList();
            }

            return View(lst);
        }


        [AuthorizeUser(idOperacion: 14)]
        public ActionResult Registrar()
        {
            return View();
        }

        //REGISTRO NUEVO CLIENTE +++++++++++++++
        //Sobre carga del Metodo
        [AuthorizeUser(idOperacion: 14)]
        public ActionResult RegistrarCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarCliente(UsuarioViewModel model)
        {
            try
            {
                //Si todas las validaciones fueron correctas
                if (ModelState.IsValid)
                {
                    using (Car2GoDBEntities db = new Car2GoDBEntities())
                    {
                        var oUsuario = new Usuario
                        {
                            password = model.Password,
                            doc_id_tipo = model.DocIdTipo,
                            doc_id_num = model.DocIdNum,
                            nombre = model.Nombre,
                            apellido1 = model.Apellido1,
                            apellido2 = model.Apellido2,
                            email = model.Email,
                            licencia_tipo = model.LicenciaTipo,
                            licencia_num = model.LicenciaNum,
                            nacionalidad = model.Nacionalidad,
                            pais_residencia = model.PaisResidencia,
                            direccion = model.Direccion,
                            id_rol = 2
                        };

                        db.Usuario.Add(oUsuario);
                        db.SaveChanges();
                    }

                    ViewBag.Usuario = model.Nombre + " " + model.Apellido1;
                    
                    return Redirect("~/Usuario/RegistroExitoso");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult RegistroExitoso()
        {
            return View();
        }


    }
}