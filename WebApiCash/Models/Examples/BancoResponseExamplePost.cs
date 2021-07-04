using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCash
{
    internal class BancoResponseExamplePost : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Banco 
            {
                idBanco = 0,
                nomeBanco = "nomeDoBanco",
                idUsuario_fk = 1

            };
        }
    }
}