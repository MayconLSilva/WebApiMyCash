using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCash
{
    public class UsuarioRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Usuario { nomeUsuario = "Junior", cpfUsuario = "15124465050", senhaUsuario = "2236", emailUsuario = "jnior@hotmail.com",ativoUsuario = true };
        }
    }
}



