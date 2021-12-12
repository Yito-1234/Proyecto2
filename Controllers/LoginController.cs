
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
    public class LoginController : Controller
    {
        private readonly Order2GoContext _context;

        public LoginController(Order2GoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario user)
        {

            if (user != null)
            {
                using (var context = new Order2GoContext())
                {
                    Usuario miUsuario = context.Usuarios.Where(x => x.UserName.ToUpper().Equals(user.UserName.ToUpper())
                            && x.Clave.Equals(user.Clave)).FirstOrDefault();

                    if (miUsuario != null)
                    {
                        HttpContext.Session.SetString("Usuario", JsonConvert.SerializeObject(miUsuario));

                        if (miUsuario.IdPerfil == 1)
                        {
                            return RedirectToAction("Index", "HomeAdmin");

                        }else
                        {
                            if (miUsuario.IdPerfil == 2)
                            {

                                return RedirectToAction("Index", "Home");


                            }else  {

                                return RedirectToAction("Index", "Homefinal");

                            } 

                        }
                       


                    }
                    else
                    {
                        return RedirectToAction("Login", "Login");
                    }
                }
            }

            return View();
        }
    }
}
