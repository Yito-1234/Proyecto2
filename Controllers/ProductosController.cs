using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto2.Models;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;







namespace Proyecto2.Controllers
{
    public class ProductosController : Controller
    {
        private readonly Order2GoContext _context;

        private Producto product;

        public object Blog { get; private set; }

        public ProductosController(Order2GoContext context)
        {

            _context = context;
        }

        bool ValidarUsuarioAdmin()
        {
            string json = HttpContext.Session.GetString("Usuario");

            if (!String.IsNullOrEmpty(json))
            {
                Usuario miUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<Usuario>(json);
                if (miUsuario == null || miUsuario.IdPerfil != 2)
                    return false;
            }
            else
                return false;

            return true;
        }




        public IActionResult Index()
        {

            var listpro = _context.Productos.Include(d => d.IdComercioNavigation);
            return View(listpro.ToList());



        }

        public ActionResult getImage(int id)
        {
            Producto producto_cret = _context.Productos.Find(id);

            byte[] byteImage = producto_cret.Fotografia;

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


        /*  public ActionResult ConvertirImagen(int IdProducto)
           {
               Producto imagen = _context.Productos.Where(x => x.IdProducto == IdProducto).FirstOrDefault();
             return File(imagen.Fotografia, "image/jpeg");

           }
        */
        /*
                public ActionResult ConvertirImagen(int IdProducto)
                {

                    var imagen = (from Producto in _context.Productos
                                  where Producto.IdProducto == IdProducto
                                  select Producto.Fotografia).FirstOrDefault();
                    return File(imagen, "image/jpeg");
                }
        */

        public ActionResult Create()
        {
            ViewBag.IdComercio = new SelectList(_context.AfiliacionComercios, "IdComercio", "Nombre");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Producto producto_cret, List<IFormFile> Fotografia)
        {

            foreach (var item in Fotografia)
            {

                if (item.Length > 0)
                {

                    using (var stream = new MemoryStream())
                    {

                        await item.CopyToAsync(stream);
                        producto_cret.Fotografia = stream.ToArray();
                    }
                }
            }

            _context.Productos.Add(producto_cret);
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
            Producto producto_cret = _context.Productos.Find(id);
            if (producto_cret == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdComercio = new SelectList(_context.AfiliacionComercios, "IdComercio", "Nombre", producto_cret.IdComercio);
            return View(producto_cret);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Producto producto_cret, List<IFormFile> Fotografia)
        {

            foreach (var item in Fotografia)
            {

                if (item.Length > 0)
                {

                    using (var stream = new MemoryStream())
                    {

                        await item.CopyToAsync(stream);
                        producto_cret.Fotografia = stream.ToArray();
                    }
                }
            }



            _context.Entry(producto_cret).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");

            ViewBag.IdComercio = new SelectList(_context.AfiliacionComercios, "IdComercio", "Nombre", producto_cret.IdComercio);
            return View(producto_cret);
        }


        // GET: DETALLE_COMPRA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto_cret = _context.Productos.Find(id);
            if (producto_cret == null)
            {
                return HttpNotFound();
            }
            return View(producto_cret);
            ViewBag.IdComercio = new SelectList(_context.AfiliacionComercios, "IdComercio", "Nombre", producto_cret.IdComercio);
            return View(producto_cret);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto_cret = _context.Productos.Find(id);
            _context.Productos.Remove(producto_cret);
            _context.SaveChanges();
            return RedirectToAction("Index");
            ViewBag.IdComercio = new SelectList(_context.AfiliacionComercios, "IdComercio", "Nombre", producto_cret.IdComercio);
            return View(producto_cret);
        }
    }
}
