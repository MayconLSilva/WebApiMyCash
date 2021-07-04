using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCash
{
    internal class UsuarioSenhaResponseExamplePatch : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Usuario 
            {
                idUsuario = 3,                
                senhaUsuario = "456789"
            };
        }
    }
}