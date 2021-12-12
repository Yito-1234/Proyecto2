using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.IO;
using Proyecto2.Models;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Imaging;



namespace Proyecto2.Controllers
{
    public class ComidasController : Controller
    {
        private readonly Order2GoContext _context;

        private Comida comidas;

        public ComidasController(Order2GoContext context)
        {

            _context = context;
        }



        public IActionResult Index()
        {

            var listpro = _context.Comidas.Include(d => d.IdComercioNavigation);
            return View(listpro.ToList());



        }

        public IActionResult Menu()
        {

            var listpro = _context.Comidas.Include(d => d.IdComercioNavigation);
            return View(listpro.ToList());



        }


        /* public ActionResult ConvertirImagen(int id)
         {
             Producto imagen = _context.Productos.Where(x => x.IdProducto == id).FirstOrDefault();
             return File(imagen.Fotografia, "image/jpeg");

         }
        */


        public ActionResult getImage(int id)
        {
            Comida comidas = _context.Comidas.Find(id);

            byte[] byteImage = comidas.Fotografia;

            MemoryStream memoryStream = new MemoryStream(byteImage);
#pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
            Image image = Image.FromStream(memoryStream);
#pragma warning restore CA1416 // Validar la compatibilidad de la plataforma


            memoryStream = new MemoryStream();
#pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
            image.Save(memoryStream, ImageFormat.Jpeg);
#pragma warning restore CA1416 // Validar la compatibilidad de la plataforma
            memoryStream.Position = 0;
            return File(memoryStream, "image/jpg");

        }
        public ActionResult Create()
        {
            ViewBag.IdComercio = new SelectList(_context.AfiliacionComercios, "IdComercio", "Nombre");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Comida comidas, List<IFormFile> Fotografia)
        {



            foreach (var item in Fotografia)
            {

                if (item.Length > 0)
                {

                    using (var stream = new MemoryStream())
                    {

                        await item.CopyToAsync(stream);
                        comidas.Fotografia = stream.ToArray();
                    }
                }
            }
            _context.Comidas.Add(comidas);
            _context.SaveChanges();
            TempData["mensaje"] = "Comercio agregado correctamente";
            return RedirectToAction("Index");



        }




        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comida comidas = _context.Comidas.Find(id);
            if (comidas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdComercio = new SelectList(_context.AfiliacionComercios, "IdComercio", "Nombre", comidas.IdComercio);
            return View(comidas);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Comida comidas, List<IFormFile> Fotografia)
        {

            foreach (var item in Fotografia)
            {

                if (item.Length > 0)
                {

                    using (var stream = new MemoryStream())
                    {

                        await item.CopyToAsync(stream);
                        comidas.Fotografia = stream.ToArray();
                    }
                }
            }

            _context.Entry(comidas).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");

            ViewBag.IdComercio = new SelectList(_context.AfiliacionComercios, "IdComercio", "Nombre", comidas.IdComercio);
            return View(comidas);
        }















        // GET: DETALLE_COMPRA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comida comidas = _context.Comidas.Find(id);
            if (comidas == null)
            {
                return HttpNotFound();
            }
            return View(comidas);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comida comidas = _context.Comidas.Find(id);
            _context.Comidas.Remove(comidas);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    internal class HttpStatusCodeResult : ActionResult
    {
        private HttpStatusCode badRequest;

        public HttpStatusCodeResult(HttpStatusCode badRequest)
        {
            this.badRequest = badRequest;
        }
    }
}
