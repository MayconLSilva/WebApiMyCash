using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCash
{
    public class Util
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public string validaUsuario(int value)
        {
            var usuario =_context.Database.SqlQuery<string>($"select ativoUsuario from usuario where idUsuario = {value}").FirstOrDefault();

            return usuario;
        }

    }
}