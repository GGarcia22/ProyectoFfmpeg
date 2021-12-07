using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaginaWebFfmpeg.DAOs;
using PaginaWebFfmpeg.Models;
using Newtonsoft.Json;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;


namespace PaginaWebFfmpeg.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public object UsuarioDAO { get; private set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            var usuarioJson = HttpContext.Session.GetString("username");

            if (usuarioJson != null)
            {
                return Redirect("/Home/Profile");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Loguearse(string username, string password)
        {

            var usuarioEncontrado = CollectionUser.getInstance().verUsuario(username, password);

            if (usuarioEncontrado != null)
            {

                HttpContext.Session.SetString("username", JsonConvert.SerializeObject(usuarioEncontrado));

                return Redirect("/Home/Profile");

            }
            else
            {

                ViewBag.msg = "El usuario no existe";
                return View("Login");

            }

        }

        public IActionResult Register(String username, String password)
        {
            User usuario = new User(username, password);
            CollectionUser.getInstance().agregarUsuario(usuario);
            ViewBag.msg = "El usuario fue creado correctamente";
            return View("Login");
        }

        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return Redirect("/Home/Index");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
