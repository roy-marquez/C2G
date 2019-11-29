using C2G.Models;
using C2G.Models.ViewModels;
using C2G.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace C2G.Controllers
{
    public class InventarioAutosController : Controller
    {
        // OBTENER REGISTROS 
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
                           ImagenRuta = d.imagen_ruta,
                           Lugar = d.lugar,
                           Estado = d.estado
                       }).ToList();
            }

            return View(lst);
        }


        // REGISTRO NUEVO
        // Sobre carga del Metodo
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult AgregarAuto()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarAuto(AutoViewModel model)
        {
            try
            {
                //Si todas las validaciones fueron correctas
                if (ModelState.IsValid)
                {
                    string nombreArchivo = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    string extension = Path.GetExtension(model.ImageFile.FileName);
                    //nombreArchivo = nombreArchivo + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                    nombreArchivo =  model.Placa+"_"+ model.Marca + "_"+ model.Modelo+"_"+ model.Color+"_"+ DateTime.Now.ToString("yyyy-MM-dd") + extension;
                    model.ImagenRuta = "~/Assets/images/inventario/" + nombreArchivo;
                    nombreArchivo = Path.Combine(Server.MapPath("~/Assets/images/inventario/"), nombreArchivo);
                    model.ImageFile.SaveAs(nombreArchivo);

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
                    ModelState.Clear();
                    return Redirect("~/InventarioAutos/");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // MODIFICAR REGISTRO 
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
                model.Rack = oAuto.rack;
                model.Tipo = oAuto.tipo;
                model.ImagenRuta = oAuto.imagen_ruta;
                model.Lugar = oAuto.lugar;
                model.Estado = oAuto.estado;
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
                    string nombreArchivo = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    string extension = Path.GetExtension(model.ImageFile.FileName);
                    //nombreArchivo = nombreArchivo + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                    nombreArchivo = model.Placa + "_" + model.Marca + "_" + model.Modelo + "_" + model.Color + "_" + DateTime.Now.ToString("yyyy-MM-dd") + extension;
                    model.ImagenRuta = "~/Assets/images/inventario/" + nombreArchivo;
                    nombreArchivo = Path.Combine(Server.MapPath("~/Assets/images/inventario/"), nombreArchivo);
                    model.ImageFile.SaveAs(nombreArchivo);

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
        
        [HttpGet]
        public ActionResult Detalles(int id)
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
                model.Rack = oAuto.rack;
                model.Tipo = oAuto.tipo;
                model.ImagenRuta = oAuto.imagen_ruta;
                model.Lugar = oAuto.lugar;
                model.Estado = oAuto.estado;
                model.IdAuto = oAuto.id_auto;
            }

            return View(model);
        }

        // ELIMINAR REGISTRO 
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