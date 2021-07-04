using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCash
{
    internal class BancoResponseExamplePut : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Banco //List<Banco>
            {
                //new Banco{ idBanco = 0, nomeBanco = "nomeBanco", idUsuario_fk = 0}
                idBanco = 5,
                nomeBanco = "nomeDoBanco",
                idUsuario_fk = 1

            };
        }
    }
}