using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaginaWebFfmpeg.DAOs;
using PaginaWebFfmpeg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaginaWebFfmpeg.Views.Home
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        public object ViewBag { get; private set; }

        [Route("ping")]
        public string Ping()
        {
            return "Pong";
        }

        [Route("check")]
        [HttpPost]
        public IActionResult checkUser([FromBody] JsonElement body)
        {

            dynamic json = JsonConvert.DeserializeObject<dynamic>(body.ToString());
            User usuario = CollectionUser.getInstance().verUsuario(json.username.Value, json.password.Value);

            //  if (usuario != null) {
            if (usuario != null && usuario.licenciado != false) {
            return Ok(new { valid = true });
            }
            else
            { 
                return Ok(new { valid = false });
            }

            

        }
    }
}
