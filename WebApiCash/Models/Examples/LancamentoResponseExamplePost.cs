using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCash
{
    internal class LancamentoResponseExamplePost : IExamplesProvider
    {
        public object GetExamples()
        {
            return new LancamentoBancario 
            {
                idLanBancario = 0,
                bancoLanBancario = 1,
                dataSaidaLanBancario = Convert.ToDateTime("2021-06-29 23:52:21"),
                valorLanBancario = 25.35f,
                tipoLanBancario = "S",
                idUsuario_fk = 1
            };
        }
    }
}