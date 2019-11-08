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
    public class InventarioAutosController : Controller
    {
        // OBTENER REGISTROS ++++++++++++++++++++
        public ActionResult Index()
        {
            List<ListAutoViewModel> lst;

            using (Car2GoDBEntities db = new Car2GoDBEntities())
            {
                lst = (from d in db.Auto
                       select new ListAutoViewModel
                       {
                           IdAuto = d.id_auto,
                           Placa = d.placa,
                           Marca = d.marca,
                           Modelo = d.modelo,
                           Transmision = d.transmision,
                           Combustible = d.combustible,
                           Color = d.color,
                           Anio = (DateTime)d.anio,
                           Rack = d.rack,
                           Tipo = d.tipo,
                           Lugar = d.lugar,
                           Estado = d.estado
                       }).ToList();
            }

            return View(lst);
        }


        //REGISTRO NUEVO+++++++++++++++
        //Sobre carga del Metodo
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult AgregarAuto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarAuto(AutoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Car2GoDBEntities db = new Car2GoDBEntities())
                    {
                        var oAuto = new Auto();
                        oAuto.placa = model.Placa;
                        oAuto.marca = model.Marca;
                        oAuto.modelo = model.Modelo;

                        db.Auto.Add(oAuto);
                        db.SaveChanges();
                    }
                    return Redirect("~/InventarioAutos/");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [AuthorizeUser(idOperacion: 5)]
        public ActionResult ConsultarAuto()
        {
            return View();
        }

        [AuthorizeUser(idOperacion: 7)]
        public ActionResult ModificarAuto()
        {
            return View();
        }

        [AuthorizeUser(idOperacion: 8)]
        public ActionResult EliminarAuto()
        {
            return View();
        }
    }
}