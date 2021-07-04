using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCash
{
    internal class BeneficiarioResponseExamplePost : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Benificiario 
            {
                idBenificiario = 0,
                nomeBenificiario = "nomeBeneficiário",
                idUsuario_fk = 1

            };
        }
    }
}