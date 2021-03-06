using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order2GoV2.Controllers
{
    public class HomefinalController : Controller
    {
        public ActionResult Index()
        {
            string json = HttpContext.Session.GetString("Usuario");

            if (!String.IsNullOrEmpty(json))
            {
                Usuario miUsuario = JsonConvert.DeserializeObject<Usuario>(json);

                if (miUsuario == null || miUsuario.IdPerfil != 3)
                {
                    return RedirectToAction("Login", "Login");
                }

                ViewData["Usuario"] = miUsuario.Nombre + " " + miUsuario.Apellidos;

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }




        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Usuario");
            return RedirectToAction("Login", "Login");
        }
    }
}