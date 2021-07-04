using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCash
{
    internal class UsuarioResponseExamplePut : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Usuario 
            {
                idUsuario = 3,
                nomeUsuario = "Joao",
                cpfUsuario = "63745113012",
                senhaUsuario = "123456",
                emailUsuario = "joao@hotmail.com",
                ativoUsuario = true

            };
        }
    }
}