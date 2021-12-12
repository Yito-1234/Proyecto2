using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto2.Models;
using System.Net;

namespace Proyecto2.Controllers
{
    public class SolicitudproductosController : Controller
    {
        private readonly Order2GoContext _context;

        private VentaComidum comidas;

        public SolicitudproductosController(Order2GoContext context)
        {

            _context = context;
        }



        public IActionResult Index()
        {

            var listpro = _context.SolicitudProductos.Include(d => d.IdComidaNavigation).Include(d => d.IdProductoNavigation);

            return View(listpro.ToList());



        }



        public ActionResult Create()

        {
            ViewBag.IdProducto = new SelectList(_context.Productos, "IdProducto", "Nombre");
            ViewBag.IdComida = new SelectList(_context.Comidas, "IdComida", "NomComida");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("IdReseta,IdProducto,Cantidad,IdComida")] SolicitudProducto soli)
        {


            if (ModelState.IsValid)
            {
                _context.SolicitudProductos.Add(soli);
                _context.SaveChanges();
                TempData["mensaje"] = "Comercio agregado correctamente";
                return RedirectToAction("Index");

            }

            ViewBag.IdProducto = new SelectList(_context.Productos, "IdProducto", "Nombre", soli.IdProductoNavigation);
            ViewBag.IdComida = new SelectList(_context.Comidas, "IdComida", "NomComida", soli.IdComidaNavigation);
            return View(soli);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudProducto soli = _context.SolicitudProductos.Find(id);
            if (soli == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProducto = new SelectList(_context.Productos, "IdProducto", "Nombre", soli.IdProductoNavigation);
            ViewBag.IdComida = new SelectList(_context.Comidas, "IdComida", "NomComida", soli.IdComidaNavigation);
            return View(soli);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("IdReseta,IdProducto,Cantidad,IdComida")] SolicitudProducto soli)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(soli).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProducto = new SelectList(_context.Productos, "IdProducto", "Nombre", soli.IdProductoNavigation);
            ViewBag.IdComida = new SelectList(_context.Comidas, "IdComida", "NomComida", soli.IdComidaNavigation);
            return View(soli);
        }










        // GET: DETALLE_COMPRA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudProducto soli = _context.SolicitudProductos.Find(id);
            if (soli == null)
            {
                return HttpNotFound();
            }
            return View(soli);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolicitudProducto soli = _context.SolicitudProductos.Find(id);
            _context.SolicitudProductos.Remove(soli);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
