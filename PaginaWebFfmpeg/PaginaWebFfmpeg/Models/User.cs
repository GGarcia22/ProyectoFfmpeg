using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaWebFfmpeg.Models
{
    public class User
    {
        public String username { get; set; }
        public String password { get; set; }
        public bool licenciado { get; set; }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
            this.licenciado = licenciado;
        }


    }
}
