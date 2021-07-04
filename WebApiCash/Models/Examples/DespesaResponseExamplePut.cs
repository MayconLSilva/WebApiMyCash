using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCash
{
    internal class DespesaResponseExamplePut : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Despesa 
            {
                idDespesa = 3,                
                idUsuario_fk = 1,
                tipoDocumentoDespesa = "BOLETO",
                dataLancamentoDespesa = Convert.ToDateTime("2021-06-29 23:52:21"),
                dataPagamentoDespesa = Convert.ToDateTime("2021-06-29 23:52:21"),
                valorDespesa = 35.26F,
                benificiarioDespesa = 0,
                obsDespesa = "Despesa ref. manutenção do carro",
                categoriaDespesa = "MANUTENÇÃO"
            };
        }
    }
}