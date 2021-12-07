using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaginaWebFfmpeg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaWebFfmpeg.Controllers
{

    [ApiController]
    [Route("controller")]
    public class UserController : Controller
    {

            [HttpGet]
            public string Get()
            {

                var usuarios = CollectionUser.getInstance().verUsuarios();
                return JsonConvert.SerializeObject(usuarios);

            }

            [HttpGet("{id}")]
            public string Get(User usuario)
            {
                var usuarios = CollectionUser.getInstance().verUsuario(usuario.username, usuario.password);

                return JsonConvert.SerializeObject(usuarios);
            }

            [HttpPost]
            public void Post([FromBody] User usuarios)
            {

                CollectionUser.getInstance().agregarUsuario(usuarios);

            }

            [HttpPut("{id}")]
            public void Put([FromBody] User usuarios)
            {

                CollectionUser.getInstance().actualizarUsuario(usuarios);

            }


            [HttpDelete("{id}")]
            public void Delete(User usuario)
            {

                CollectionUser.getInstance().eliminarUsuario(usuario);

            }
        
}
