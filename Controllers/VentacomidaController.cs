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
    public class VentacomidaController : Controller
    {
        private readonly Order2GoContext _context;

        private VentaComidum comidas;

        public VentacomidaController(Order2GoContext context)
        {

            _context = context;
        }



        public IActionResult Index()
        {

            var listpro = _context.VentaComida.Include(d => d.IdComidaNavigation);
            return View(listpro.ToList());



        }

        public IActionResult Menu()
        {

            var listpro = _context.Comidas.Include(d => d.IdComercioNavigation);
            return View(listpro.ToList());



        }



        public ActionResult Create()
        {
            ViewBag.IdComida = new SelectList(_context.Comidas, "IdComida", "NomComida");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("IdVenta,IdComida,Cantidad,Fecha")] VentaComidum ventacomida)
        {



            if (ModelState.IsValid)
            {
                _context.VentaComida.Add(ventacomida);
                _context.SaveChanges();
                TempData["mensaje"] = "Comercio agregado correctamente";
                return RedirectToAction("Index");

            }

            ViewBag.IdComida = new SelectList(_context.VentaComida, "IdComida", "NomComida", ventacomida.IdComidaNavigation);
            return View(ventacomida);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VentaComidum venta_comida = _context.VentaComida.Find(id);
            if (venta_comida == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdComida = new SelectList(_context.Comidas, "IdComida", "NomComida", venta_comida.IdComidaNavigation);
            return View(venta_comida);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("IdVenta,IdComida,Cantidad,Fecha")] VentaComidum ventacomida)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(ventacomida).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdComida = new SelectList(_context.Comidas, "IdComida", "NomComida", ventacomida.IdComidaNavigation);
            return View(ventacomida);
        }










        // GET: DETALLE_COMPRA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VentaComidum ventacomida = _context.VentaComida.Find(id);
            if (ventacomida == null)
            {
                return HttpNotFound();
            }
            return View(ventacomida);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VentaComidum ventacomida = _context.VentaComida.Find(id);
            _context.VentaComida.Remove(ventacomida);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
