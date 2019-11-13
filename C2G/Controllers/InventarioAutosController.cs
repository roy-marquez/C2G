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
                //Si todas las validaciones fueron correctas
                if (ModelState.IsValid)
                {
                    using (Car2GoDBEntities db = new Car2GoDBEntities())
                    {
                        var oAuto = new Auto
                        {
                            placa = model.Placa,
                            marca = model.Marca,
                            modelo = model.Modelo,
                            transmision = model.Transmision,
                            combustible = model.Combustible,
                            color = model.Color,
                            anio = model.Anio,
                            rack = Convert.ToByte(model.Rack),
                            tipo = model.Tipo,
                            imagen_ruta = model.ImagenRuta,
                            lugar = model.Lugar,
                            estado = model.Estado
                        };

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

        
        //[AuthorizeUser(idOperacion: 5)]
        //public ActionResult ConsultarAuto()
        //{
        //    return View();
        //}

        //MODIFICAR REGISTRO+++++++++++++++
        [AuthorizeUser(idOperacion: 7)]
        public ActionResult ModificarAuto(int id)
        {
            AutoViewModel model = new AutoViewModel();
            using (Car2GoDBEntities db = new Car2GoDBEntities())
            {
                var oAuto = db.Auto.Find(id);
                model.Placa = oAuto.placa;
                model.Marca = oAuto.marca;
                model.Modelo = oAuto.modelo;
                model.Transmision = oAuto.transmision;
                model.Combustible = oAuto.combustible;
                model.Color = oAuto.color;
                model.Anio = Convert.ToDateTime(oAuto.anio);
                model.Rack = Convert.ToBoolean(oAuto.rack);
                model.Tipo = oAuto.tipo;
                model.ImagenRuta = oAuto.imagen_ruta;
                model.Lugar = oAuto.lugar;
                model.Estado = oAuto.estado;
                //int v = model.IdAuto = oAuto.id_auto;
                model.IdAuto = oAuto.id_auto;
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult ModificarAuto(AutoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Car2GoDBEntities db = new Car2GoDBEntities())
                    {
                        var oAuto = db.Auto.Find(model.IdAuto);
                        oAuto.placa = model.Placa;
                        oAuto.marca = model.Marca;
                        oAuto.modelo = model.Modelo;
                        oAuto.transmision = model.Transmision;
                        oAuto.combustible = model.Combustible;
                        oAuto.color = model.Color;
                        oAuto.anio = model.Anio;
                        oAuto.rack = Convert.ToByte(model.Rack);
                        oAuto.imagen_ruta = model.ImagenRuta;
                        oAuto.lugar = model.Lugar;
                        oAuto.estado = model.Estado;
                        oAuto.id_auto = model.IdAuto;

                        db.Entry(oAuto).State = System.Data.Entity.EntityState.Modified;
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

        // ELIMINAR REGISTRO+++++++++++++++
        //Muy muy muy importante el parametro de entrada debe llamarse "id"
        //debe coincidir con el establecido en el archivo de Rutas "RouteConfig"
        [AuthorizeUser(idOperacion: 8)]
        public ActionResult EliminarAuto(int id)
        {
            
            using (Car2GoDBEntities db = new Car2GoDBEntities())
            {

                var oAuto = db.Auto.Find(id);
                db.Auto.Remove(oAuto);
                db.SaveChanges();
            }
                return Redirect("~/InventarioAutos/");
        }
    }
}